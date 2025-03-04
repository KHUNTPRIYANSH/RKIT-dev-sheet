using FinalDemo.BL.Operations;
using FinalDemo.Filters;
using FinalDemo.Models;
using FinalDemo.Models.DTO;
using FinalDemo.Models.ENUM;
using FinalDemo.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    public class USR01Controller : ApiController
    {
        #region Fields

        private readonly BLUSR01 _objBLUser = new BLUSR01();
        private Response _objResponse = new Response();

        #endregion

        #region API Methods

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>Returns a response with a list of users.</returns>
        [HttpGet]
        [Route("get_all_users")]
        [JWTAuthorizationFilter]
        public IHttpActionResult GetAllUsers()
        {
            Response users = _objBLUser.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>Returns a response containing the user data or an error message.</returns>
        [HttpGet]
        [Route("get_user_by_id")]
        [JWTAuthorizationFilter]
        public IHttpActionResult GetUserByID(int id)
        {
            Response _objRes = _objBLUser.Get(id);
            if (_objRes.Data == null)
            {
                _objResponse.IsError = true;
                _objResponse.Message = "Error: Unable to retrieve user by ID.";
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

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="objDTOUser">The user data to add.</param>
        /// <returns>Returns a response indicating the success or failure of the operation.</returns>
        [HttpPost]
        [Route("add_user")]
        [AllowAnonymous]
        public IHttpActionResult AddUser(DTOUSR01 objDTOUser)
        {
            _objBLUser.Type = EnumType.A;
            _objBLUser.PreSave(objDTOUser);
            _objResponse = _objBLUser.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLUser.Save();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Updates an existing user's information.
        /// </summary>
        /// <param name="objDTOUser">The updated user data.</param>
        /// <returns>Returns a response indicating the success or failure of the operation.</returns>
        [HttpPut]
        [Route("update_user")]
        [JWTAuthorizationFilter(EnmRoleType.Admin, EnmRoleType.Editor)]
        public IHttpActionResult UpdateUser(DTOUSR01 objDTOUser)
        {

            var temp = objDTOUser.R01F04;
            _objBLUser.Type = EnumType.E;
            _objBLUser.PreSave(objDTOUser);
            _objResponse = _objBLUser.Validation();
            if (!_objResponse.IsError)
            {
                _objResponse = _objBLUser.Save();
            }
            return Ok(_objResponse);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns a response indicating the success or failure of the deletion.</returns>
        [HttpDelete]
        [Route("delete_user")]
        [JWTAuthorizationFilter(EnmRoleType.Admin)]
        public IHttpActionResult DeleteUser(int id)
        {
            _objResponse = _objBLUser.Delete(id);
            return Ok(_objResponse);
        }

        /// <summary>
        /// Checks if a user exists by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to check.</param>
        /// <returns>Returns a response indicating whether the user exists or not.</returns>
        [HttpGet]
        [Route("check_user_exists")]
        public IHttpActionResult IsUserExists(int id)
        {
            _objResponse = _objBLUser.IsUserExist(id);
            if (_objResponse.Data == true)
            {
                _objResponse.IsError = false;
                _objResponse.Message = "Success: User exists.";
            }
            return Ok(_objResponse);
        }

        #endregion
    }
}
