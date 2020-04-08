using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SBAccountAPI.Context;
using SBAccountAPI.ModelCustom;
using SBAccountAPI.Models;

namespace SBAccountAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/ListProduct")]
    public class ListProductController : BaseController
    {
        private EntityContext db = new EntityContext();

        [Route("GetListProducts")]
        public List<ListProductModel> GetListProducts()
        {
            //var result = db.ListProducts.Where(w => w.IsDeleted == false);

            var result = (from t1 in db.ListProducts
                          join t2 in db.CategoryProducts
                          on
                          t1.CategoryProductId equals t2.CategoryProductId
                          into t1t2
                          from CategoryProduct in t1t2.DefaultIfEmpty()

                          join t3 in db.CategoryBrands
                          on
                          t1.CategoryBrandId equals t3.CategoryBrandId
                          into t1t3
                          from CategoryBrand in t1t3.DefaultIfEmpty()

                          join t4 in db.SettingProductTypes
                          on
                          t1.SettingProductTypeId equals t4.SettingProductTypeId
                          into t1t4
                          from ProductType in t1t4.DefaultIfEmpty()
                          
                          where t1.IsDeleted == false
                          select new ListProductModel
                          {
                              ProductId = t1.ProductId,
                              Code = t1.Code,
                              Name = t1.Name,
                              ProductTypeName = ProductType.Name,
                              CategoryProductName = CategoryProduct.Name,
                              CategoryBrandName = CategoryBrand.Name,
                              SalePrice = t1.SalePrice,

                              CompanyId = t1.CompanyId,
                              Barcode = t1.Barcode,
                              Description = t1.Description,
                              ShortDescription = t1.ShortDescription,
                              Symbol = t1.Symbol,
                              Precision = t1.Precision,
                              FractionalUnit = t1.FractionalUnit,
                              TrackInventory = t1.TrackInventory,
                              InventoryAccountId = t1.InventoryAccountId,
                              IsForSale = t1.IsForSale,
                              SaleAccountId = t1.SaleAccountId,
                              SaleDiscountAccountId = t1.SaleDiscountAccountId,
                              SaleTax = t1.SaleTax,
                              IsForPurchase = t1.IsForPurchase,
                              PurchasePrice = t1.PurchasePrice,
                              ExpenseAccountId = t1.ExpenseAccountId,
                              PurchaseDiscountAccountId = t1.PurchaseDiscountAccountId,
                              PurchaseTax = t1.PurchaseTax,
                              MaximumRetailPrice = t1.MaximumRetailPrice,
                              IsMRPExclusiveTax = t1.IsMRPExclusiveTax,
                              ImageUrl = t1.ImageUrl,
                              Base64imagestring = t1.Base64imagestring,
                              BaseProductId = t1.BaseProductId,
                              ConversionFactor = t1.ConversionFactor,
                              HasCalculationfield = t1.HasCalculationfield,
                              AverageCost = t1.AverageCost,
                              HasBatch = t1.HasBatch,
                              IsOpening = t1.IsOpening,
                              HasSerialnumber = t1.HasSerialnumber,
                              MinimumStockLevel = t1.MinimumStockLevel,
                              CategoryProductId = t1.CategoryProductId,
                              UnitOfMeasurementId = t1.UnitOfMeasurementId,
                              CategoryProductPrincipleId = t1.CategoryProductPrincipleId,
                              CategoryBrandId = t1.CategoryBrandId,
                              HSCodeId = t1.HSCodeId,
                              SettingProductTypeId = t1.SettingProductTypeId,
                              IsDeleted = t1.IsDeleted,
                              IsActive = t1.IsActive,
                              Created = t1.Created,
                              CreatedBy = t1.CreatedBy,
                              Modified = t1.Modified,
                              ModifiedBy = t1.ModifiedBy,

                          }).ToList();
            return result;
        }
        [ResponseType(typeof(ListProduct))]
        [Route("Post")]
        public IHttpActionResult Post(ListProduct objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            objModel.CompanyId = 1;
            var pbj = db.ListProducts.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.ProductId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.ProductId+1;

            objModel.CompanyId = 1;
            objModel.ProductId = primaryKey ;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.ListProducts.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }
        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutListProduct(int id, ListProduct objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.ProductId)
            {
                return BadRequest();
            }


            db.Entry(objModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListProductExists(id))
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
        [ResponseType(typeof(ListProduct))]
        [Route("Delete")]
        public IHttpActionResult DeleteListProduct(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            ListProduct objModel = db.ListProducts.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.ListProducts.Remove(objModel);
            db.SaveChanges();

            return Ok(objModel);
        }

        [Route("GetSettingUnitOfMeasurments")]
        [HttpGet]
        public IQueryable<ComboBox> GetSettingUnitOfMeasurments()
        {
            var result = (from t1 in db.SettingUnitOfMeasurments
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.UnitOfMeasurementId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetSettingInventoryAccounts")]
        [HttpGet]
        public IQueryable<ComboBox> GetSettingInventoryAccounts()
        {
            var result = (from t1 in db.SettingInventoryAccounts
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.SettingInventoryAccountId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetSettingAccounts")]
        [HttpGet]
        public IQueryable<ComboBox> GetSettingAccounts()
        {
            var result = (from t1 in db.SettingAccounts
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.SettingAccountId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetSettingSaleAccounts")]
        [HttpGet]
        public IQueryable<ComboBox> GetSettingSaleAccounts()
        {
            var result = (from t1 in db.SettingSaleAccounts
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.SettingSaleAccountId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetCategoryProducts")]
        [HttpGet]
        public IQueryable<ComboBox> GetCategoryProducts()
        {
            var result = (from t1 in db.CategoryProducts
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.CategoryProductId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetCategoryBrands")]
        [HttpGet]
        public IQueryable<ComboBox> GetCategoryBrands()
        {
            var result = (from t1 in db.CategoryBrands
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.CategoryBrandId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [Route("GetCategoryProductPrinciples")]
        [HttpGet]
        public IQueryable<ComboBox> GetCategoryProductPrinciples()
        {
            var result = (from t1 in db.CategoryProductPrinciples
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.CategoryProductPrincipleId,
                              text = t1.Name
                          }).AsQueryable();

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
        private bool ListProductExists(int id)
        {
            return db.ListProducts.Count(e => e.ProductId == id) > 0;
        }
    }
}