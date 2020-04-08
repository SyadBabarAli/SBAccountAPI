using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SBAccountAPI.Context;
using SBAccountAPI.ModelCustom;
using SBAccountAPI.Models;
using SBAccountAPI.Utility;

namespace SBAccountAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/SaleRefund")]
    public class SaleRefundController : BaseController
    {
        private readonly EntityContext db = new EntityContext();

        [HttpPut]
        [Route("GetSaleRefund")]
        public IHttpActionResult GetSaleRefund(PagedResult<SaleRefundModel> pagedResult)
        {
            //IQueryable<SaleRefundModel> source = (from t1 in db.SaleRefunds
            //                                      join t2 in db.ListCustomers
            //                                      on
            //                                      t1.CustomerId equals t2.CustomerId
            //                                      into t1t2
            //                                      from leftt1t2 in t1t2.DefaultIfEmpty()

            //                                      where t1.IsDeleted == false
            //                                      select new SaleRefundModel
            //                                      {
            //                                          SaleRefundId = t1.SaleRefundId,
            //                                          Number = t1.Number,
            //                                          Date = t1.Date,
            //                                          CustomerId = t1.CustomerId,
            //                                          Reference = t1.Reference,
            //                                          CurrencyId = t1.CurrencyId,
            //                                          ExchangeRate = t1.ExchangeRate,

            //                                      });

            IQueryable<SaleRefundModel> source = (from t1 in db.SaleRefunds
                                                  join t2 in db.ListCustomers
                                                                on
                                                                t1.CustomerId equals t2.CustomerId
                                                                into t1t2
                                                  from leftt1t2 in t1t2.DefaultIfEmpty()


                                                  join t5 in db.SettingStatuses
                                                  on
                                                  t1.SettingStatusId equals t5.SettingStatusId
                                                  into t1t5
                                                  from leftt1t5 in t1t5.DefaultIfEmpty()

                                                  where
                                                  t1.IsDeleted == false
                                                  select new SaleRefundModel
                                                  {
                                                      CustomerName = leftt1t2.CustomerName,
                                                      Number = t1.Number,
                                                      Date = t1.Date,
                                                      Reference = t1.Reference,
                                                      TotalAmount = t1.TotalAmount,
                                                      OutstandingBalance = t1.OutstandingBalance,
                                                      CustomerId = t1.CustomerId,
                                                      StatusName = leftt1t5.Name,
                                                      SaleRefundId = t1.SaleRefundId,
                                                  });

            var result = source.GetTable<SaleRefundModel>(pagedResult);
            return Ok(result);
        }

        //[Route("GetSaleRefundDetails")]
        [HttpGet]
        public SaleRefundModel GetSaleRefundDetails(int id)
        {
            SaleRefundModel objModel = new SaleRefundModel();

            var child = (from t1 in db.SaleRefundDetails
                         join t2 in db.SettingPaymentMode
                         on
                         t1.PaymentModeId equals t2.SettingPaymentModeId
                         into t1t2
                         from leftt1t2 in t1t2.DefaultIfEmpty()

                         join t3 in db.SettingAccounts
                         on
                         t1.AccountId equals t3.SettingAccountId
                         into t1t3
                         from leftt1t3 in t1t3.DefaultIfEmpty()

                         where t1.CompanyId == 1 && t1.SaleRefundId == id && t1.IsDeleted == false
                         select new SaleRefundDetailModel
                         {
                             PaymentModeId = t1.PaymentModeId,
                             PaymentMode = leftt1t2.Name,
                             AccountId = t1.AccountId,
                             AccountName = leftt1t3.Name,

                             DocumentNumber = t1.DocumentNumber,
                             Amount = t1.Amount,

                             SaleRefundId = t1.SaleRefundId,

                             
                             CompanyId = t1.CompanyId,
                             IsDeleted = objModel.IsDeleted,
                             SaleRefundDetailId = t1.SaleRefundDetailId,
                         }).ToList();

            var master = (from t1 in db.SaleRefunds
                          join t2 in db.ListCustomers
                          on
                          t1.CustomerId equals t2.CustomerId
                          into t1t2
                          from leftt1t2 in t1t2.DefaultIfEmpty()

                          where
                          t1.CompanyId == 1
                          &&
                           t1.SaleRefundId == id
                           &&
                          t1.IsDeleted == false
                          select new SaleRefundModel
                          {
                              CustomerName = leftt1t2.CustomerName,
                              Number = t1.Number,
                              Date = t1.Date,
                              Reference = t1.Reference,
                              TotalAmount = t1.TotalAmount,
                              CustomerId = t1.CustomerId,
                              Comment = t1.Comment,
                              SaleRefundId = t1.SaleRefundId,
                          }).ToList();
            if (master.Count == 0)
            {
                objModel.settingStatus = db.SettingStatuses.ToList();
                objModel.settingPaymentMode = db.SettingPaymentMode.ToList();
                objModel.saleRefundDetailModel = child;
                return objModel;
            }
            else
            {
                master.FirstOrDefault().settingStatus = db.SettingStatuses.ToList();
                master.FirstOrDefault().settingPaymentMode = db.SettingPaymentMode.ToList();
                master.FirstOrDefault().saleRefundDetailModel = child;
                return master.FirstOrDefault();
            }
        }


        [ResponseType(typeof(SaleRefund))]
        [Route("Post")]
        public IHttpActionResult Post(SaleRefund objModel)
        {
            try
            {
                objModel.CompanyId = 1;
                objModel.IsDeleted = false;

                var pbj = db.SaleRefunds.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleRefundId).FirstOrDefault();
                int primaryKey = pbj == null ? 1 : pbj.SaleRefundId + 1;
                using (var dbContext = new EntityContext())
                {
                    var parent = new SaleRefund
                    {
                        CompanyId = objModel.CompanyId,
                        SaleRefundId = primaryKey,

                        CustomerId = objModel.CustomerId,
                        Number = objModel.Number,
                        Date = objModel.Date,
                        Reference = objModel.Reference,

                        Comment = objModel.Comment,
                        TotalAmount = objModel.TotalAmount,

                        SettingStatusId = objModel.SettingStatusId,

                        IsDeleted = objModel.IsDeleted,

                    };
                    dbContext.SaleRefunds.Add(parent);
                    dbContext.SaveChanges();

                    int cnt = 1;
                    foreach (var item in objModel.saleRefundDetail)
                    {
                        var child = new SaleRefundDetail
                        {
                            CompanyId = objModel.CompanyId,
                            SaleRefundId = primaryKey,
                            SaleRefundDetailId = cnt,

                            PaymentModeId = item.PaymentModeId,
                            AccountId = item.AccountId,
                            DocumentNumber = item.DocumentNumber,
                            Amount = item.Amount,

                            IsDeleted = false,
                        };
                        cnt++;
                        dbContext.SaleRefundDetails.Add(child);
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                string exMessage = null;
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        exMessage += "Error: \n" + ve.ErrorMessage;
                    }
                }
                return Content(HttpStatusCode.BadRequest, exMessage);
            }

            return Ok("Save");
        }
        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutSaleRefund(int id, SaleRefund objModel)
        {
            objModel.CompanyId = 1;
            objModel.IsDeleted = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleRefundId)
            {
                return BadRequest();
            }
            db.SaleRefundDetails.RemoveRange(db.SaleRefundDetails.Where(w => w.SaleRefundId == id));
            db.SaveChanges();
            int cnt = 1;
            foreach (SaleRefundDetail item in objModel.saleRefundDetail)
            {
                item.SaleRefundId = id;
                item.SaleRefundDetailId = cnt;
                item.CompanyId = objModel.CompanyId;
                item.IsDeleted = objModel.IsDeleted;
                cnt++;
            }

            if (objModel.saleRefundDetail.Count > 0)
            {
                db.SaleRefundDetails.AddRange(objModel.saleRefundDetail);
                db.SaveChanges();
            }

            db.Entry(objModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);

        }
        [ResponseType(typeof(SaleRefund))]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            SaleRefund objModel = db.SaleRefunds.Where(w => w.SaleRefundId == id).FirstOrDefault();
            objModel.IsDeleted = true;
            db.Entry(objModel).State = EntityState.Modified;

            objModel.IsDeleted = true;
            if (objModel == null)
            {
                return NotFound();
            }

            db.SaveChanges();

            return Ok(objModel);
        }

        [Route("GetProposedNumber")]
        public string GetProposedNumber()
        {
            SupportingFunctions objFN = new SupportingFunctions();
            var obj = db.SaleRefunds.OrderByDescending(x => x.SaleRefundId).Take(1);
            return objFN.GenerateProposedNumber(obj.FirstOrDefault() == null ? "0" : obj.FirstOrDefault().Number); ;
        }
        [Route("GetGroupBranches")]
        public IQueryable<ComboBox> GetGroupBranches()
        {
            var result = (from t1 in db.GroupBranches
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.GroupBranchId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetCurrencies")]
        public IQueryable<ComboBox> GetCurrencies()
        {
            var result = (from t1 in db.SettingCurrencies
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.CurrencyId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetSaleGeographySubAreas")]
        public IQueryable<ComboBox> GetSaleGeographySubAreas()
        {
            var result = (from t1 in db.SaleGeographySubAreas
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.SaleGeographySubAreaId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetProducts")]
        public IQueryable<ComboBox> GetProducts()
        {
            var result = (from t1 in db.ListProducts
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.ProductId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetGeneralWarehouses")]
        public IQueryable<ComboBox> GetGeneralWarehouses()
        {
            var result = (from t1 in db.GeneralWarehouses
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.GeneralWarehouseId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetSalePersons")]
        public IQueryable<ComboBox> GetSalePersons()
        {
            var result = (from t1 in db.ListSalePersons
                          orderby t1.SalePersonName ascending
                          select new ComboBox
                          {
                              value = t1.SalePersonId,
                              text = t1.SalePersonName
                          }).AsQueryable();

            return result;
        }

        [Route("GetSaleRefunde")]
        public IQueryable<SaleRefund> GetSaleRefunde(int id)
        {
            var result = db.SaleRefunds.Where(w => w.SaleRefundId == id && w.IsDeleted == false);
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("IsExist")]
        private bool IsExist(int id)
        {
            return db.SaleRefunds.Count(e => e.SaleRefundId == id) > 0;
        }

        [Route("GetProduct")]
        [HttpGet]
        public HttpResponseMessage GetProduct(string pSearch)
        {
            SupportingFunctions objFN = new SupportingFunctions();

            string serach = pSearch == null ? "" : pSearch.ToString();
            string query = "SELECT TOP 5 ProductId Value,Name Text,SalePrice FROM ListProduct " +
               " WHERE Name LIKE  '%" + serach + "%'  ORDER BY Name ASC ";

            SupportingFunctions supportingFunctions = new SupportingFunctions();
            DataTable objDataTable = supportingFunctions.QueryIntoDataTable(query);
            List<ListProductModel> objModelList = new List<ListProductModel>();
            objModelList = objFN.ConvertDataTable2<ListProductModel>(objDataTable);

            if (objModelList.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record were not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, objModelList);
        }

        [Route("GetCustomer")]
        [HttpGet]
        public HttpResponseMessage GetCustomer(string pSearch)
        {
            SupportingFunctions objFN = new SupportingFunctions();
            List<AutoCompelete> objModelList = new List<AutoCompelete>();
            string serach = pSearch == null ? "" : pSearch.ToString();
            string query = " SELECT TOP 5 CustomerId [Value],CustomerName [Text],* FROM ListCustomer " +
                " WHERE CustomerName LIKE  '%" + serach + "%'  ORDER BY CustomerName ASC ";
            objModelList = objFN.GetList(query);
            SupportingFunctions supportingFunctions = new SupportingFunctions();
            DataTable objDataTable = supportingFunctions.QueryIntoDataTable(query);
            objModelList = objFN.ConvertDataTable2<AutoCompelete>(objDataTable);

            if (objModelList.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record were not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, objModelList);
        }
        private bool SaleRefundExists(int id)
        {
            return db.SaleRefunds.Count(e => e.SaleRefundId == id) > 0;
        }
    }
}
