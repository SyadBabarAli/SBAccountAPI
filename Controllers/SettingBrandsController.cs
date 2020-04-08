using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net; 
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SBAccountAPI.Context;
using SBAccountAPI.Models;

namespace SBAccountAPI.Controllers
{
    public class SettingBrandsController : ApiController
    {
        private EntityContext db = new EntityContext();

        // GET: api/SettingBrands
        public IQueryable<SettingBrand> GetSettingBrands()
        {
            return db.SettingBrands;
        }

        // GET: api/SettingBrands/5
        [ResponseType(typeof(SettingBrand))]
        public IHttpActionResult GetSettingBrand(int id)
        {
            SettingBrand settingBrand = db.SettingBrands.Find(id);
            if (settingBrand == null)
            {
                return NotFound();
            }

            return Ok(settingBrand);
        }

        // PUT: api/SettingBrands/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSettingBrand(int id, SettingBrand settingBrand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != settingBrand.BrandId)
            {
                return BadRequest();
            }

            db.Entry(settingBrand).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettingBrandExists(id))
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

        // POST: api/SettingBrands
        [ResponseType(typeof(SettingBrand))]
        public IHttpActionResult PostSettingBrand(SettingBrand settingBrand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SettingBrands.Add(settingBrand);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = settingBrand.BrandId }, settingBrand);
        }

        // DELETE: api/SettingBrands/5
        [ResponseType(typeof(SettingBrand))]
        public IHttpActionResult DeleteSettingBrand(int id)
        {
            SettingBrand settingBrand = db.SettingBrands.Find(id);
            if (settingBrand == null)
            {
                return NotFound();
            }

            db.SettingBrands.Remove(settingBrand);
            db.SaveChanges();

            return Ok(settingBrand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SettingBrandExists(int id)
        {
            return db.SettingBrands.Count(e => e.BrandId == id) > 0;
        }
    }
}