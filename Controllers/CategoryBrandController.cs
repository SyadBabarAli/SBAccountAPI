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
    [RoutePrefix("api/CategoryBrand")]
    public class CategoryBrandController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetCategoryBrands")]
        public IQueryable<CategoryBrand> GetCategoryBrands()
        {
            var result = db.CategoryBrands.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutCategoryBrand(int id, CategoryBrand objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.CategoryBrandId)
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
                if (!CategoryBrandExists(id))
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

        [ResponseType(typeof(CategoryBrand))]
        [Route("Post")]
        public IHttpActionResult Post(CategoryBrand objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.CategoryBrands.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.CategoryBrandId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.CategoryBrandId;

            objModel.CompanyId = 1;
            objModel.CategoryBrandId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.CategoryBrands.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(CategoryBrand))]
        [Route("Delete")]
        public IHttpActionResult DeleteCategoryBrand(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            CategoryBrand objModel = db.CategoryBrands.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.CategoryBrands.Remove(objModel);
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

        private bool CategoryBrandExists(int id)
        {
            return db.CategoryBrands.Count(e => e.CategoryBrandId == id) > 0;
        }
    }
}