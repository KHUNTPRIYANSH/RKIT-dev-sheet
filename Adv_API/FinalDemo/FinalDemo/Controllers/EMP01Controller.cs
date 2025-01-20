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

namespace FinalDemo.Controllers
{
    public class EMP01Controller : ApiController
    {
        #region Fields

        private readonly BLEMP01 _objBLEmployee = new BLEMP01();
        private Response _objResponse = new Response();

        #endregion

        #region API Methods

        [HttpGet]
        [Route("get_all_employees")]

        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult GetAllEmployees()
        {
            Response employees = _objBLEmployee.GetAll();
            return Ok(employees);
        }
        [HttpGet]
        [Route("get_employee_by_id")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult GetEmployeeByID(int id)
        {
            Response _objRes = _objBLEmployee.Get(id);
            if (_objRes.Data == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error : can't get use by id";
                string strResponse = $"Data : [no data], IsError : {_objResponse.IsError}, Message: {_objResponse.Message}";
                return BadRequest(strResponse);
            }
            else
            {
                _objResponse.Data = _objRes.Data;
                _objResponse.IsError = false;
                _objResponse.Message = "Success : get use by id";
                return Ok(_objResponse);
            }
        }
        [HttpPost]
        [Route("add_employee")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        public IHttpActionResult AddEmployee(DTOEMP01 objDTOEmp01)
        {
            _objBLEmployee.Type = EnumType.A;
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
        [HttpPut]
        [Route("update_employee")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        public IHttpActionResult UpdateEmployee(DTOEMP01 objDTOEmp01)
        {
            _objBLEmployee.Type = EnumType.E;
            _objBLEmployee.PreSave(objDTOEmp01);
            _objResponse = _objBLEmployee.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLEmployee.Save();
            }
            return Ok(_objResponse);
        }
        [HttpDelete]
        [Route("delete_employee")]

        [JWTAuthorizationFilter(EnmRoleType.Admin)]
        public IHttpActionResult DeleteEmployee(int id)
        {
            _objResponse = _objBLEmployee.Delete(id);
            if (_objResponse.IsError)
                return BadRequest(_objResponse.Message);
            return Ok(_objResponse);
        }

        [HttpGet]
        [Route("check_employee_exists")]

        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        //[RoleAuthorize(EnmRoleType.Admin, EnmRoleType.Editor, EnmRoleType.User)]
        public IHttpActionResult IsEmployeeExists(int id)
        {
            _objResponse = _objBLEmployee.IsEmployeeExist(id);
            if (_objResponse.Data == true)
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

        #endregion


    }
}
