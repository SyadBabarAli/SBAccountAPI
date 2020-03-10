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
    [RoutePrefix("api/ListVendors")]
    public class ListVendorsController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetListVendors")]
        public IQueryable<ListVendor> GetListVendors()
        {
            var result = db.ListVendors.Where(w => w.IsDeleted == false);
            return result;
        }

        [Route("GetVendorCategory")]
        public IQueryable<ComboBox> GetVendorCategory()
        {
            var result = (from t1 in db.CategoryVendors
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.VendorId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }
        [Route("GetCurrencies")]
        public IQueryable<ComboBox> GetCurrencies()
        {
            var result = (from t1 in db.SettingCurrencies
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.CurrencyId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }
        [ResponseType(typeof(ListVendor))]
        [Route("GetListVendor")]
        public IHttpActionResult GetListVendor(int id)
        {
            ListVendor objModel = db.ListVendors.Find(id);
            if (objModel == null)
            {
                return NotFound();
            }

            return Ok(objModel);
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutListVendor(int id, ListVendor objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.VendorId)
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
                if (!ListVendorExists(id))
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

        [ResponseType(typeof(ListVendor))]
        [Route("Post")]
        public IHttpActionResult Post(ListVendor objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.ListVendors.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.VendorId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.VendorId;

            objModel.CompanyId = 1;
            objModel.VendorId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.ListVendors.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(ListVendor))]
        [Route("Delete")]
        public IHttpActionResult DeleteListVendor(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            ListVendor objModel = db.ListVendors.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.ListVendors.Remove(objModel);
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

        private bool ListVendorExists(int id)
        {
            return db.ListVendors.Count(e => e.VendorId == id) > 0;
        }
    }
}