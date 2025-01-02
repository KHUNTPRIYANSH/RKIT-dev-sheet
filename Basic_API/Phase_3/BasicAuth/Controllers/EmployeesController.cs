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
    public class EmployeesController : ApiController
    {
        //[Authorize]
        [BasicAuthAttribute]
        public List<Employee> GetEmployees()
        {
            return Employee.GetEmployees();
        }
    }
}
