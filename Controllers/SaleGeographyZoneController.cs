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
    [RoutePrefix("api/SaleGeographyZone")]
    public class SaleGeographyZoneController : BaseController
    {
        private EntityContext db = new EntityContext();

        [Route("GetSaleGeographyZones")]
        public IQueryable<SaleGeographyZone> GetSaleGeographyZones()
        {
            var result = db.SaleGeographyZones.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutSaleGeographyZone(int id, SaleGeographyZone objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleGeographyZoneId)
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
                if (!SaleGeographyZoneExists(id))
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

        [ResponseType(typeof(SaleGeographyZone))]
        [Route("Post")]
        public IHttpActionResult Post(SaleGeographyZone objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.SaleGeographyZones.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleGeographyZoneId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.SaleGeographyZoneId;

            objModel.CompanyId = 1;
            objModel.SaleGeographyZoneId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.SaleGeographyZones.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(SaleGeographyZone))]
        [Route("Delete")]
        public IHttpActionResult DeleteSaleGeographyZone(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            SaleGeographyZone objModel = db.SaleGeographyZones.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.SaleGeographyZones.Remove(objModel);
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

        private bool SaleGeographyZoneExists(int id)
        {
            return db.SaleGeographyZones.Count(e => e.SaleGeographyZoneId == id) > 0;
        }
    }
}