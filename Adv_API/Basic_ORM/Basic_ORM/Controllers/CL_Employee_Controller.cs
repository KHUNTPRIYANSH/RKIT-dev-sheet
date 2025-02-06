using Basic_ORM.BL.Operations;
using Basic_ORM.Models;
using Basic_ORM.Models.POCO;
using Basic_ORM.Models.DTO;
using Basic_ORM.Models.Enum;
using System.Web.Http;
using System.Collections.Generic;
using System;
using Google.Protobuf.Collections;

namespace Basic_ORM.Controllers
{
    /// <summary>
    /// Controller to manage employee-related API operations.
    /// </summary>
    public class CL_Employee_Controller : ApiController
    {
        #region Fields

        private readonly BL_Employee _objBLEmployee = new BL_Employee();
        private Response _objResponse = new Response();

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        [HttpGet]
        [Route("get_all_employees")]
        public IHttpActionResult GetAllEmployees()
        {
            List<Emp01> employees = _objBLEmployee.GetAll();
            return Ok(employees);
        }

        /// <summary>
        /// Retrieves an employee by ID.
        /// </summary>
        [HttpGet]
        [Route("get_employee_by_id")]
        public IHttpActionResult GetEmployeeByID(int id)
        {
            Emp01 _objRes = _objBLEmployee.Get(id);
            if (_objRes == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error : can't get use by id";
                string strResponse = $"Data : [no data], IsError : {_objResponse.IsError}, Message: {_objResponse.Message}";
                return BadRequest(strResponse);
            }
            else
            {
                _objResponse.Data = _objRes;
                _objResponse.IsError = false;
                _objResponse.Message = "Success : get use by id";
                return Ok(_objResponse);
            }
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
            else if (_objResponse.IsError)
            {
                string strResponse = $"Data : [no data], IsError : {_objResponse.IsError}, Message: {_objResponse.Message}";
                return BadRequest(strResponse);
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
            return Ok(_objResponse);
        }

        /// <summary>
        /// Validates if an employee exists by their ID.
        /// </summary>
        [HttpGet]
        [Route("check_employee_exists")]
        public IHttpActionResult IsEmployeeExists(int id)
        {
            _objResponse.Data = _objBLEmployee.Get(id);
            if (_objResponse.Data != null)
            {
                _objResponse.IsError = false;
                _objResponse.Message = $"Success : Employee Exists";
                return Ok(_objResponse);
            }
            else
            {
                return BadRequest("Employee not found");
            }
        }

        /// <summary>
        /// Retrieves the first employee in the list.
        /// </summary>
        [HttpGet]
        [Route("get-first-employee")]
        public IHttpActionResult GetFirstEmployee()
        {
            _objResponse = _objBLEmployee.FirstEmployee();
            if (_objResponse != null)
            {
                return Ok(_objResponse);
            }
            return BadRequest(_objResponse.Message);
        }

        /// <summary>
        /// Retrieves the last employee in the list.
        /// </summary>
        [HttpGet]
        [Route("get_last_employee")]
        public IHttpActionResult GetLastEmployee()
        {
            _objResponse = _objBLEmployee.LastEmployee();
            if (_objResponse != null)
            {
                return Ok(_objResponse);
            }
            return BadRequest(_objResponse.Message);
        }

        /// <summary>
        /// Retrieves the highest-paid employee.
        /// </summary>
        [HttpGet]
        [Route("get_highest_paid_employee")]
        public IHttpActionResult GetRichEmployee()
        {
            _objResponse = _objBLEmployee.RichestEmployee();
            if (_objResponse != null)
            {
                return Ok(_objResponse);
            }
            return BadRequest(_objResponse.Message);
        }

        /// <summary>
        /// Retrieves employees where the name starts with a specific character.
        /// </summary>
        [HttpGet]
        [Route("get_employee_where_name_starts_with")]
        public IHttpActionResult GetEmployeeWhereNameStartsWith(char ch)
        {
            _objResponse = _objBLEmployee.EmployeeWhereNameStartsWith(ch);
            if (_objResponse != null)
            {
                return Ok(_objResponse);
            }
            return BadRequest(_objResponse.Message);
        }

        /// <summary>
        /// Retrieves insights for a specific department.
        /// </summary>
        [HttpGet]
        [Route("get_department_insights")]
        public IHttpActionResult GetDepartmentInsigts(string dpt)
        {
            _objResponse = _objBLEmployee.DepartmentInsigts(dpt);
            if (!_objResponse.IsError)
            {
                return Ok(_objResponse);
            }
            string strResponse = $"Data : {_objResponse.Data}, IsError : {_objResponse.IsError}, Message: {_objResponse.Message}";
            return BadRequest(strResponse);
        }

        #endregion
    }
}
