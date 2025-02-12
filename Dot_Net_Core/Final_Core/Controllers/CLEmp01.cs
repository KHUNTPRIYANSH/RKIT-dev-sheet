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
    /// Provides CRUD and utility operations for employees.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLEmp01 : ControllerBase
    {
        #region Fields

        private readonly BLEmp01 _objBLEmployee;
        private Response _objResponse;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the controller with the employee business logic service.
        /// </summary>
        /// <param name="objBLEmployee">Business logic layer for employee operations.</param>
        public CLEmp01(BLEmp01 objBLEmployee)
        {
            _objBLEmployee = objBLEmployee ?? throw new ArgumentNullException(nameof(objBLEmployee));
            _objResponse = new Response();
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves all employees from the database.
        /// </summary>
        /// <returns>A response containing a list of all employees.</returns>
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
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>A response containing the employee data.</returns>
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
        /// Adds a new employee to the system.
        /// </summary>
        /// <param name="objDTOEmp01">Employee data transfer object containing employee details.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost("add_employee")]
        public IActionResult AddEmployee([FromBody] DTOEmp01 objDTOEmp01)
        {
            // Automatically validate the model state based on data annotations in DTOEmp01
            if (!ModelState.IsValid)
            {
                _objResponse.IsError = true;
                _objResponse.Data = ModelState;
                _objResponse.Message = "Formate Error";
                return Ok(_objResponse);  // Return validation errors
            }
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
        /// Updates an existing employee's details.
        /// </summary>
        /// <param name="objDTOEmp01">Employee data transfer object containing updated employee details.</param>
        /// <returns>A response indicating the result of the update operation.</returns>
        [HttpPut("update_employee")]
        public IActionResult UpdateEmployee([FromBody] DTOEmp01 objDTOEmp01)
        {
            // Automatically validate the model state based on data annotations in DTOEmp01
            if (!ModelState.IsValid)
            {
                _objResponse.IsError = true;
                _objResponse.Data = ModelState;
                _objResponse.Message = "Formate Error";
                return Ok(_objResponse);  // Return validation errors
            }
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
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to be deleted.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        [HttpDelete("delete_employee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            _objResponse = _objBLEmployee.Delete(id);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Validates if an employee exists based on their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to check.</param>
        /// <returns>A response indicating whether the employee exists.</returns>
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
        /// Retrieves the first employee from the employee list.
        /// </summary>
        /// <returns>A response containing the first employee.</returns>
        [HttpGet("get-first-employee")]
        public IActionResult GetFirstEmployee()
        {
            _objResponse = _objBLEmployee.FirstEmployee();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves the last employee from the employee list.
        /// </summary>
        /// <returns>A response containing the last employee.</returns>
        [HttpGet("get_last_employee")]
        public IActionResult GetLastEmployee()
        {
            _objResponse = _objBLEmployee.LastEmployee();
            return Ok(_objResponse);
        }

        /// <summary>
        /// Retrieves the highest-paid employee from the database.
        /// </summary>
        /// <returns>A response containing the highest-paid employee.</returns>
        [HttpGet("get_highest_paid_employee")]
        public IActionResult GetRichEmployee()
        {
            _objResponse = _objBLEmployee.RichestEmployee();
            return Ok(_objResponse);
        }

        #endregion
    }
}
