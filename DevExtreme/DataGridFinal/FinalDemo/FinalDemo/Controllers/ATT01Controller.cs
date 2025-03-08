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
    /// Controller for managing attendance-related operations.
    /// </summary>
    public class ATT01Controller : ApiController
    {
        #region Fields

        private readonly BLATT01 _objBLAttendance = new BLATT01();
        private Response _objResponse = new Response();

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves all attendance records.
        /// </summary>
        [HttpGet]
        [Route("get_all_attendance")]
        //[JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult GetAllAttendance()
        {
            Response attendanceRecords = _objBLAttendance.GetAll();
            return Ok(attendanceRecords);
        }

        /// <summary>
        /// Retrieves attendance by employee ID.
        /// </summary>
        [HttpGet]
        [Route("get_attendance_by_id")]
        //[JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult GetAttendanceByID(int id)
        {
            Response _objRes = _objBLAttendance.Get(id);
            if (_objRes.Data == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error: Can't get attendance by ID.";
                return Ok(_objResponse);
            }
            else
            {
                _objResponse.Data = _objRes.Data;
                _objResponse.IsError = false;
                _objResponse.Message = "Success: Retrieved attendance by ID.";
                return Ok(_objResponse);
            }
        }

        /// <summary>
        /// Adds a new attendance record.
        /// </summary>
        [HttpPost]
        [Route("add_attendance")]
        //[JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        // ATT01Controller.cs (Update AddAttendance)
        public IHttpActionResult AddAttendance(DTOATT01 objDTOAtt01)
        {
                if (objDTOAtt01 == null)
                return BadRequest("Request body is empty");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate time format and relationship
           

            _objBLAttendance.Type = EnumType.A;
            _objBLAttendance.PreSave(objDTOAtt01);
            _objResponse = _objBLAttendance.Validation();

            return _objResponse.IsError
                ? (IHttpActionResult)BadRequest(_objResponse.Message)
                : Ok(_objBLAttendance.Save());
        }
        /// <summary>
        /// Updates an attendance record.
        /// </summary>
        [HttpPut]
        [Route("update_attendance")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        public IHttpActionResult UpdateAttendance(DTOATT01 objDTOAtt01)
        {
            if (objDTOAtt01 == null)
            {
                return BadRequest("Invalid request body");
            }

            _objBLAttendance.Type = EnumType.E;
            _objBLAttendance.PreSave(objDTOAtt01);
            _objResponse = _objBLAttendance.Validation();

            if (!_objResponse.IsError)
            {
                _objResponse = _objBLAttendance.Save();
            }

            return Ok(_objResponse);
        }

        /// <summary>
        /// Deletes an attendance record by ID.
        /// </summary>
        [HttpDelete]
        [Route("delete_attendance")]
        [JWTAuthorizationFilter(EnmRoleType.Admin)]
        public IHttpActionResult DeleteAttendance(int id)
        {
            _objResponse = _objBLAttendance.Delete(id);
            return Ok(_objResponse);
        }

        #endregion
    }
}
