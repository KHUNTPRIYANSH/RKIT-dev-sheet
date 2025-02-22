using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> Login([FromBody] LoginRequest request)
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

            // Construct the redirect URL
            var redirectUrl = $"http://localhost:58047/default.aspx?token={HttpUtility.UrlEncode(token)}&role={role}";

            // Make a server-side HTTP request to the redirect URL to set up the cookie
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(redirectUrl);
                if (!response.IsSuccessStatusCode)
                {
                    return InternalServerError(new Exception("Failed to set up cookie and redirect."));
                }
            }

            // Return a simple "OK" response to Swagger
            return Ok("OK");
        }


        //[HttpPost]
        //[Route("api/auth/login")]
        //public IHttpActionResult Login([FromBody] LoginRequest request)
        //{
        //    if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        //    {
        //        return BadRequest("Username and password are required.");
        //    }

        //    var userDetails = AuthService.GetUserDetails(request.Username, request.Password);

        //    if (userDetails == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var (username, role) = userDetails.Value;
        //    string token = AuthService.GenerateJwtToken(username, role);

        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return InternalServerError(new Exception("Token generation failed."));
        //    }

        //    // Construct the redirect URL
        //    var redirectUrl = $"http://localhost:58047/default.aspx?token={HttpUtility.UrlEncode(token)}&role={role}";

        //    // Return JSON response with the redirect URL and a message
        //    return Ok(new
        //    {
        //        Message = "Setup done",
        //        RedirectUrl = redirectUrl
        //    });
        //}
    }
}