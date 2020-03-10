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
    [RoutePrefix("api/CategoryDesignation")]
    public class CategoryDesignationController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetCategoryDesignations")]
        public IQueryable<CategoryDesignation> GetCategoryDesignations()
        {
            var result = db.CategoryDesignations.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutCategoryDesignation(int id, CategoryDesignation objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.CategoryDesignationId)
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
                if (!CategoryDesignationExists(id))
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

        [ResponseType(typeof(CategoryDesignation))]
        [Route("Post")]
        public IHttpActionResult Post(CategoryDesignation objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.CategoryDesignations.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.CategoryDesignationId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.CategoryDesignationId;

            objModel.CompanyId = 1;
            objModel.CategoryDesignationId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.CategoryDesignations.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(CategoryDesignation))]
        [Route("Delete")]
        public IHttpActionResult DeleteCategoryDesignation(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            CategoryDesignation objModel = db.CategoryDesignations.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.CategoryDesignations.Remove(objModel);
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

        private bool CategoryDesignationExists(int id)
        {
            return db.CategoryDesignations.Count(e => e.CategoryDesignationId == id) > 0;
        }
    }
}