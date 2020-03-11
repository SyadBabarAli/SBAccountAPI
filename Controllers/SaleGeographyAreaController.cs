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
    [RoutePrefix("api/SaleGeographyArea")]
    public class SaleGeographyAreaController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetSaleGeographyAreas")]
        public IQueryable<SaleGeographyArea> GetSaleGeographyAreas()
        {
            var result = db.SaleGeographyAreas.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutSaleGeographyArea(int id, SaleGeographyArea objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleGeographyAreaId)
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
                if (!SaleGeographyAreaExists(id))
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

        [ResponseType(typeof(SaleGeographyArea))]
        [Route("Post")]
        public IHttpActionResult Post(SaleGeographyArea objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.SaleGeographyAreas.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleGeographyAreaId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.SaleGeographyAreaId;

            objModel.CompanyId = 1;
            objModel.SaleGeographyAreaId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.SaleGeographyAreas.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(SaleGeographyArea))]
        [Route("Delete")]
        public IHttpActionResult DeleteSaleGeographyArea(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            SaleGeographyArea objModel = db.SaleGeographyAreas.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.SaleGeographyAreas.Remove(objModel);
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

        private bool SaleGeographyAreaExists(int id)
        {
            return db.SaleGeographyAreas.Count(e => e.SaleGeographyAreaId == id) > 0;
        }
    }
}