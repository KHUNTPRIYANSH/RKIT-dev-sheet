using Final_Core.BL.Operations;
using Final_Core.Models;
using Final_Core.Models.POCO;
using Final_Core.Models.DTO;
using Final_Core.Models.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Final_Core.Controllers
{
    /// <summary>
    /// Controller to manage employee-related API operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLEmp01 : ControllerBase
    {
        #region Fields

        private readonly BLEmp01 _objBLEmployee;
        private Response _objResponse;

        public CLEmp01(BLEmp01 objBLEmployee)
        {
            _objBLEmployee = objBLEmployee ?? throw new ArgumentNullException(nameof(objBLEmployee));
            _objResponse = new Response();
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        [HttpGet("get_all_employees")]
        public IActionResult GetAllEmployees()
        {
            var employees = _objBLEmployee.GetAll();
            if (employees == null || !employees.Any())
            {
                _objResponse = new Response { IsError = true, Message = "No employees found." };
            }
            else
            {
                _objResponse = new Response { IsError = false, Message = "Success: Employees retrieved", Data = employees };
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves an employee by ID.
        /// </summary>
        [HttpGet("get_employee_by_id/{id}")]
        public IActionResult GetEmployeeByID(int id)
        {
            var employee = _objBLEmployee.Get(id);
            if (employee == null)
            {
                _objResponse = new Response { IsError = true, Message = $"Employee with ID {id} not found." };
            }
            else
            {
                _objResponse = new Response { IsError = false, Message = "Success: Employee found", Data = employee };
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        [HttpPost("add_employee")]
        public IActionResult AddEmployee([FromBody] DTOEmp01 objDTOEmp01)
        {
            _objBLEmployee.Type = EnmType.A;
            _objBLEmployee.PreSave(objDTOEmp01);
            _objResponse = _objBLEmployee.Validation();
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }
            _objResponse = _objBLEmployee.Save();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        [HttpPut("update_employee")]
        public IActionResult UpdateEmployee([FromBody] DTOEmp01 objDTOEmp01)
        {
            _objBLEmployee.Type = EnmType.E;
            _objBLEmployee.PreSave(objDTOEmp01);
            _objResponse = _objBLEmployee.Validation();
            if (_objResponse.IsError)
            {
                return Ok(_objResponse);
            }
            _objResponse = _objBLEmployee.Save();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        [HttpDelete("delete_employee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _objResponse = _objBLEmployee.Delete(id);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Validates if an employee exists by their ID.
        /// </summary>
        [HttpGet("check_employee_exists/{id}")]
        public IActionResult IsEmployeeExists(int id)
        {
            var employee = _objBLEmployee.Get(id);
            if (employee != null)
            {
                _objResponse = new Response { IsError = false, Message = "Success: Employee Exists", Data = employee };
            }
            else
            {
                _objResponse = new Response { IsError = true, Message = "Employee not found", Data = "" };
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves the first employee in the list.
        /// </summary>
        [HttpGet("get-first-employee")]
        public IActionResult GetFirstEmployee()
        {
            _objResponse = _objBLEmployee.FirstEmployee();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves the last employee in the list.
        /// </summary>
        [HttpGet("get_last_employee")]
        public IActionResult GetLastEmployee()
        {
            _objResponse = _objBLEmployee.LastEmployee();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves the highest-paid employee.
        /// </summary>
        [HttpGet("get_highest_paid_employee")]
        public IActionResult GetRichEmployee()
        {
            _objResponse = _objBLEmployee.RichestEmployee();
            return Ok(_objResponse);
        }

        #endregion
    }
}
