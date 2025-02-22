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

            // Determine if the client is an API client (e.g., Swagger) based on the Accept header
            bool isApiClient = Request.Headers.Accept.Any(m => m.MediaType.Contains("application/json"));

            if (isApiClient)
            {
                // Return JSON response for API clients
                return Ok(new
                {
                    Message = "Cookie setup and script loading completed successfully.",
                    Token = token,
                    Role = role
                });
            }
            else
            {
                // Redirect browser-based requests to the GUI
                var redirectUrl = $"http://localhost:58047/default.aspx?token={HttpUtility.UrlEncode(token)}&role={role}";
                return Redirect(redirectUrl);
            }
        }
    }
}