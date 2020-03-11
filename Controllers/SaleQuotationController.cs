using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using SBAccountAPI.Models;
using SBAccountAPI.Utility;

namespace SBAccountAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/SaleQuotation")]
    public class SaleQuotationController : BaseController
    {
        private Context.Context db = new Context.Context();

        [Route("GetSaleQuotationes")]
        public IQueryable<SaleQuotation> GetSaleQuotationes()
        {
            var result = db.SaleQuotationes.Where(w => w.IsDeleted == false);
            return result;
        }

        [Route("GetGroupBranches")]
        public IQueryable<ComboBox> GetGroupBranches()
        {
            var result = (from t1 in db.GroupBranches
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.GroupBranchId,
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

        [Route("GetProducts")]
        public IQueryable<ComboBox> GetProducts()
        {
            var result = (from t1 in db.CategoryProducts
                          orderby t1.Name ascending
                          select new ComboBox
                          {
                              value = t1.CategoryProductId,
                              text = t1.Name
                          }).AsQueryable();

            return result;
        }

        [ResponseType(typeof(void))]
        [Route("Put")]
        public IHttpActionResult PutSaleQuotation(int id, SaleQuotation objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != objModel.SaleQuotationId)
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
                if (!SaleQuotationExists(id))
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

        [ResponseType(typeof(SaleQuotation))]
        [Route("Post")]
        public IHttpActionResult Post(SaleQuotation objModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pbj = db.SaleQuotationes.Where(w => w.CompanyId == objModel.CompanyId).OrderByDescending(u => u.SaleQuotationId).FirstOrDefault();
            int primaryKey = pbj == null ? 1 : pbj.SaleQuotationId;

            objModel.CompanyId = 1;
            objModel.SaleQuotationId = primaryKey + 1;
            objModel.IsDeleted = false;
            objModel.IsActive = true;
            db.SaleQuotationes.Add(objModel);
            db.SaveChanges();

            var response = Request.CreateResponse(HttpStatusCode.Created);
            return Ok("Save");

        }

        [ResponseType(typeof(SaleQuotation))]
        [Route("Delete")]
        public IHttpActionResult DeleteSaleQuotation(int companyId, int id)
        {
            object[] objKey = { companyId, id };
            SaleQuotation objModel = db.SaleQuotationes.Find(objKey);
            if (objModel == null)
            {
                return NotFound();
            }

            db.SaleQuotationes.Remove(objModel);
            db.SaveChanges();

            return Ok(objModel);
        }

        [Route("GetCustomer")]
        [HttpGet]
        public HttpResponseMessage GetCustomer(string pSearch)
        {
            SupportingFunctions supportingFunctions = new SupportingFunctions();
            string serach = pSearch == null ? "" : pSearch.ToString();
            string query = " SELECT TOP 5 CustomerId [Value],CustomerName [Text] FROM ListCustomer " +
                " WHERE CustomerName LIKE  '%" + serach + "%'  ORDER BY CustomerName ASC ";
            DataTable objDataTable = supportingFunctions.QueryIntoDataTable(query);
            List<AutoCompelete> objModelList = new List<AutoCompelete>();
            objModelList = ConvertDataTable<AutoCompelete>(objDataTable);

            if (objDataTable.Rows.Count == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record were not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, objModelList);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaleQuotationExists(int id)
        {
            return db.SaleQuotationes.Count(e => e.SaleQuotationId == id) > 0;
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return obj;
        }
    }
}