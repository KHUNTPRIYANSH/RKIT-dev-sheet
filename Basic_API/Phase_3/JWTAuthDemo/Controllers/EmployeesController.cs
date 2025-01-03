using System.Collections.Generic;
using System.Web.Http;
using JWTAuthDemo.Models;

namespace JWTAuthDemo.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            return Employee.GetEmployees();
        }
    }
}
