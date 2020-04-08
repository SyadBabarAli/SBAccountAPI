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
    [RoutePrefix("api/SaleReceiveMoney")]
    public class SaleReceiveMoneyController : BaseController
    {
        private readonly EntityContext db = new EntityContext();

        [HttpPut]
        [Route("GetSaleReceiveMoney")]
        public IHttpActionResult GetSaleReceiveMoney(PagedResult<SaleReceiveMoneyModel> pagedResult)
        {
            IQueryable<SaleReceiveMoneyModel> source = (from t1 in db.SaleReceiveMones
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

                                                        where t1.IsDeleted == false
                                                        select new SaleReceiveMoneyModel
                                                        {
                                                            SaleReceiveMoneyId = t1.SaleReceiveMoneyId,
                                                            Number = t1.Number,
                                                            ReceiveMoneyDate = t1.ReceiveMoneyDate,
                                                            CustomerId = t1.CustomerId,
                                                            CustomerName = leftt1t2.CustomerName,
                                                            Reference = t1.Reference,
                                                            CurrencyId = t1.CurrencyId,
                                                            Comment = t1.Comment,
                                                            TotalAmount = t1.TotalAmount,
                                                            AllocatedAmount = t1.AllocatedAmount,

                                                            StatusName = leftt1t5.Name,

                                                            SettingStatusId = t1.SettingStatusId,
                                                            ExchangeRate = t1.ExchangeRate,
                                                        });
            var result = source.GetTable<SaleReceiveMoneyModel>(pagedResult);
            return Ok(result);
        }

        [HttpGet]
        public SaleReceiveMoneyModel GetSaleReceiveMoneyDetails(int id)
        {
            SaleReceiveMoneyModel objModel = new SaleReceiveMoneyModel();

            //var v2 = db.SaleReceiveMoneyDetails;
            //var v1 = db.SaleReceiveMoneyDetails.AsQueryable();
            //var v3 =  db.CategoryBrands.Where(w => w.IsDeleted == false).AsQueryable();

            var child = (from t1 in db.SaleReceiveMoneyDetails
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

                         where t1.CompanyId == 1 
                         && t1.SaleReceiveMoneyId == id
                         && t1.IsDeleted == false
                         select new SaleReceiveMoneyDetailModel
                         {
                             SaleReceiveMoneyId = t1.SaleReceiveMoneyId,
                             CompanyId = t1.CompanyId,
                             SaleReceiveMoneyDetailId = t1.SaleReceiveMoneyDetailId,
                             PaymentModeId = t1.PaymentModeId,
                             PaymentModeName = leftt1t2.Name,

                             AccountId = t1.PaymentModeId,
                             AccountName = leftt1t3.Name,

                             Reference = t1.Reference,
                             InstrumentNumber = t1.InstrumentNumber,
                             InstrumentDate = t1.InstrumentDate,
                             Amount = t1.Amount,
                         }).ToList();

            var master = (from t1 in db.SaleReceiveMones
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
                          t1.CompanyId == 1
                          && t1.SaleReceiveMoneyId == id
                          && t1.IsDeleted == false
                          select new SaleReceiveMoneyModel
                          {
                              SaleReceiveMoneyId = t1.SaleReceiveMoneyId,
                              Number = t1.Number,
                              ReceiveMoneyDate = t1.ReceiveMoneyDate,
                              CustomerId = t1.CustomerId,
                              CustomerName = leftt1t2.CustomerName,
                              SettingStatusId = t1.SettingStatusId,
                              Reference = t1.Reference,
                              CurrencyId = t1.CurrencyId,
                              StatusName = leftt1t5.Name,
                              Comment = t1.Comment,
                              ExchangeRate = t1.ExchangeRate,
                          }).ToList();
            if (master.Count == 0)
            {
                objModel.settingStatusModel = db.SettingStatuses.ToList();
                objModel.SettingPaymentMode = db.SettingPaymentMode.ToList();
                objModel.SaleReceiveMoneyDetail = child;
                return objModel;
            }
            else
            {
                master.FirstOrDefault().settingStatusModel = db.SettingStatuses.ToList();
                master.FirstOrDefault().SettingPaymentMode = db.SettingPaymentMode.ToList();
                master.FirstOrDefault().SaleReceiveMoneyDetail = child;
                return master.FirstOrDefault();
            }
        }

        [ResponseType(typeof(SaleReceiveMoney))]
        [Route("Post")]
        public IHttpActionResult Post(SaleReceiveMoney objModel)
        {
            try
            {
                objModel.CompanyId = 1;
                objModel.IsDeleted = false;

                var pbj = db.SaleReceiveMones.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleReceiveMoneyId).FirstOrDefault();
                int primaryKey = pbj == null ? 1 : pbj.SaleReceiveMoneyId + 1;
                using (var dbContext = new EntityContext())
                {
                    var parent = new SaleReceiveMoney
                    {
                        SaleReceiveMoneyId = primaryKey,
                        CustomerId = objModel.CustomerId,
                        CompanyId = objModel.CompanyId,
                        Number = objModel.Number,
                        ReceiveMoneyDate = objModel.ReceiveMoneyDate,
                        Reference = objModel.Reference,
                        SettingStatusId = objModel.SettingStatusId,
                        IsDeleted = objModel.IsDeleted,
                        Comment = objModel.Comment,
                        ExchangeRate = objModel.ExchangeRate,
                        TotalAmount = objModel.TotalAmount,
                    };
                    dbContext.SaleReceiveMones.Add(parent);
                    dbContext.SaveChanges();

                    int cnt = 1;
                    foreach (var item in objModel.SaleReceiveMoneyDetail)
                    {
                        var child = new SaleReceiveMoneyDetail
                        {
                            SaleReceiveMoneyId = primaryKey,
                            CompanyId = objModel.CompanyId,
                            SaleReceiveMoneyDetailId = cnt,
                            PaymentModeId = item.PaymentModeId,
                            AccountId = item.AccountId,
                            Reference = item.Reference,
                            InstrumentNumber = item.InstrumentNumber,
                            InstrumentDate = item.InstrumentDate,
                            Amount = item.Amount,
                            IsDeleted = false
                        };
                        cnt++;
                        dbContext.SaleReceiveMoneyDetails.Add(child);
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
        public IHttpActionResult PutSaleReceiveMoney(int id, SaleReceiveMoney objModel)
        {
            objModel.CompanyId = 1;
            objModel.IsDeleted = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleReceiveMoneyId)
            {
                return BadRequest();
            }
            db.SaleReceiveMoneyDetails.RemoveRange(db.SaleReceiveMoneyDetails.Where(w => w.SaleReceiveMoneyId == id && w.CompanyId == objModel.CompanyId));
            db.SaveChanges();
            int cnt = 1;
            foreach (SaleReceiveMoneyDetail item in objModel.SaleReceiveMoneyDetail)
            {
                item.SaleReceiveMoneyId = id;
                item.SaleReceiveMoneyDetailId = cnt;
                item.CompanyId = objModel.CompanyId;
                item.IsDeleted = false;
                cnt++;
            }

            if (objModel.SaleReceiveMoneyDetail.Count > 0)
            {
                db.SaleReceiveMoneyDetails.AddRange(objModel.SaleReceiveMoneyDetail);
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
        [ResponseType(typeof(SaleReceiveMoney))]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            SaleReceiveMoney objModel = db.SaleReceiveMones.Where(w => w.SaleReceiveMoneyId == id).FirstOrDefault();
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
            var obj = db.SaleReceiveMones.OrderByDescending(x => x.SaleReceiveMoneyId).Take(1);
            return objFN.GenerateProposedNumber(obj.FirstOrDefault() == null ? "0" : obj.FirstOrDefault().Number); ;
        }

        [Route("GetSettingStatuses")]
        public IQueryable<ComboBox> GetSettingStatuses()
        {
            var result = (from t1 in db.SettingStatuses
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.SettingStatusId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetSaleReceiveMoneye")]
        public IQueryable<SaleReceiveMoney> GetSaleReceiveMoneye(int id)
        {
            var result = db.SaleReceiveMones.Where(w => w.SaleReceiveMoneyId == id && w.IsDeleted == false);
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
            return db.SaleReceiveMones.Count(e => e.SaleReceiveMoneyId == id) > 0;
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
        private bool SaleReceiveMoneyExists(int id)
        {
            return db.SaleReceiveMones.Count(e => e.SaleReceiveMoneyId == id) > 0;
        }
    }
}
