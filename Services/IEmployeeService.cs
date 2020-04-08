using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SBAccountAPI.Models;
using SBAccountAPI.Models.Entities;

namespace SBAccountAPI.Services
{
    public interface IEmployeeService
    {
        Task<IQueryable<Employee>> GetEmployees();
    }

    public class EmployeeService
    {
        
    }
}