using Microsoft.AspNetCore.Mvc;
using Logging.Services;
using Logging.Models;

namespace Logging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly LoggingService _service;

        public EmployeeController(ILogger<EmployeeController> logger, LoggingService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet("log-test")]
        public IActionResult LogTest()
        {
            _logger.LogInformation("EmployeeController: This is an info log.");
            _logger.LogWarning("EmployeeController: This is a warning log.");
            _logger.LogError("EmployeeController: This is an error log.");

            _service.PerformTask(); // Demonstrates logging in a service
            return Ok("Logs have been written.");
        }

        [HttpGet("GetAllEmployees")]
        public IActionResult GetEmployees()
        {
            _logger.LogInformation("Controller: [GetAllEmployees] executed.");

            if (Employee.employees.Count > 0)
            {
                _logger.LogInformation("Controller: Employee list retrieved successfully.");
                return Ok(Employee.employees);
            }
            else
            {
                _logger.LogWarning("Controller: No employees found.");
                _service.PerformTask();
                return BadRequest("Fail: No data in employee list.");
            }
        }

        [HttpGet("GetEmployeeById/{id}")]
        public IActionResult GetEmployee(int id)
        {
            _logger.LogInformation($"Controller: [GetEmployeeById] with id = {id} executed.");

            if (id <= 0)
            {
                _logger.LogWarning("Controller: Invalid id provided.");
                return BadRequest("Fail: id must be at least 1");
            }

            var employee = Employee.employees.SingleOrDefault(e => e.id == id);

            if (employee == null)
            {
                _logger.LogError($"Controller: Employee with id = {id} not found.");
                return NotFound($"Fail: Employee with id = {id} not found.");
            }

            _logger.LogInformation($"Controller: Employee with id = {id} retrieved successfully.");
            return Ok(employee);
        }

        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            _logger.LogInformation("Controller: [AddEmployee] executed.");

            if (employee == null)
            {
                _logger.LogWarning("Controller: Invalid employee data provided.");
                return BadRequest("Fail: Invalid employee data.");
            }

            Employee.employees.Add(employee);
            _logger.LogInformation($"Controller: Employee with id = {employee.id} added successfully.");
            return Ok($"Employee with id = {employee.id} added successfully.");
        }

        [HttpPut("UpdateEmployee/{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            _logger.LogInformation($"Controller: [UpdateEmployee] with id = {id} executed.");

            var employee = Employee.employees.SingleOrDefault(e => e.id == id);
            if (employee == null)
            {
                _logger.LogError($"Controller: Employee with id = {id} not found.");
                return NotFound($"Fail: Employee with id = {id} not found.");
            }

            employee.name = updatedEmployee.name;
            employee.city = updatedEmployee.city;
            _logger.LogInformation($"Controller: Employee with id = {id} updated successfully.");
            return Ok($"Employee with id = {id} updated successfully.");
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _logger.LogInformation($"Controller: [DeleteEmployee] with id = {id} executed.");

            var employee = Employee.employees.SingleOrDefault(e => e.id == id);
            if (employee == null)
            {
                _logger.LogError($"Controller: Employee with id = {id} not found.");
                return NotFound($"Fail: Employee with id = {id} not found.");
            }

            Employee.employees.Remove(employee);
            _logger.LogInformation($"Controller: Employee with id = {id} deleted successfully.");
            return Ok($"Employee with id = {id} deleted successfully.");
        }
    }

    
}
