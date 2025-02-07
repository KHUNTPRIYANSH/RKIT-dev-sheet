using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FinalDemo.BL.Operation;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Models.ENUM;
using FinalDemo.Filters;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing department-related operations.
    /// </summary>
    public class DEPT01Controller : ApiController
    {
        #region Fields

        private readonly BLDEPT01 _objBLDepartment = new BLDEPT01();
        private Response _objResponse = new Response();

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>Returns a list of all departments.</returns>
        [HttpGet]
        [Route("get_all_departments")]
        [JWTAuthorizationFilter]
        public IHttpActionResult GetAllDepartments()
        {
            Response departments = _objBLDepartment.GetAll();
            return Ok(departments);
        }

        /// <summary>
        /// Retrieves a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to retrieve.</param>
        /// <returns>Returns the department details or an error message if not found.</returns>
        [HttpGet]
        [Route("get_department_by_id")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult GetDepartmentByID(int id)
        {
            Response _objRes = _objBLDepartment.Get(id);
            if (_objRes.Data == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error: Can't get department by ID.";
                return Ok(_objResponse);
            }
            else
            {
                _objResponse.Data = _objRes.Data;
                _objResponse.IsError = false;
                _objResponse.Message = "Success: Retrieved department by ID.";
                return Ok(_objResponse);
            }
        }

        /// <summary>
        /// Adds a new department.
        /// </summary>
        /// <param name="objDTODept01">The department data to add.</param>
        /// <returns>Returns a success or error message based on the operation result.</returns>
        [HttpPost]
        [Route("add_department")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        public IHttpActionResult AddDepartment(DTODEPT01 objDTODept01)
        {
            _objBLDepartment.Type = EnumType.A;
            _objBLDepartment.PreSave(objDTODept01);
            _objResponse = _objBLDepartment.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLDepartment.Save();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="objDTODept01">The updated department data.</param>
        /// <returns>Returns a success or error message based on the operation result.</returns>
        [HttpPut]
        [Route("update_department")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        public IHttpActionResult UpdateDepartment(DTODEPT01 objDTODept01)
        {
            _objBLDepartment.Type = EnumType.E;
            _objBLDepartment.PreSave(objDTODept01);
            _objResponse = _objBLDepartment.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLDepartment.Save();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>Returns a success or error message based on the operation result.</returns>
        [HttpDelete]
        [Route("delete_department")]
        [JWTAuthorizationFilter(EnmRoleType.Admin)]
        public IHttpActionResult DeleteDepartment(int id)
        {
            _objResponse = _objBLDepartment.Delete(id);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Checks if a department exists by its ID.
        /// </summary>
        /// <param name="id">The ID of the department to check.</param>
        /// <returns>Returns a message indicating whether the department exists or not.</returns>
        [HttpGet]
        [Route("check_department_exists")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult IsDepartmentExists(int id)
        {
            _objResponse = _objBLDepartment.IsDepartmentExist(id);
            if (_objResponse.Data != null && (bool)_objResponse.Data)
            {
                _objResponse.IsError = false;
                _objResponse.Message = "Success: Department exists.";
            }
            return Ok(_objResponse);
        }

        #endregion
    }
}
