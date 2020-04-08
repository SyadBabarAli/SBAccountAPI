using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SBAccountAPI.Context;
using SBAccountAPI.Models;

namespace SBAccountAPI.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : BaseController
    {
        private EntityContext db = new EntityContext();

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IHttpActionResult> GetEmployee(int? page = null, int pageSize = 10, string orderBy = nameof(Employee.Id), bool ascending = true)
        {
            if (page == null)
                return Ok(await db.Employees.ToListAsync());

            var employees = await CreatePagedResults< Employee, EmployeeModel>(db.Employees, page.Value, pageSize, orderBy, ascending);
            return Ok(employees);
        }

        //public EmployeesController(EntityContext context) : base(context) {}
    }
}
