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
    [RoutePrefix("api/ListCustomers")]
    public class ListCustomersController : BaseController
    {
        private EntityContext db = new EntityContext();

        [Route("GetListCustomers")]
        public IQueryable<ListCustomer> GetListCustomers()
        {
            var result = db.ListCustomers.Where(w => w.IsDeleted == false);
            return result;
        }

        [Route("GetCustomerCategory")]
        public IQueryable<ComboBox> GetCustomerCategory()
        {
            var result = (from t1 in db.SettingCustomerCategories
                          orderby t1.CustomerCategory ascending
                          select new ComboBox
                          {
                              value = t1.CustomerCategoryId,
                              text = t1.CustomerCategory
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
        [ResponseType(typeof(ListCustomer))]
        [Route("GetListCustomer")]
        public IHttpActionResult GetListCustomer(int id)
        {
            ListCustomer objModel = db.ListCustomers.Find(id);
            if (objModel == null)
            {
                return NotFound();
            }

            return Ok(objModel);
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutListCustomer(int id, ListCustomer objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.CustomerId)
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
                if (!ListCustomerExists(id))
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

        [ResponseType(typeof(ListCustomer))]
        [Route("Post")]
        public IHttpActionResult Post(ListCustomer objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.ListCustomers.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.CustomerId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.CustomerId;

            objModel.CustomerId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.ListCustomers.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(ListCustomer))]
        [Route("Delete")]
        public IHttpActionResult DeleteListCustomer(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            ListCustomer objModel = db.ListCustomers.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.ListCustomers.Remove(objModel);
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

        private bool ListCustomerExists(int id)
        {
            return db.ListCustomers.Count(e => e.CustomerId == id) > 0;
        }
    }
}