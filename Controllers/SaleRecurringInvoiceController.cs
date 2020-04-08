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
    [RoutePrefix("api/SaleRecurringInvoice")]
    public class SaleRecurringInvoiceController : BaseController
    {
        private readonly EntityContext db = new EntityContext();

        [HttpPut]
        [Route("GetSaleRecurringInvoice")]
        public IHttpActionResult GetSaleRecurringInvoice(PagedResult<SaleRecurringInvoiceModel> pagedResult)
        {
            IQueryable<SaleRecurringInvoiceModel> source = (from t1 in db.SaleRecurringInvoices
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
                                                   select new SaleRecurringInvoiceModel
                                                   {
                                                       SaleRecurringInvoiceId = t1.SaleRecurringInvoiceId,
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
                                                       StartDate = t1.StartDate,
                                                       EndDate = t1.EndDate,
                                                       SettingFrequencyId = t1.SettingFrequencyId
                                                   });
            var result = source.GetTable<SaleRecurringInvoiceModel>(pagedResult);
            return Ok(result);
        }

        [HttpGet]
        public SaleRecurringInvoiceModel GetSaleRecurringInvoiceDetails(int id)
        {
            SaleRecurringInvoiceModel objModel = new SaleRecurringInvoiceModel();

            var child = (from t1 in db.SaleRecurringInvoiceDetails
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
                         t1.SaleRecurringInvoiceId == id
                         && t1.IsDeleted == false
                         select new SaleRecurringInvoiceDetailModel
                         {
                             SaleRecurringInvoiceId = t1.SaleRecurringInvoiceId,
                             CompanyId = t1.CompanyId,
                             SaleRecurringInvoiceDetailId = t1.SaleRecurringInvoiceDetailId,
                             ProductId = t1.ProductId,
                             ProductName = leftt1t2.Name,

                             
                             //GeneralWarehouseId = t1.GeneralWarehouseId,
                             //WarehouseName = leftt1t2.Name,

                             Quantity = t1.Quantity,
                             Price = t1.Price,
                             DiscountAmount = t1.DiscountAmount,
                             NetAmount = t1.NetAmount,
                             Description = t1.Description,
                             IsDeleted = t1.IsDeleted,
                         }).ToList();
                                        
            var master = (from t1 in db.SaleRecurringInvoices
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
                           t1.SaleRecurringInvoiceId == id
                           &&
                          t1.IsDeleted == false
                          select new SaleRecurringInvoiceModel
                          {
                              GroupBranchId = t1.GroupBranchId,
                              SaleRecurringInvoiceId = t1.SaleRecurringInvoiceId,
                              Number = t1.Number,
                              InvoiceDate = t1.InvoiceDate,
                              CustomerId = t1.CustomerId,
                              CustomerName = leftt1t2.CustomerName,
                              BranchName = leftt1t3.Name,
                              SalePersonId = t1.SalePersonId,
                              SalePersonName = leftt1t4.SalePersonName,
                              InvoiceDueDate = t1.InvoiceDueDate,
                              SettingStatusId = t1.SettingStatusId,
                              SettingFrequencyId = t1.SettingFrequencyId,
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
                              StartDate = t1.StartDate,
                              EndDate = t1.EndDate,
                          }).ToList();
            if (master.Count == 0)
            {
                objModel.settingFrequencyModel = db.SettingFrequences.ToList();
                objModel.settingStatusModel = db.SettingStatuses.ToList();
                objModel.SaleRecurringInvoiceDetailModel = child;
                return objModel;
            }
            else
            {
                master.FirstOrDefault().settingFrequencyModel = db.SettingFrequences.ToList();
                master.FirstOrDefault().settingStatusModel = db.SettingStatuses.ToList();
                master.FirstOrDefault().SaleRecurringInvoiceDetailModel = child;
                return master.FirstOrDefault();
            }
        }

        [ResponseType(typeof(SaleRecurringInvoice))]
        [Route("Post")]
        public IHttpActionResult Post(SaleRecurringInvoice objModel)
        {
            try
            {
                objModel.CompanyId = 1;
                objModel.IsActive = true;
                objModel.IsDeleted = false;

                var pbj = db.SaleRecurringInvoices.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleRecurringInvoiceId).FirstOrDefault();
                int primaryKey = pbj == null ? 1 : pbj.SaleRecurringInvoiceId + 1;
                using (var dbContext = new EntityContext())
                {
                    var parent = new SaleRecurringInvoice
                    {
                        SaleRecurringInvoiceId = primaryKey,
                        CustomerId = objModel.CustomerId,
                        CompanyId = objModel.CompanyId,
                        Number = objModel.Number,
                        InvoiceDate = objModel.InvoiceDate,
                        InvoiceDueDate = objModel.InvoiceDueDate,
                        StartDate = objModel.StartDate,
                        EndDate = objModel.EndDate,
                        Reference = objModel.Reference,
                        SettingStatusId = objModel.SettingStatusId,
                        SettingFrequencyId = objModel.SettingFrequencyId,
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
                    dbContext.SaleRecurringInvoices.Add(parent);
                    dbContext.SaveChanges();

                    int cnt = 1;
                    foreach (var item in objModel.SaleRecurringInvoiceDetail)
                    {
                        var child = new SaleRecurringInvoiceDetail
                        {
                            SaleRecurringInvoiceId = primaryKey,
                            CompanyId = objModel.CompanyId,
                            SaleRecurringInvoiceDetailId = cnt,
                            IsDeleted = false,
                            ProductId = item.ProductId,
                            Description = item.Description,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            DiscountAmount = item.DiscountAmount,
                            NetAmount = item.NetAmount,
                        };
                        cnt++;
                        dbContext.SaleRecurringInvoiceDetails.Add(child);
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
        public IHttpActionResult PutSaleRecurringInvoice(int id, SaleRecurringInvoice objModel)
        {
            objModel.CompanyId = 1;
            objModel.IsActive = true;
            objModel.IsDeleted = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleRecurringInvoiceId)
            {
                return BadRequest();
            }
            db.SaleRecurringInvoiceDetails.RemoveRange(db.SaleRecurringInvoiceDetails.Where(w => w.SaleRecurringInvoiceId == id));
            db.SaveChanges();
            int cnt = 1;
            foreach (SaleRecurringInvoiceDetail item in objModel.SaleRecurringInvoiceDetail)
            {
                item.SaleRecurringInvoiceId = id;
                item.SaleRecurringInvoiceDetailId = cnt;
                item.CompanyId = objModel.CompanyId;
                item.IsDeleted = false;
                cnt++;
            }

            if (objModel.SaleRecurringInvoiceDetail.Count > 0)
            {
                db.SaleRecurringInvoiceDetails.AddRange(objModel.SaleRecurringInvoiceDetail);
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
        [ResponseType(typeof(SaleRecurringInvoice))]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            SaleRecurringInvoice objModel = db.SaleRecurringInvoices.Where(w => w.SaleRecurringInvoiceId == id).FirstOrDefault();
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
            var obj = db.SaleRecurringInvoices.OrderByDescending(x => x.SaleRecurringInvoiceId).Take(1);
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

        [Route("GetSaleRecurringInvoicee")]
        public IQueryable<SaleRecurringInvoice> GetSaleRecurringInvoicee(int id)
        {
            var result = db.SaleRecurringInvoices.Where(w => w.SaleRecurringInvoiceId == id && w.IsDeleted == false);
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
            return db.SaleRecurringInvoices.Count(e => e.SalePersonId == id) > 0;
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
        private bool SaleRecurringInvoiceExists(int id)
        {
            return db.SaleRecurringInvoices.Count(e => e.SaleRecurringInvoiceId == id) > 0;
        }
    }
}
