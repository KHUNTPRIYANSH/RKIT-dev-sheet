using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalDemo.BL.Operation;
using FinalDemo.Extension;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Models.ENUM;
using FinalDemo.Filters;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing employee-related operations.
    /// </summary>
    public class EMP01Controller : ApiController
    {
        #region Fields

        private readonly BLEMP01 _objBLEmployee = new BLEMP01();
        private Response _objResponse = new Response();

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>Returns a list of all employees.</returns>
        [HttpGet]
        [Route("get_all_employees")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult GetAllEmployees()
        {
            Response employees = _objBLEmployee.GetAll();
            return Ok(employees);
        }

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>Returns the employee details or an error message if not found.</returns>
        [HttpGet]
        [Route("get_employee_by_id")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult GetEmployeeByID(int id)
        {
            Response _objRes = _objBLEmployee.Get(id);
            if (_objRes.Data == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error: Can't get user by ID.";
                return Ok(_objResponse);
            }
            else
            {
                _objResponse.Data = _objRes.Data;
                _objResponse.IsError = false;
                _objResponse.Message = "Success: Retrieved user by ID.";
                return Ok(_objResponse);
            }
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="objDTOEmp01">The employee data to add.</param>
        /// <returns>Returns a success or error message based on the operation result.</returns>
        [HttpPost]
        [Route("add_employee")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        public IHttpActionResult AddEmployee( DTOEMP01 objDTOEmp01)
        {
            _objBLEmployee.Type = EnumType.A;
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
        /// <param name="objDTOEmp01">The updated employee data.</param>
        /// <returns>Returns a success or error message based on the operation result.</returns>
        [HttpPut]
        [Route("update_employee")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        public IHttpActionResult UpdateEmployee( DTOEMP01 objDTOEmp01) // <== Add [FromBody]
        {
            if (objDTOEmp01 == null)
            {
                return BadRequest("Invalid request body");
            }

            Console.WriteLine("Received Update Request:");
            Console.WriteLine($"P01F01: {objDTOEmp01.P01F01}");
            Console.WriteLine($"P01F02: {objDTOEmp01.P01F02}");

            _objBLEmployee.Type = EnumType.E;
            _objBLEmployee.PreSave(objDTOEmp01);
            _objResponse = _objBLEmployee.Validation();

            if (!_objResponse.IsError)
            {
                _objResponse = _objBLEmployee.Save();
            }

            return Ok(_objResponse);
        }


        /// <summary>
        /// Deletes an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>Returns a success or error message based on the operation result.</returns>
        [HttpDelete]
        [Route("delete_employee")]
        [JWTAuthorizationFilter(EnmRoleType.Admin)]
        public IHttpActionResult DeleteEmployee(int id)
        {
            _objResponse = _objBLEmployee.Delete(id);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Checks if an employee exists by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to check.</param>
        /// <returns>Returns a message indicating whether the employee exists or not.</returns>
        [HttpGet]
        [Route("check_employee_exists")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult IsEmployeeExists(int id)
        {
            _objResponse = _objBLEmployee.IsEmployeeExist(id);
            if (_objResponse.Data == true)
            {
                _objResponse.IsError = false;
                _objResponse.Message = "Success: Employee exists.";
            }
            return Ok(_objResponse);
        }

        #endregion
    }
}
