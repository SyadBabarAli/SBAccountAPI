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
    [RoutePrefix("api/SaleReturn")]
    public class SaleReturnController : BaseController
    {
        private readonly EntityContext db = new EntityContext();

        [HttpPut]
        [Route("GetSaleReturn")]
        public IHttpActionResult GetSaleReturn(PagedResult<SaleReturnModel> pagedResult)
        {
            IQueryable<SaleReturnModel> source = (from t1 in db.SaleReturns
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
                                                  select new SaleReturnModel
                                                  {
                                                      SaleReturnId = t1.SaleReturnId,
                                                      Number = t1.Number,
                                                      SaleReturnDate = t1.SaleReturnDate,
                                                      CustomerId = t1.CustomerId,
                                                      CustomerName = leftt1t2.CustomerName,
                                                      SettingStatusId = t1.SettingStatusId,
                                                      Reference = t1.Reference,
                                                      CurrencyId = t1.CurrencyId,
                                                      StatusName = leftt1t5.Name,
                                                      Comment = t1.Comment,
                                                      GrossAmount = t1.GrossAmount,
                                                      NetAmount = t1.NetAmount,
                                                      ExchangeRate = t1.ExchangeRate,
                                                      TaxAmount = t1.TaxAmount,
                                                      DiscountPercent = t1.DiscountPercent,
                                                      DiscountAmount = t1.DiscountAmount,
                                                  });
            var result = source.GetTable<SaleReturnModel>(pagedResult);
            return Ok(result);
        }

        [HttpGet]
        public SaleReturnModel GetSaleReturnDetails(int id)
        {
            SaleReturnModel objModel = new SaleReturnModel();

            var child = (from t1 in db.SaleReturnDetails
                         join t2 in db.ListProducts
                         on
                         t1.ProductId equals t2.ProductId
                         into t1t2
                         from leftt1t2 in t1t2.DefaultIfEmpty()

                         where
                           t1.CompanyId == 1
                          &&
                         t1.SaleReturnId == id
                         && t1.IsDeleted == false
                         select new SaleReturnDetailModel
                         {
                             SaleReturnId = t1.SaleReturnId,
                             CompanyId = t1.CompanyId,
                             SaleReturnDetailId = t1.SaleReturnDetailId,
                             ProductId = t1.ProductId,
                             ProductName = leftt1t2.Name,
                             Quantity = t1.Quantity,
                             Price = t1.Price,
                             DiscountAmount = t1.DiscountAmount,
                             NetAmount = t1.NetAmount,
                             Description = t1.Description,
                         }).ToList();

            var master = (from t1 in db.SaleReturns
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
                          && t1.SaleReturnId == id
                          && t1.IsDeleted == false
                          select new SaleReturnModel
                          {
                              SaleReturnId = t1.SaleReturnId,
                              Number = t1.Number,
                              SaleReturnDate = t1.SaleReturnDate,
                              CustomerId = t1.CustomerId,
                              CustomerName = leftt1t2.CustomerName,
                              SettingStatusId = t1.SettingStatusId,
                              Reference = t1.Reference,
                              CurrencyId = t1.CurrencyId,
                              StatusName = leftt1t5.Name,
                              Comment = t1.Comment,
                              GrossAmount = t1.GrossAmount,
                              NetAmount = t1.NetAmount,
                              ExchangeRate = t1.ExchangeRate,
                              TaxAmount = t1.TaxAmount,
                              DiscountPercent = t1.DiscountPercent,
                              DiscountAmount = t1.DiscountAmount,
                          }).ToList();
            if (master.Count == 0)
            {
                objModel.settingStatusModel = db.SettingStatuses.ToList();
                objModel.saleReturnDetailModel = child;
                return objModel;
            }
            else
            {
                master.FirstOrDefault().settingStatusModel = db.SettingStatuses.ToList();
                master.FirstOrDefault().saleReturnDetailModel = child;
                return master.FirstOrDefault();
            }
        }

        [ResponseType(typeof(SaleReturn))]
        [Route("Post")]
        public IHttpActionResult Post(SaleReturn objModel)
        {
            try
            {
                objModel.CompanyId = 1;
                objModel.IsDeleted = false;

                var pbj = db.SaleReturns.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleReturnId).FirstOrDefault();
                int primaryKey = pbj == null ? 1 : pbj.SaleReturnId + 1;
                using (var dbContext = new EntityContext())
                {
                    var parent = new SaleReturn
                    {
                        SaleReturnId = primaryKey,
                        CustomerId = objModel.CustomerId,
                        CompanyId = objModel.CompanyId,
                        Number = objModel.Number,
                        SaleReturnDate = objModel.SaleReturnDate,
                        Reference = objModel.Reference,
                        SettingStatusId = objModel.SettingStatusId,
                        IsDeleted = objModel.IsDeleted,
                        Comment = objModel.Comment,
                        GrossAmount = objModel.GrossAmount,
                        NetAmount = objModel.NetAmount,
                        ExchangeRate = objModel.ExchangeRate,
                        TaxAmount = objModel.TaxAmount,
                        DiscountPercent = objModel.DiscountPercent,
                        DiscountAmount = objModel.DiscountAmount,
                    };
                    dbContext.SaleReturns.Add(parent);
                    dbContext.SaveChanges();

                    int cnt = 1;
                    foreach (var item in objModel.SaleReturnDetail)
                    {
                        var child = new SaleReturnDetail
                        {
                            SaleReturnId = primaryKey,
                            CompanyId = objModel.CompanyId,
                            SaleReturnDetailId = cnt,
                            ProductId = item.ProductId,
                            Description = item.Description,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            DiscountAmount = item.DiscountAmount,
                            NetAmount = item.NetAmount,
                            IsDeleted = false
                        };
                        cnt++;
                        dbContext.SaleReturnDetails.Add(child);
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
        public IHttpActionResult PutSaleReturn(int id, SaleReturn objModel)
        {
            objModel.CompanyId = 1;
            objModel.IsDeleted = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleReturnId)
            {
                return BadRequest();
            }
            db.SaleReturnDetails.RemoveRange(db.SaleReturnDetails.Where(w => w.SaleReturnId == id && w.CompanyId == objModel.CompanyId));
            db.SaveChanges();
            int cnt = 1;
            foreach (SaleReturnDetail item in objModel.SaleReturnDetail)
            {
                item.SaleReturnId = id;
                item.SaleReturnDetailId = cnt;
                item.CompanyId = objModel.CompanyId;
                item.IsDeleted = false;
                cnt++;
            }

            if (objModel.SaleReturnDetail.Count > 0)
            {
                db.SaleReturnDetails.AddRange(objModel.SaleReturnDetail);
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
        [ResponseType(typeof(SaleReturn))]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            SaleReturn objModel = db.SaleReturns.Where(w => w.SaleReturnId == id).FirstOrDefault();
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
            var obj = db.SaleReturns.OrderByDescending(x => x.SaleReturnId).Take(1);
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

        [Route("GetSaleReturne")]
        public IQueryable<SaleReturn> GetSaleReturne(int id)
        {
            var result = db.SaleReturns.Where(w => w.SaleReturnId == id && w.IsDeleted == false);
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
            return db.SaleReturns.Count(e => e.SaleReturnId == id) > 0;
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
        private bool SaleReturnExists(int id)
        {
            return db.SaleReturns.Count(e => e.SaleReturnId == id) > 0;
        }
    }
}
