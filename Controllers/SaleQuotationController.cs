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
    [RoutePrefix("api/GroupBranch")]
    public class SaleQuotationController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetGroupBranches")]
        public IQueryable<GroupBranch> GetGroupBranches()
        {
            var result = db.GroupBranches.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutGroupBranch(int id, GroupBranch objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.GroupBranchId)
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
                if (!GroupBranchExists(id))
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

        [ResponseType(typeof(GroupBranch))]
        [Route("Post")]
        public IHttpActionResult Post(GroupBranch objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.GroupBranches.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.GroupBranchId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.GroupBranchId;

            objModel.CompanyId = 1;
            objModel.GroupBranchId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.GroupBranches.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(GroupBranch))]
        [Route("Delete")]
        public IHttpActionResult DeleteGroupBranch(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            GroupBranch objModel = db.GroupBranches.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.GroupBranches.Remove(objModel);
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

        private bool GroupBranchExists(int id)
        {
            return db.GroupBranches.Count(e => e.GroupBranchId == id) > 0;
        }
    }
}