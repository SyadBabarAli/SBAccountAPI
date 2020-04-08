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
    [RoutePrefix("api/CategoryDepartment")]
    public class CategoryDepartmentController : BaseController
    {
        EntityContext db = new EntityContext();


        [Route("GetCategoryDepartments")]
        public IQueryable<CategoryDepartment> GetCategoryDepartments()
        {
            var result = db.CategoryDepartments.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutCategoryDepartment(int id, CategoryDepartment objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.CategoryDepartmentId)
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
                if (!CategoryDepartmentExists(id))
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

        [ResponseType(typeof(CategoryDepartment))]
        [Route("Post")]
        public IHttpActionResult Post(CategoryDepartment objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.CategoryDepartments.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.CategoryDepartmentId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.CategoryDepartmentId;

            objModel.CompanyId = 1;
            objModel.CategoryDepartmentId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.CategoryDepartments.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(CategoryDepartment))]
        [Route("Delete")]
        public IHttpActionResult DeleteCategoryDepartment(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            CategoryDepartment objModel = db.CategoryDepartments.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.CategoryDepartments.Remove(objModel);
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

        private bool CategoryDepartmentExists(int id)
        {
            return db.CategoryDepartments.Count(e => e.CategoryDepartmentId == id) > 0;
        }
    }
}