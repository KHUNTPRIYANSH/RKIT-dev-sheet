using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Adv_Final.BL.Operation;
using Adv_Final.Models;
using Adv_Final.Models.DTO;
using Adv_Final.Models.Enum;
using Adv_Final.Models.POCO;

namespace Adv_Final.Controllers
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
        public IHttpActionResult GetAllDepartments()
        {
            Response departments = _objBLDepartment.GetAll();
            return Ok(departments);
        }

        [HttpGet]
        [Route("get_department_by_id")]
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
        public IHttpActionResult AddDepartment(DTODEPT01 objDTODept01)
        {
            _objBLDepartment.Type = EnmType.A;
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
        public IHttpActionResult UpdateDepartment(DTODEPT01 objDTODept01)
        {
            _objBLDepartment.Type = EnmType.E;
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
        public IHttpActionResult DeleteDepartment(int id)
        {
            _objResponse = _objBLDepartment.Delete(id);
            if (_objResponse.IsError)
                return BadRequest(_objResponse.Message);
            return Ok(_objResponse);
        }

        [HttpGet]
        [Route("check_department_exists")]
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

        #endregion
    }
}
