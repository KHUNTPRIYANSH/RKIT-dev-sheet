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
    [RoutePrefix("api/LoginPanel")]
    public class LoginPanelController : ApiController
    {
        private BLUSR01 _objBLuser;
        private readonly IDbConnectionFactory _dbFactory;
        

        public Response _objResponce;

        public LoginPanelController()
        {
            _objBLuser = new BLUSR01();
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _objResponce = new Response();
        }



        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] Login userInfo)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var ecrypted = EncryptionHelper.GetEncryptPassword(userInfo.Password);
                // Fetch user from the database based on username
                var user = db.Single<USR01>(u => u.R01F02 == userInfo.UserName && u.R01F03.Equals(ecrypted));

                // Validate user existence and password (you can implement your own validation here)
                if (user == null)
                {
                    return Unauthorized(); // Invalid credentials
                }

                // Generate JWT token with the user's role from the database
                var token = JWTHelper.GenerateJwtToken(user.R01F02, user.R01F04);

                // Send back the token along with the user information
                return Ok(new
                {
                    Token = token,
                    Role = user.R01F04.ToString(),
                    UserName = user.R01F02
                });
            }
        }
    }
}
