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
    [RoutePrefix("api/GeneralTax")]
    public class GeneralTaxController : BaseController
    {
        private EntityContext db = new EntityContext();

        [Route("GetGeneralTaxs")]
        public IQueryable<GeneralTax> GetGeneralTaxs()
        {
            var result = db.GeneralTaxes.Where(w => w.IsDeleted == false);
            return result;
        }

        [Route("GetSettingAccount")]
        [HttpGet]
        public IQueryable<ComboBox> SettingAccount()
        {
            var result = (from t1 in db.SettingAccounts
                          where t1.IsActive == true
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.SettingAccountId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutGeneralTax(int id, GeneralTax objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.GeneralTaxId)
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
                if (!GeneralTaxExists(id))
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

        [ResponseType(typeof(GeneralTax))]
        [Route("Post")]
        public IHttpActionResult Post(GeneralTax objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.GeneralTaxes.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.GeneralTaxId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.GeneralTaxId;

            objModel.CompanyId = 1;
            objModel.GeneralTaxId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.GeneralTaxes.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(GeneralTax))]
        [Route("Delete")]
        public IHttpActionResult DeleteGeneralTax(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            GeneralTax objModel = db.GeneralTaxes.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.GeneralTaxes.Remove(objModel);
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

        private bool GeneralTaxExists(int id)
        {
            return db.GeneralTaxes.Count(e => e.GeneralTaxId == id) > 0;
        }
    }
}