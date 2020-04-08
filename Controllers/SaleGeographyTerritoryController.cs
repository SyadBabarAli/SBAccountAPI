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
    [RoutePrefix("api/SaleGeographyTerritory")]
    public class SaleGeographyTerritoryController : BaseController
    {
        private EntityContext db = new EntityContext();

        [Route("GetSaleGeographyTerritories")]
        public IQueryable<SaleGeographyTerritory> GetSaleGeographyTerritories()
        {
            var result = db.SaleGeographyTerritories.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutSaleGeographyTerritory(int id, SaleGeographyTerritory objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleGeographyTerritoryId)
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
                if (!SaleGeographyTerritoryExists(id))
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

        [ResponseType(typeof(SaleGeographyTerritory))]
        [Route("Post")]
        public IHttpActionResult Post(SaleGeographyTerritory objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.SaleGeographyTerritories.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleGeographyTerritoryId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.SaleGeographyTerritoryId;

            objModel.CompanyId = 1;
            objModel.SaleGeographyTerritoryId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.SaleGeographyTerritories.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(SaleGeographyTerritory))]
        [Route("Delete")]
        public IHttpActionResult DeleteSaleGeographyTerritory(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            SaleGeographyTerritory objModel = db.SaleGeographyTerritories.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.SaleGeographyTerritories.Remove(objModel);
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

        private bool SaleGeographyTerritoryExists(int id)
        {
            return db.SaleGeographyTerritories.Count(e => e.SaleGeographyTerritoryId == id) > 0;
        }
    }
}