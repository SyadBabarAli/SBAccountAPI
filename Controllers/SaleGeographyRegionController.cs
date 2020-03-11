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
    [RoutePrefix("api/SaleGeographyRegion")]
    public class SaleGeographyRegionController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetSaleGeographyRegions")]
        public IQueryable<SaleGeographyRegion> GetSaleGeographyRegions()
        {
            var result = db.SaleGeographyRegions.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutSaleGeographyRegion(int id, SaleGeographyRegion objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleGeographyRegionId)
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
                if (!SaleGeographyRegionExists(id))
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

        [ResponseType(typeof(SaleGeographyRegion))]
        [Route("Post")]
        public IHttpActionResult Post(SaleGeographyRegion objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.SaleGeographyRegions.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleGeographyRegionId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.SaleGeographyRegionId;

            objModel.CompanyId = 1;
            objModel.SaleGeographyRegionId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.SaleGeographyRegions.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(SaleGeographyRegion))]
        [Route("Delete")]
        public IHttpActionResult DeleteSaleGeographyRegion(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            SaleGeographyRegion objModel = db.SaleGeographyRegions.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.SaleGeographyRegions.Remove(objModel);
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

        private bool SaleGeographyRegionExists(int id)
        {
            return db.SaleGeographyRegions.Count(e => e.SaleGeographyRegionId == id) > 0;
        }
    }
}