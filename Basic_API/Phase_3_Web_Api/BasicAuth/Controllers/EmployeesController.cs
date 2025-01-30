using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BasicAuth.BasicAuth;
using BasicAuth.Models;

namespace BasicAuth.Controllers
{
    /// <summary>
    /// EmployeesController class is used to get the list of employees
    /// </summary>
    public class EmployeesController : ApiController
    {
        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns>Return static list of employees</returns>
        //[Authorize]
        [BasicAuthAttribute]
        [HttpGet]
        public List<Employee> GetEmployees()
        {
            return Employee.GetEmployees();
        }
    }
}
