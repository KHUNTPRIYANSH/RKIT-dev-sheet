using Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        public EmployeeController()
        {
        }

        [HttpGet("GetAllEmployees")]
        public IActionResult GetEmployees()
        {
            if (Employee.employees.Count > 0)
            {
                return Ok(Employee.employees);
            }
            else
            {
                return BadRequest("Fail : No data in employee list");
            }
        }

        [HttpGet("GetEmployeeById")]
        public IActionResult GetEmployee(int id)
        {
            if (id > 0)
            {
                var temp = Employee.employees.SingleOrDefault<Employee>(e => e.id == id);
                if (temp != null)
                {
                    return Ok(temp);
                }
                else
                {
                    return BadRequest($"Fail : Employee having id = {id} not found");
                }
            }
            return BadRequest("Fail : id must be at least 1");
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            if (employee == null) return BadRequest("Fail : employee details not provided");

            if (employee.id > 0)
            {
                var isExists = Employee.employees.Find(e => e.id == employee.id);
                if (isExists != null)
                {
                    return BadRequest($"Fail : Employee having id = {employee.id} already exists");
                }
                else
                {
                    Employee.employees.Add(employee);
                    return Ok("Success : Employee added!!");
                }
            }
            else
            {
                return BadRequest("Fail : id can't be negative or zero");
            }
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            if (employee == null) return BadRequest("Fail : employee details not provided");

            if (employee.id > 0)
            {
                var isExists = Employee.employees.Find(e => e.id == employee.id);
                if (isExists != null)
                {
                    isExists.name = employee.name;
                    isExists.city = employee.city;
                    isExists.isActive = employee.isActive;
                    return Ok("Success : Employee details updated");
                }
                else
                {
                    return BadRequest($"Fail : Employee having id = {employee.id} does not exist");
                }
            }
            else
            {
                return BadRequest("Fail : id can't be negative or zero");
            }
        }

        [HttpPatch]
        public IActionResult PatchEmployee(int id, string name = null, string city = null, bool? isActive = null)
        {
            if (id <= 0) return BadRequest("Fail : id must be greater than zero");

            var employee = Employee.employees.Find(e => e.id == id);
            if (employee != null)
            {
                if (!string.IsNullOrEmpty(name)) employee.name = name;
                if (!string.IsNullOrEmpty(city)) employee.city = city;
                if (isActive.HasValue) employee.isActive = isActive.Value;

                return Ok("Success : Employee details patched");
            }
            else
            {
                return BadRequest($"Fail : Employee having id = {id} does not exist");
            }
        }

        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            if (id <= 0) return BadRequest("Fail : id must be greater than zero");

            var isExists = Employee.employees.Find(e => e.id == id);
            if (isExists != null)
            {
                Employee.employees.Remove(isExists);
                return Ok($"Success : Employee having id = {id} deleted");
            }
            else
            {
                return BadRequest($"Fail : Employee having id = {id} does not exist");
            }
        }
    }
}