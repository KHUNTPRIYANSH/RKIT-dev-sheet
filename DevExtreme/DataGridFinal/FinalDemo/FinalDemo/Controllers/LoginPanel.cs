using FinalDemo.BL.Operations;
using FinalDemo.Models;
using FinalDemo.Models.POCO;
using System;
using System.Web.Http;
using FinalDemo.Helpers;
using System.Data.Odbc;
using FinalDemo.Models.DTO;
using ServiceStack.Data;
using System.Web;
using ServiceStack.OrmLite;

namespace FinalDemo.Controllers
{
    /// <summary>
    /// Controller responsible for login operations in the Login Panel.
    /// </summary>
    [RoutePrefix("api/LoginPanel")]
    public class LoginPanelController : ApiController
    {
        private BLUSR01 _objBLuser;
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Response object for holding API responses.
        /// </summary>
        public Response _objResponce;

        /// <summary>
        /// Initializes a new instance of the LoginPanelController.
        /// </summary>
        public LoginPanelController()
        {
            _objBLuser = new BLUSR01();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _objResponce = new Response();
        }

        /// <summary>
        /// Authenticates the user by validating the username and password.
        /// </summary>
        /// <param name="userInfo">The login information containing username and password.</param>
        /// <returns>Returns an HTTP response with a JWT token if the login is successful or Unauthorized if the credentials are invalid.</returns>
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] Login userInfo)
       {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Encrypt the password and fetch the user from the database
                var ecrypted = EncryptionHelper.GetEncryptPassword(userInfo.Password);
                var user = db.Single<USR01>(u => u.R01F02 == userInfo.UserName && u.R01F03.Equals(ecrypted));
             

                // Validate user existence and password
                if (user == null)
                {
                    return Unauthorized(); // Invalid credentials
                }

                // Generate JWT token with the user's role from the database
                var token = JWTHelper.GenerateJwtToken(user.R01F02, user.R01F04);

                // Return the token along with the user's role and username
                return Ok(new
                {
                    ID = user.R01F01,
                    Token = token,
                    Role = user.R01F04.ToString(),
                    UserName = user.R01F02
                });
            }
        }
    }
}
