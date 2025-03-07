﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Adv_Final.BL.Operation;
using Adv_Final.Extensions;
using Adv_Final.Filters;
using Adv_Final.Models;
using Adv_Final.Models.DTO;
using Adv_Final.Models.Enum;
using Adv_Final.Models.POCO;

namespace Adv_Final.Controllers
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

        [JWTAuthorizationFilter(EnmRole.Admin, EnmRole.Editor, EnmRole.Normal)]
        public IHttpActionResult GetAllEmployees()
        {
            Response employees = _objBLEmployee.GetAll();
            return Ok(employees);
        }
        [HttpGet]
        [Route("get_employee_by_id")]
        [JWTAuthorizationFilter(EnmRole.Admin, EnmRole.Editor, EnmRole.Normal)]
        public IHttpActionResult GetEmployeeByID(int id)
        {
            Response _objRes = _objBLEmployee.Get(id);
            if (_objRes.Data == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error : can't get use by id";
                string strResponse = $"Data : [no data], IsError : {_objResponse.IsError}, Message: {_objResponse.Message}";
                return Ok(_objResponse); 
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
        [JWTAuthorizationFilter(EnmRole.Admin, EnmRole.Editor)]
        public IHttpActionResult AddEmployee(DTOEMP01 objDTOEmp01)
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
                return Ok(_objResponse);
            }
            return Ok(_objResponse);
        }
        [HttpPut]
        [Route("update_employee")]
        [JWTAuthorizationFilter(EnmRole.Admin, EnmRole.Editor)]
        public IHttpActionResult UpdateEmployee(DTOEMP01 objDTOEmp01)
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
        [HttpDelete]
        [Route("delete_employee")]

        [JWTAuthorizationFilter(EnmRole.Admin)]
        public IHttpActionResult DeleteEmployee(int id)
        {
            _objResponse = _objBLEmployee.Delete(id);
            if (_objResponse.IsError)
                return Ok(_objResponse);
            return Ok(_objResponse);
        }

        [HttpGet]
        [Route("check_employee_exists")]

        [JWTAuthorizationFilter(EnmRole.Admin, EnmRole.Editor, EnmRole.Normal)]
        //[RoleAuthorize(EnmRole.Admin, EnmRole.Editor, EnmRole.Normal)]
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
                _objResponse.Data = null;
                _objResponse.Message = "Error : Employee not found";
                return Ok(_objResponse); ;
            }
        }
        #endregion
    }
}
