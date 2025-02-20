using System;
using System.Web;
using System.Web.Http;
using TPA_Handler.Models;
using TPA_Handler.Services;

namespace TPA_Handler.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var userDetails = AuthService.GetUserDetails(request.Username, request.Password);

            if (userDetails == null)
            {
                return Unauthorized();
            }

            var (username, role) = userDetails.Value;

            string token = AuthService.GenerateJwtToken(username, role);

            if (string.IsNullOrEmpty(token))
            {
                return InternalServerError(new Exception("Token generation failed."));
            }

            // Set the auth token in a cookie
            var cookie = new HttpCookie("authToken", token)
            {
                HttpOnly = true,
                Secure = true, // Use HTTPS
                Path = "/",
                Expires = DateTime.UtcNow.AddHours(2)
            };
            HttpContext.Current.Response.Cookies.Add(cookie);

            return Ok(new { Token = token, Role = role });
        }
    }
}