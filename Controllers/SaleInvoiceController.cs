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
    [RoutePrefix("api/SaleInvoice")]
    public class SaleInvoiceController : BaseController
    {
        private readonly EntityContext db = new EntityContext();

        [HttpPut]
        [Route("GetSaleInvoice")]
        public IHttpActionResult GetSaleInvoice(PagedResult<SaleInvoiceModel> pagedResult)
        {
            IQueryable<SaleInvoiceModel> source = (from t1 in db.SaleInvoices
                                                   join t2 in db.ListCustomers
                                                   on
                                                   t1.CustomerId equals t2.CustomerId
                                                   into t1t2
                                                   from leftt1t2 in t1t2.DefaultIfEmpty()

                                                   join t3 in db.GroupBranches
                                                   on
                                                   t1.GroupBranchId equals t3.GroupBranchId
                                                   into t1t3
                                                   from leftt1t3 in t1t3.DefaultIfEmpty()

                                                   join t4 in db.ListSalePersons
                                                   on
                                                   t1.SalePersonId equals t4.SalePersonId
                                                   into t1t4
                                                   from leftt1t4 in t1t4.DefaultIfEmpty()

                                                   join t5 in db.SettingStatuses
                                                   on
                                                   t1.SettingStatusId equals t5.SettingStatusId
                                                   into t1t5
                                                   from leftt1t5 in t1t5.DefaultIfEmpty()

                                                   where t1.IsDeleted == false
                                                   select new SaleInvoiceModel
                                                   {
                                                       SaleInvoiceId = t1.SaleInvoiceId,
                                                       Number = t1.Number,
                                                       InvoiceDate = t1.InvoiceDate,
                                                       CustomerId = t1.CustomerId,
                                                       CustomerName = leftt1t2.CustomerName,
                                                       GroupBranchId = t1.GroupBranchId,
                                                       BranchName = leftt1t3.Name,
                                                       SalePersonId = t1.SalePersonId,
                                                       SalePersonName = leftt1t4.SalePersonName,
                                                       InvoiceDueDate = t1.InvoiceDueDate,
                                                       SettingStatusId = t1.SettingStatusId,
                                                       Reference = t1.Reference,
                                                       CurrencyId = t1.CurrencyId,
                                                       StatusName = leftt1t5.Name,
                                                       Comments = t1.Comments,
                                                       GrossAmount = t1.GrossAmount,
                                                       NetAmount = t1.NetAmount,
                                                       ExchangeRate = t1.ExchangeRate,
                                                       TaxAmount = t1.TaxAmount,
                                                       DiscountPercent = t1.DiscountPercent,
                                                       DiscountAmount = t1.DiscountAmount,
                                                   });
            var result = source.GetTable<SaleInvoiceModel>(pagedResult);
            return Ok(result);
        }

        //[Route("GetSaleInvoiceDetails")]
        [HttpGet]
        public SaleInvoiceModel GetSaleInvoiceDetails(int id)
        {
            SaleInvoiceModel objModel = new SaleInvoiceModel();

            var child = (from t1 in db.SaleInvoiceDetails
                         join t2 in db.ListProducts
                         on
                         t1.ProductId equals t2.ProductId
                         into t1t2
                         from leftt1t2 in t1t2.DefaultIfEmpty()

                         join t6 in db.GeneralWarehouses
                         on
                         t1.GeneralWarehouseId equals t6.GeneralWarehouseId
                         into t1t6
                         from leftt1t6 in t1t6.DefaultIfEmpty()

                         where
                           t1.CompanyId == 1
                          &&
                         t1.SaleInvoiceId == id
                         && t1.IsDeleted == false
                         select new SaleInvoiceDetailModel
                         {
                             SaleInvoiceId = t1.SaleInvoiceId,
                             CompanyId = t1.CompanyId,
                             SaleInvoiceDetailId = t1.SaleInvoiceDetailId,
                             ProductId = t1.ProductId,
                             ProductName = leftt1t2.Name,

                             GeneralWarehouseId = t1.GeneralWarehouseId,
                             WarehouseName = leftt1t2.Name,

                             Quantity = t1.Quantity,
                             Price = t1.Price,
                             DiscountAmount = t1.DiscountAmount,
                             NetAmount = t1.NetAmount,
                             Description = t1.Description,
                             IsDeleted = t1.IsDeleted,
                         }).ToList();

            var master = (from t1 in db.SaleInvoices
                          join t2 in db.ListCustomers
                          on
                          t1.CustomerId equals t2.CustomerId
                          into t1t2
                          from leftt1t2 in t1t2.DefaultIfEmpty()

                          join t3 in db.GroupBranches
                          on
                          t1.GroupBranchId equals t3.GroupBranchId
                          into t1t3
                          from leftt1t3 in t1t3.DefaultIfEmpty()

                          join t4 in db.ListSalePersons
                          on
                          t1.SalePersonId equals t4.SalePersonId
                          into t1t4
                          from leftt1t4 in t1t4.DefaultIfEmpty()

                          join t5 in db.SettingStatuses
                          on
                          t1.SettingStatusId equals t5.SettingStatusId
                          into t1t5
                          from leftt1t5 in t1t5.DefaultIfEmpty()

                          where
                          t1.CompanyId == 1
                          &&
                           t1.SaleInvoiceId == id
                           &&
                          t1.IsDeleted == false
                          select new SaleInvoiceModel
                          {
                              GroupBranchId = t1.GroupBranchId,
                              SaleInvoiceId = t1.SaleInvoiceId,
                              Number = t1.Number,
                              InvoiceDate = t1.InvoiceDate,
                              CustomerId = t1.CustomerId,
                              CustomerName = leftt1t2.CustomerName,
                              BranchName = leftt1t3.Name,
                              SalePersonId = t1.SalePersonId,
                              SalePersonName = leftt1t4.SalePersonName,
                              InvoiceDueDate = t1.InvoiceDueDate,
                              SettingStatusId = t1.SettingStatusId,
                              Reference = t1.Reference,
                              CurrencyId = t1.CurrencyId,
                              StatusName = leftt1t5.Name,
                              Comments = t1.Comments,
                              GrossAmount = t1.GrossAmount,
                              NetAmount = t1.NetAmount,
                              ExchangeRate = t1.ExchangeRate,
                              TaxAmount = t1.TaxAmount,
                              DiscountPercent = t1.DiscountPercent,
                              DiscountAmount = t1.DiscountAmount,
                          }).ToList();
            if (master.Count == 0)
            {

                objModel.groupBranchModel = db.GroupBranches.ToList();
                objModel.salePersonModel = db.ListSalePersons.ToList();
                objModel.currencyModel = db.SettingCurrencies.ToList();
                objModel.groupBranchModel = db.GroupBranches.ToList();
                objModel.generalWarehouseModel = db.GeneralWarehouses.ToList();
                objModel.saleInvoiceDetailModel = child;
                return objModel;

            }
            else
            {

                master.FirstOrDefault().groupBranchModel = db.GroupBranches.ToList();
                master.FirstOrDefault().salePersonModel = db.ListSalePersons.ToList();
                master.FirstOrDefault().currencyModel = db.SettingCurrencies.ToList();
                master.FirstOrDefault().groupBranchModel = db.GroupBranches.ToList();
                master.FirstOrDefault().generalWarehouseModel = db.GeneralWarehouses.ToList();
                master.FirstOrDefault().saleInvoiceDetailModel = child;
                return master.FirstOrDefault();

            }
        }



        //[Route("GetSaleInvoices")]
        //[HttpGet]
        //public List<SaleInvoiceModel> GetSaleInvoices()
        //{
        //    var result = (from t1 in db.SaleInvoices
        //                  join t2 in db.ListCustomers
        //                  on
        //                  t1.CustomerId equals t2.CustomerId
        //                  into t1t2
        //                  from leftt1t2 in t1t2.DefaultIfEmpty()

        //                  join t3 in db.GroupBranches
        //                  on
        //                  t1.GroupBranchId equals t3.GroupBranchId
        //                  into t1t3
        //                  from leftt1t3 in t1t3.DefaultIfEmpty()

        //                  join t4 in db.ListSalePersons
        //                  on
        //                  t1.SalePersonId equals t4.SalePersonId
        //                  into t1t4
        //                  from leftt1t4 in t1t4.DefaultIfEmpty()

        //                  join t5 in db.SettingStatuses
        //                  on
        //                  t1.SettingStatusId equals t5.SettingStatusId
        //                  into t1t5
        //                  from leftt1t5 in t1t5.DefaultIfEmpty()

        //                  where t1.IsDeleted == false
        //                  select new SaleInvoiceModel
        //                  {
        //                      SaleInvoiceId = t1.SaleInvoiceId,
        //                      Number = t1.Number,
        //                      InvoiceDate = t1.InvoiceDate,
        //                      CustomerId = t1.CustomerId,
        //                      CustomerName = leftt1t2.CustomerName,
        //                      GroupBranchId = t1.GroupBranchId,
        //                      BranchName = leftt1t3.Name,
        //                      SalePersonId = t1.SalePersonId,
        //                      SalePersonName = leftt1t4.SalePersonName,
        //                      InvoiceDueDate = t1.InvoiceDueDate,
        //                      SettingStatusId = t1.SettingStatusId,
        //                      Reference = t1.Reference,
        //                      CurrencyId = t1.CurrencyId,
        //                      StatusName = leftt1t5.Name,
        //                      Comments = t1.Comments,
        //                      GrossAmount = t1.GrossAmount,
        //                      NetAmount = t1.NetAmount,
        //                      ExchangeRate = t1.ExchangeRate,
        //                      TaxAmount = t1.TaxAmount,
        //                      DiscountPercent = t1.DiscountPercent,
        //                      DiscountAmount = t1.DiscountAmount,
        //                  }).ToList();
        //    return result;
        //}
        [ResponseType(typeof(SaleInvoice))]
        [Route("Post")]
        public IHttpActionResult Post(SaleInvoice objModel)
        {
            try
            {
                objModel.CompanyId = 1;
                objModel.IsActive = true;
                objModel.IsDeleted = false;
                objModel.SettingStatusId = 1;

                var pbj = db.SaleInvoices.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleInvoiceId).FirstOrDefault();
                int primaryKey = pbj == null ? 1 : pbj.SaleInvoiceId + 1;
                using (var dbContext = new EntityContext())
                {
                    var parent = new SaleInvoice
                    {
                        SaleInvoiceId = primaryKey,
                        CustomerId = objModel.CustomerId,
                        CompanyId = objModel.CompanyId,
                        Number = objModel.Number,
                        InvoiceDate = objModel.InvoiceDate,
                        InvoiceDueDate = objModel.InvoiceDueDate,
                        Reference = objModel.Reference,
                        GroupBranchId = objModel.GroupBranchId,
                        SalePersonId = objModel.SalePersonId,
                        CurrencyId = objModel.CurrencyId,
                        SettingStatusId = objModel.SettingStatusId,
                        IsActive = objModel.IsActive,
                        IsDeleted = objModel.IsDeleted,
                        Comments = objModel.Comments,
                        GrossAmount = objModel.GrossAmount,
                        NetAmount = objModel.NetAmount,
                        ExchangeRate = objModel.ExchangeRate,
                        TaxAmount = objModel.TaxAmount,
                        DiscountPercent = objModel.DiscountPercent,
                        DiscountAmount = objModel.DiscountAmount,
                    };
                    dbContext.SaleInvoices.Add(parent);
                    dbContext.SaveChanges();

                    int cnt = 1;
                    foreach (var item in objModel.saleInvoiceDetail)
                    {
                        var child = new SaleInvoiceDetail
                        {
                            SaleInvoiceId = primaryKey,
                            CompanyId = objModel.CompanyId,
                            SaleInvoiceDetailId = cnt,
                            IsDeleted = false,
                            GeneralWarehouseId = item.GeneralWarehouseId,
                            ProductId = item.ProductId,
                            Description = item.Description,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            DiscountAmount = item.DiscountAmount,
                            NetAmount = item.NetAmount,
                        };
                        cnt++;
                        dbContext.SaleInvoiceDetails.Add(child);
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
        public IHttpActionResult PutSaleInvoice(int id, SaleInvoice objModel)
        {
            objModel.CompanyId = 1;
            objModel.IsActive = true;
            objModel.IsDeleted = false;
            objModel.SettingStatusId = 1;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleInvoiceId)
            {
                return BadRequest();
            }
            db.SaleInvoiceDetails.RemoveRange(db.SaleInvoiceDetails.Where(w => w.SaleInvoiceId == id));
            db.SaveChanges();
            int cnt = 1;
            foreach (SaleInvoiceDetail item in objModel.saleInvoiceDetail)
            {
                item.SaleInvoiceId = id;
                item.SaleInvoiceDetailId = cnt;
                item.CompanyId = objModel.CompanyId;
                item.IsDeleted = false;
                cnt++;
            }

            if (objModel.saleInvoiceDetail.Count > 0)
            {
                db.SaleInvoiceDetails.AddRange(objModel.saleInvoiceDetail);
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
        [ResponseType(typeof(SaleInvoice))]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            SaleInvoice objModel = db.SaleInvoices.Where(w => w.SaleInvoiceId == id).FirstOrDefault();
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
            var obj = db.SaleInvoices.OrderByDescending(x => x.SaleInvoiceId).Take(1);
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

        [Route("GetSaleInvoicee")]
        public IQueryable<SaleInvoice> GetSaleInvoicee(int id)
        {
            var result = db.SaleInvoices.Where(w => w.SaleInvoiceId == id && w.IsDeleted == false);
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
            return db.SaleInvoices.Count(e => e.SalePersonId == id) > 0;
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
        private bool SaleInvoiceExists(int id)
        {
            return db.SaleInvoices.Count(e => e.SaleInvoiceId == id) > 0;
        }
    }
}
