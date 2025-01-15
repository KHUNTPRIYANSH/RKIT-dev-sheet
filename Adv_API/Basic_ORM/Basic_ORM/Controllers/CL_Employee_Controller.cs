using Basic_ORM.BL.Operations;
using Basic_ORM.Models;
using Basic_ORM.Models.POCO;
using Basic_ORM.Models.DTO;
using Basic_ORM.Models.Enum;
using System.Web.Http;
using System.Collections.Generic;
using System;

namespace Basic_ORM.Controllers
{
    /// <summary>
    /// Controller to manage employee-related API operations.
    /// </summary>
    public class CL_Employee_Controller : ApiController
    {
        private readonly BL_Employee _objBLEmployee = new BL_Employee();
        private Response _objResponse;

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        [HttpGet]
        [Route("get_all_employees")]
        public IHttpActionResult GetAllEmployees()
        {
            var employees = _objBLEmployee.GetAll();
            return Ok(employees);
        }

        /// <summary>
        /// Retrieves an employee by ID.
        /// </summary>
        [HttpGet]
        [Route("get_employee_by_id")]
        public IHttpActionResult GetEmployeeByID(int id)
        {
            var employee = _objBLEmployee.Get(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        [HttpPost]
        [Route("add_employee")]
        public IHttpActionResult AddEmployee(DTO_Emp01 objDTOEmp01)
        {
            _objBLEmployee.Type = EnmType.A;
            _objBLEmployee.PreSave(objDTOEmp01);
            _objResponse = _objBLEmployee.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLEmployee.Save();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        [HttpPut]
        [Route("update_employee")]
        public IHttpActionResult UpdateEmployee(DTO_Emp01 objDTOEmp01)
        {
            _objBLEmployee.Type = EnmType.E;
            _objBLEmployee.PreSave(objDTOEmp01);
            _objResponse = _objBLEmployee.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLEmployee.Save();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        [HttpDelete]
        [Route("delete_employee")]
        public IHttpActionResult DeleteEmployee(int id)
        {
            _objResponse = _objBLEmployee.Delete(id);
            if (_objResponse.IsError)
                return BadRequest(_objResponse.Message);
            return Ok(_objResponse.Message);
        }

        /// <summary>
        /// Validates if an employee exists by their ID.
        /// </summary>
        [HttpGet]
        [Route("check_employee_exists")]
        public IHttpActionResult IsEmployeeExists(int id)
        {
            var exists = _objBLEmployee.Get(id);
            if (exists != null)
            {
                return Ok($"Employee Exists: {exists}");
            }
            else
            {
                return BadRequest(_objResponse.Message);
            }
        }

        [HttpGet]
        [Route("get-first-employee")]
        public IHttpActionResult GetFirstEmployee()
        {
            var record = _objBLEmployee.FirstEmployee();
            if(record != null)
            {
                return Ok(record);
            }
            return BadRequest(_objResponse.Message);
        }
        [HttpGet]
        [Route("get_last_employee")]
        public IHttpActionResult GetLastEmployee()
        {
            var record = _objBLEmployee.LastEmployee();
            if(record != null)
            {
                return Ok(record);
            }
            return BadRequest(_objResponse.Message);
        }

        [HttpGet]
        [Route("get_highest_paid_employee")]
        public IHttpActionResult GetRichEmployee()
        {
            var record = _objBLEmployee.RichestEmployee();
            if (record != null)
            {
                return Ok(record);
            }
            return BadRequest(_objResponse.Message);
        }[HttpGet]
        [Route("get_mployee_where_name_starts_with")]
        public IHttpActionResult GetEmployeeWhereNameStartsWith(char ch)
        {
            var record = _objBLEmployee.EmployeeWhereNameStartsWith(ch);
            if (record != null)
            {
                return Ok(record);
            }
            return BadRequest(_objResponse.Message);
        }
    }
}
