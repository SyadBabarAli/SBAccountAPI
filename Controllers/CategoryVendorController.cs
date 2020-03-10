using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SBAccountAPI.Models;

namespace SBAccountAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/CategoryVendor")]
    public class CategoryVendorController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetCategoryVendors")]
        public IQueryable<CategoryVendor> GetCategoryVendors()
        {
            var result = db.CategoryVendors.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutCategoryVendor(int id, CategoryVendor objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.CategoryVendorId)
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
                if (!CategoryVendorExists(id))
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

        [ResponseType(typeof(CategoryVendor))]
        [Route("Post")]
        public IHttpActionResult Post(CategoryVendor objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.CategoryVendors.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.CategoryVendorId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.CategoryVendorId;

            objModel.CompanyId = 1;
            objModel.CategoryVendorId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.CategoryVendors.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(CategoryVendor))]
        [Route("Delete")]
        public IHttpActionResult DeleteCategoryVendor(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            CategoryVendor objModel = db.CategoryVendors.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.CategoryVendors.Remove(objModel);
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

        private bool CategoryVendorExists(int id)
        {
            return db.CategoryVendors.Count(e => e.CategoryVendorId == id) > 0;
        }
    }
}