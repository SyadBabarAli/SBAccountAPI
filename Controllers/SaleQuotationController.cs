using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
    [RoutePrefix("api/SaleQuotation")]
    public class SaleQuotationController : BaseController
    {
        private EntityContext db = new EntityContext();

        [Route("GetSaleQuotationes")]
        [HttpGet]
        public List<SaleQuotationModel> GetSaleQuotationes()
        {
            var result = (from t1 in db.SaleQuotations
                          join t2 in db.ListCustomers
                          on
                          t1.CustomerId equals t2.CustomerId
                          into t1t2
                          from leftt1t2 in t1t2.DefaultIfEmpty()

                          join t3 in db.GroupBranches
                          on
                          t1.BranchId equals t3.GroupBranchId
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
                          select new SaleQuotationModel
                          {
                              SaleQuotationId = t1.SaleQuotationId,
                              Number = t1.Number,
                              DateSale = t1.DateSale,
                              CustomerId = t1.CustomerId,
                              CustomerName = leftt1t2.CustomerName,
                              BranchId = t1.BranchId,
                              BranchName = leftt1t3.Name,
                              SalePersonId = t1.SalePersonId,
                              SalePersonName = leftt1t4.SalePersonName,
                              ExpiryDate = t1.ExpiryDate,
                              SettingStatusId = t1.SettingStatusId,
                              Reference = t1.Reference,
                              CurrencyId = t1.CurrencyId,
                              StatusName = leftt1t5.Name,
                              Term = t1.Term,
                              GrossAmount = t1.GrossAmount,
                              NetAmount = t1.NetAmount,
                              ExchangeRate = t1.ExchangeRate,
                              TaxAmount = t1.TaxAmount,
                              DiscountPercent = t1.DiscountPercent,
                              DiscountAmount = t1.DiscountAmount,
                          }).ToList();
            return result;
        }

        [ResponseType(typeof(SaleQuotation))]
        [Route("Post")]
        public IHttpActionResult Post(SaleQuotation objModel)
        {
            try
            {
                var pbj = db.SaleQuotations.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleQuotationId).FirstOrDefault();
                int primaryKey = pbj == null ? 1 : pbj.SaleQuotationId + 1;
                using (var dbContext = new EntityContext())
                {
                    var parent = new SaleQuotation
                    {
                        SaleQuotationId = primaryKey,
                        CustomerId = objModel.CustomerId,
                        CompanyId = objModel.CompanyId,
                        Number = objModel.Number,
                        DateSale = objModel.DateSale,
                        ExpiryDate = objModel.ExpiryDate,
                        Reference = objModel.Reference,
                        BranchId = objModel.BranchId,
                        SalePersonId = objModel.SalePersonId,
                        CurrencyId = objModel.CurrencyId,
                        SettingStatusId = objModel.SettingStatusId,
                        IsActive = true,
                        IsDeleted = false,
                        Term = objModel.Term,

                        GrossAmount = objModel.GrossAmount,
                        NetAmount = objModel.NetAmount,
                        ExchangeRate = objModel.ExchangeRate,
                        TaxAmount = objModel.TaxAmount,
                        DiscountPercent = objModel.DiscountPercent,
                        DiscountAmount = objModel.DiscountAmount,
                    };
                    dbContext.SaleQuotations.Add(parent);
                    dbContext.SaveChanges();

                    int cnt = 1;
                    foreach (var item in objModel.saleQuotationDetail)
                    {
                        var child = new SaleQuotationDetail
                        {
                            SaleQuotationId = primaryKey,
                            CompanyId = objModel.CompanyId,
                            SaleQuotationDetailId = cnt,
                            ProductId = item.ProductId,
                            Description = item.Description,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Discount = item.Discount,
                            Amount = item.Amount,
                        };
                        cnt++;
                        dbContext.SaleQuotationDetails.Add(child);
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (DbUpdateException e)
            {
                throw; //couldn’t handle that error, so rethrow
            }

            return Ok("Save");
        }
        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutSaleQuotation(int id, SaleQuotation objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleQuotationId)
            {
                return BadRequest();
            }

            db.SaleQuotationDetails.RemoveRange(db.SaleQuotationDetails.Where(w => w.SaleQuotationId == id));
            db.SaveChanges();
            int cnt = 1;
            foreach (SaleQuotationDetail item in objModel.saleQuotationDetail)
            {
                item.SaleQuotationId = id;
                item.SaleQuotationDetailId = cnt;
                item.CompanyId = objModel.CompanyId;
                cnt++;
            }

            if (objModel.saleQuotationDetail.Count > 0)
            {
                db.SaleQuotationDetails.AddRange(objModel.saleQuotationDetail);
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

        [ResponseType(typeof(SaleQuotation))]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            SaleQuotation objModel = db.SaleQuotations.Where(w => w.SaleQuotationId == id).FirstOrDefault();
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

        [Route("GetSaleQuotationDetails")]
        [HttpGet]
        public List<SaleQuotationDetailModel> GetSaleQuotationDetails(int id)
        {
            var result = (from t1 in db.SaleQuotationDetails
                          join t2 in db.ListProducts
                          on
                          t1.ProductId equals t2.ProductId
                          into t1t2
                          from leftt1t2 in t1t2.DefaultIfEmpty()

                          where t1.SaleQuotationId == id
                          && t1.IsDeleted == false
                          select new SaleQuotationDetailModel
                          {
                              SaleQuotationId = t1.SaleQuotationId,
                              CompanyId = t1.CompanyId,
                              SaleQuotationDetailId = t1.SaleQuotationDetailId,
                              ProductId = t1.ProductId,
                              ProductName = leftt1t2.Name,
                              Quantity = t1.Quantity,
                              Price = t1.Price,
                              Discount = t1.Discount,
                              Amount = t1.Amount,
                              Description = t1.Description,
                              IsDeleted = t1.IsDeleted,
                          }).ToList();
            return result;
        }
        [Route("GetProposedNumber")]
        public string GetProposedNumber()
        {
            SupportingFunctions objFN = new SupportingFunctions();
            var obj = db.SaleQuotations.OrderByDescending(x => x.SaleQuotationId).Take(1);
            return objFN.GenerateProposedNumber(obj.FirstOrDefault().Number); ;
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

        [Route("GetSaleQuotatione")]
        public IQueryable<SaleQuotation> GetSaleQuotatione(int id)
        {
            var result = db.SaleQuotations.Where(w => w.SaleQuotationId == id && w.IsDeleted == false);
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
            return db.SaleQuotations.Count(e => e.SalePersonId == id) > 0;
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
        private bool SaleQuotationExists(int id)
        {
            return db.SaleQuotations.Count(e => e.SaleQuotationId == id) > 0;
        }
    }
}
