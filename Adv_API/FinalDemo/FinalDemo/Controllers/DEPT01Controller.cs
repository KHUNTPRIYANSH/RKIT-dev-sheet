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

namespace FinalDemo.Controllers
{
    public class DEPT01Controller : ApiController
    {
        #region Fields

        private readonly BLDEPT01 _objBLDepartment = new BLDEPT01();
        private Response _objResponse = new Response();

        #endregion

        #region API Methods

        [HttpGet]
        [Route("get_all_departments")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult GetAllDepartments()
        {
            Response departments = _objBLDepartment.GetAll();
            return Ok(departments);
        }

        [HttpGet]
        [Route("get_department_by_id")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]

        public IHttpActionResult GetDepartmentByID(int id)
        {
            Response _objRes = _objBLDepartment.Get(id);
            if (_objRes.Data == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error: can't get department by id";
                string strResponse = $"Data: [no data], IsError: {_objResponse.IsError}, Message: {_objResponse.Message}";
                return BadRequest(strResponse);
            }
            else
            {
                _objResponse.Data = _objRes.Data;
                _objResponse.IsError = false;
                _objResponse.Message = "Success: get department by id";
                return Ok(_objResponse);
            }
        }

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
            else if (_objResponse.IsError)
            {
                string strResponse = $"Data: [no data], IsError: {_objResponse.IsError}, Message: {_objResponse.Message}";
                return BadRequest(strResponse);
            }
            return Ok(_objResponse);
        }

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

        [HttpDelete]
        [Route("delete_department")]
        [JWTAuthorizationFilter(EnmRoleType.Admin)]

        public IHttpActionResult DeleteDepartment(int id)
        {
            _objResponse = _objBLDepartment.Delete(id);
            if (_objResponse.IsError)
                return BadRequest(_objResponse.Message);
            return Ok(_objResponse);
        }

        [HttpGet]
        [Route("check_department_exists")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]

        public IHttpActionResult IsDepartmentExists(int id)
        {
            _objResponse = _objBLDepartment.IsDepartmentExist(id);
            if (_objResponse.Data != null && (bool)_objResponse.Data)
            {
                _objResponse.IsError = false;
                _objResponse.Message = "Success: Department Exists";
                return Ok(_objResponse);
            }
            else
            {
                return BadRequest("Department not found");
            }
        }
        [HttpGet]
        [Route("check_department_insights")]
        [JWTAuthorizationFilter(EnmRoleType.Admin)]
        public IHttpActionResult DepartmentInsights(int id)
        {
            _objResponse = _objBLDepartment.IsDepartmentExist(id);
            if (_objResponse.Data != false && (bool)_objResponse.Data)
            {
                _objResponse = _objBLDepartment.GetDepartmentInsights(id);
                return Ok(_objResponse);
            }
            else
            {
                return BadRequest("Department not found 😞");
            }
        }

        #endregion
    }
}
