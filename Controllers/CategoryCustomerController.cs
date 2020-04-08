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
using SBAccountAPI.Models;

namespace SBAccountAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/CategoryCustomer")]
    public class CategoryCustomerController : BaseController
    {
        private EntityContext db = new EntityContext();

        [Route("GetCategoryCustomers")]
        public IQueryable<CategoryCustomer> GetCategoryCustomers()
        {
            var result = db.CategoryCustomers.Where(w => w.IsDeleted == false);
            return result;
        }

        [Route("GetSettingParentProductCategory")]
        [HttpGet]
        public IQueryable<ComboBox> SettingParentProductCategory()
        {
            var result = (from t1 in db.SettingParentProductCategories
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.ParentProductCategoryId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutCategoryCustomer(int id, CategoryCustomer objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.CategoryCustomerId)
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
                if (!CategoryCustomerExists(id))
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

        [ResponseType(typeof(CategoryCustomer))]
        [Route("Post")]
        public IHttpActionResult Post(CategoryCustomer objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.CategoryCustomers.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.CategoryCustomerId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.CategoryCustomerId;

            objModel.CompanyId = 1;
            objModel.CategoryCustomerId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.CategoryCustomers.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(CategoryCustomer))]
        [Route("Delete")]
        public IHttpActionResult DeleteCategoryCustomer(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            CategoryCustomer objModel = db.CategoryCustomers.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.CategoryCustomers.Remove(objModel);
            db.SaveChanges();

            return Ok(objModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryCustomerExists(int id)
        {
            return db.CategoryCustomers.Count(e => e.CategoryCustomerId == id) > 0;
        }
    }
}