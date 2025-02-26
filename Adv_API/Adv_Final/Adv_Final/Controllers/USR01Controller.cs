using System;
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
    public class USR01Controller : ApiController
    {
        #region Fields

        private readonly BLUSR01 _objBLUser = new BLUSR01();
        private Response _objResponse = new Response();

        #endregion

        #region API Methods

        [HttpGet]
        [Route("get_all_users")]
        public IHttpActionResult GetAllUsers()
        {
            Response users = _objBLUser.GetAll();
            return Ok(users);
        }

        [HttpGet]
        [Route("get_user_by_id")]
        public IHttpActionResult GetUserByID(int id)
        {
            Response _objRes = _objBLUser.Get(id);
            if (_objRes.Data == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error: Unable to retrieve user by ID.";
                string strResponse = $"Data: [no data], IsError: {_objResponse.IsError}, Message: {_objResponse.Message}";
                return Ok(_objResponse);
            }
            else
            {
                _objResponse.Data = _objRes.Data;
                _objResponse.IsError = false;
                _objResponse.Message = "Success: User retrieved by ID.";
                return Ok(_objResponse);
            }
        }

        [HttpPost]
        [Route("add_user")]
        public IHttpActionResult AddUser(DTOUSR01 objDTOUser)
        {
            _objBLUser.Type = EnmType.A;
            _objBLUser.PreSave(objDTOUser);
            _objResponse = _objBLUser.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLUser.Save();
            }
            else
            {
                string strResponse = $"Data: [no data], IsError: {_objResponse.IsError}, Message: {_objResponse.Message}";
                return Ok(_objResponse);
            }
            return Ok(_objResponse);
        }

        [HttpPut]
        [Route("update_user")]
        public IHttpActionResult UpdateUser(DTOUSR01 objDTOUser)
        {
            _objBLUser.Type = EnmType.E;
            _objBLUser.PreSave(objDTOUser);
            _objResponse = _objBLUser.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLUser.Save();
            }
            return Ok(_objResponse);
        }

        [HttpDelete]
        [Route("delete_user")]
        public IHttpActionResult DeleteUser(int id)
        {
            _objResponse = _objBLUser.Delete(id);
            if (_objResponse.IsError)
                return Ok(_objResponse);
            return Ok(_objResponse);
        }

        [HttpGet]
        [Route("check_user_exists")]
        public IHttpActionResult IsUserExists(int id)
        {
            _objResponse = _objBLUser.IsUserExist(id);
            if (_objResponse.Data == true)
            {
                _objResponse.IsError = false;
                _objResponse.Message = "Success: User exists.";
                return Ok(_objResponse);
            }
            else
            {
                return Ok(_objResponse);
            }
        }

        #endregion
    }
}
