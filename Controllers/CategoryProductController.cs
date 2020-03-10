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
    [RoutePrefix("api/CategoryProduct")]
    public class CategoryProductController : BaseController
    {
        private Context.Context db = new Context.Context();


        [Route("GetCategoryProducts")]
        public IQueryable<CategoryProduct> GetCategoryProducts()
        {
            var result = db.CategoryProducts.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutCategoryProduct(int id, CategoryProduct objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.CategoryProductId)
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
                if (!CategoryProductExists(id))
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

        [ResponseType(typeof(CategoryProduct))]
        [Route("Post")]
        public IHttpActionResult Post(CategoryProduct objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.CategoryProducts.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.CategoryProductId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.CategoryProductId;

            objModel.CompanyId = 1;
            objModel.CategoryProductId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.CategoryProducts.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(CategoryProduct))]
        [Route("Delete")]
        public IHttpActionResult DeleteCategoryProduct(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            CategoryProduct objModel = db.CategoryProducts.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.CategoryProducts.Remove(objModel);
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

        private bool CategoryProductExists(int id)
        {
            return db.CategoryProducts.Count(e => e.CategoryProductId == id) > 0;
        }
    }
}