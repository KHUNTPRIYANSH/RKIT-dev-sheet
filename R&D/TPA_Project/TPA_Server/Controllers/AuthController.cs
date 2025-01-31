using System.Collections.Generic;
using System.Web.Http;
using TPA_Server.Helpers;
using TPA_Server.Models;

namespace TPA_Server.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly DbHelper _dbHelper;

        public AuthController()
        {
            _dbHelper = new DbHelper(); // Initialize the DbHelper class
        }

        // POST: api/auth/generate-token
        [HttpPost]
        [Route("generate-token")]
        public IHttpActionResult GenerateToken([FromBody] AuthModel authModel)
        {
            if (authModel == null)
            {
                return BadRequest("AuthModel is required.");
            }

            _dbHelper.SaveAuthModelToDatabase(authModel.Username, authModel.Password, authModel.Role, authModel.Dependencies, authModel.TokenExpiryInMinutes);

            return Ok(new
            {
                Username = authModel.Username,
                Role = authModel.Role,
                Dependencies = authModel.Dependencies,
                TokenExpiryInMinutes = authModel.TokenExpiryInMinutes
            });
        }

        // GET: api/auth/get-all
        [HttpGet]
        [Route("get-all")]
        public IHttpActionResult GetAllUsers()
        {
            var users = _dbHelper.GetAllUsers();
            return Ok(users);
        }

        // GET: api/auth/get/{id}
        [HttpGet]
        [Route("get/{id:int}")]
        public IHttpActionResult GetUserById(int id)
        {
            var user = _dbHelper.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/auth/update/{id}
        [HttpPut]
        [Route("update/{id:int}")]
        public IHttpActionResult UpdateUser(int id, [FromBody] AuthModel authModel)
        {
            if (authModel == null)
            {
                return BadRequest("AuthModel is required.");
            }

            bool updated = _dbHelper.UpdateUser(id, authModel.Username, authModel.Password, authModel.Role, authModel.Dependencies, authModel.TokenExpiryInMinutes);

            if (!updated)
            {
                return NotFound();
            }

            return Ok("User updated successfully.");
        }

        // DELETE: api/auth/delete/{id}
        [HttpDelete]
        [Route("delete/{id:int}")]
        public IHttpActionResult DeleteUser(int id)
        {
            bool deleted = _dbHelper.DeleteUser(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok("User deleted successfully.");
        }



        [HttpGet]
        [Route("get-user")]
        public IHttpActionResult GetUser()
        {
            string token = Request.Headers.Authorization?.Parameter;
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            var authModel = JWTHelper.ValidateJwtToken(token);
            if (authModel == null)
                return Unauthorized();

            return Ok(authModel);
        }

    }
}
