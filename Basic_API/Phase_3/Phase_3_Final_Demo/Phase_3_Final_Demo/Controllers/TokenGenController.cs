using System.Web.Http;
using Phase_3_Final_Demo.Helpers;
using Phase_3_Final_Demo.Models;

namespace Phase_3_Final_Demo.Controllers
{
    /// <summary>
    /// Controller responsible for user authentication and generating JWT tokens.
    /// </summary>
    public class TokenGenController : ApiController
    {
        #region Login Endpoint

        /// <summary>
        /// Authenticates the user and generates a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="userInfo">The user login information (username and password).</param>
        /// <returns>
        /// Returns an HTTP response with a JWT token if the credentials are valid; otherwise, returns Unauthorized.
        /// </returns>
        [HttpPost]
        [Route("api/auth/login")] // Define the API route for login
        public IHttpActionResult Login([FromBody] Login userInfo)
        {
            // Check if the provided username and password match "admin" credentials
            if (userInfo.UserName == "admin" && userInfo.Password == "admin@123")
            {
                // Generate a token for the admin user
                var token = JwtHelper.GenerateJwtToken(userInfo.UserName, "Admin");

                // Return the generated token in the response
                return Ok(new { Token = token });
            }

            // Check if the provided username and password match "editor" credentials
            if (userInfo.UserName == "editor" && userInfo.Password == "editor@123")
            {
                // Generate a token for the editor user
                var token = JwtHelper.GenerateJwtToken(userInfo.UserName, "Editor");

                // Return the generated token in the response
                return Ok(new { Token = token });
            }

            // Check if the provided username and password match "user" credentials
            if (userInfo.UserName == "user" && userInfo.Password == "user@123")
            {
                // Generate a token for the regular user
                var token = JwtHelper.GenerateJwtToken(userInfo.UserName, "User");

                // Return the generated token in the response
                return Ok(new { Token = token });
            }

            // If the credentials don't match any valid user, return Unauthorized
            return Unauthorized();
        }

        #endregion
    }
}
