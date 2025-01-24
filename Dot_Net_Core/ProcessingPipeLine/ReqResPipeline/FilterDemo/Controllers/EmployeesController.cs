using FilterDemo.Filters.ActionFilters;
using FilterDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FilterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Fixed: Added a semicolon at the end of the list definition.
        List<Employee> employees = new List<Employee>
        {
            new Employee { id = 1, name = "KPD", dept_id = 7, salary = 50000, isActive = true },
            new Employee { id = 2, name = "Keyur", dept_id = 11, salary = 45000, isActive = true },
            new Employee { id = 3, name = "Parth", dept_id = 7, salary = 45000, isActive = true },
            new Employee { id = 4, name = "Yash", dept_id = 11, salary = 45000, isActive = true },
            new Employee { id = 5, name = "Maulik", dept_id = 7, salary = 45000, isActive = true },
            new Employee { id = 6, name = "Dimple", dept_id = 2, salary = 45000, isActive = true }
        };

        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return employees;
        }

        [HttpGet("{id:int:min(0):max(100)}")]
        [MySpecificActionFilterAttribute("Only run for GetEmployees [sync]", 2)]
        //[MySpecificActionFilterAttribute("Only run for GetEmployees [sync]", -10)] // this will  get executed first as order value in min
        [MyActionFilterAsyncAttribute("Only run for GetEmployees [async]")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            Employee employee = employees.SingleOrDefault(e => e.id == id);

            if (employee == null)
            {
                throw new Exception("!!! Just to test exception filter");            }

            return Ok(employee); // Return the employee
        }

    }
}
