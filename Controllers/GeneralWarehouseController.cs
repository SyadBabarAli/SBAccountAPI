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
    [RoutePrefix("api/GeneralWarehouse")]
    public class GeneralWarehouseController : BaseController
    {
        private EntityContext db = new EntityContext();

        [Route("GetGeneralWarehouses")]
        public IQueryable<GeneralWarehouse> GetGeneralWarehouses()
        {
            var result = db.GeneralWarehouses.Where(w => w.IsDeleted == false);
            return result;
        }

        [Route("GetSettingCountries")]
        [HttpGet]
        public IQueryable<ComboBox> SettingCountries()
        {
            var result = (from t1 in db.SettingCountries
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.CountryId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutGeneralWarehouse(int id, GeneralWarehouse objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.GeneralWarehouseId)
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
                if (!GeneralWarehouseExists(id))
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

        [ResponseType(typeof(GeneralWarehouse))]
        [Route("Post")]
        public IHttpActionResult Post(GeneralWarehouse objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.GeneralWarehouses.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.GeneralWarehouseId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.GeneralWarehouseId;

            objModel.CompanyId = 1;
            objModel.GeneralWarehouseId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.GeneralWarehouses.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(GeneralWarehouse))]
        [Route("Delete")]
        public IHttpActionResult DeleteGeneralWarehouse(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            GeneralWarehouse objModel = db.GeneralWarehouses.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.GeneralWarehouses.Remove(objModel);
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

        private bool GeneralWarehouseExists(int id)
        {
            return db.GeneralWarehouses.Count(e => e.GeneralWarehouseId == id) > 0;
        }
    }
}