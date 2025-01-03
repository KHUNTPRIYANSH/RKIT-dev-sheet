using System.Web.Http;
using JWTAuthDemo.Helpers;

namespace JWTAuthDemo.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginRequest login)
        {
            if (login == null || login.Username != "admin" || login.Password != "password")
                return Unauthorized();

            var token = JwtHelper.GenerateToken(login.Username);
            return Ok(new { token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
