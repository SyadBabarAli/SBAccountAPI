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
    [RoutePrefix("api/SaleGeographySubArea")]
    public class SaleGeographySubAreaController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetSaleGeographySubAreas")]
        public IQueryable<SaleGeographySubArea> GetSaleGeographySubAreas()
        {
            var result = db.SaleGeographySubAreas.Where(w => w.IsDeleted == false);
            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutSaleGeographySubArea(int id, SaleGeographySubArea objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleGeographySubAreaId)
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
                if (!SaleGeographySubAreaExists(id))
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

        [ResponseType(typeof(SaleGeographySubArea))]
        [Route("Post")]
        public IHttpActionResult Post(SaleGeographySubArea objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.SaleGeographySubAreas.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleGeographySubAreaId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.SaleGeographySubAreaId;

            objModel.CompanyId = 1;
            objModel.SaleGeographySubAreaId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.SaleGeographySubAreas.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(SaleGeographySubArea))]
        [Route("Delete")]
        public IHttpActionResult DeleteSaleGeographySubArea(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            SaleGeographySubArea objModel = db.SaleGeographySubAreas.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.SaleGeographySubAreas.Remove(objModel);
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

        private bool SaleGeographySubAreaExists(int id)
        {
            return db.SaleGeographySubAreas.Count(e => e.SaleGeographySubAreaId == id) > 0;
        }
    }
}