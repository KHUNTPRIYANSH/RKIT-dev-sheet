using Microsoft.IdentityModel.Tokens;
using Phase_3_Final_Demo.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace Phase_3_Final_Demo.Controllers
{
    /// <summary>
    /// Controller responsible for generating JWT tokens for user authentication.
    /// </summary>
    public class TokenGenController : ApiController
    {
        #region Login Endpoint

        /// <summary>
        /// Authenticates a user and generates a JWT token if credentials are valid.
        /// </summary>
        /// <param name="userInfo">Login information containing the username and password.</param>
        /// <returns>
        /// An HTTP response with a JWT token if authentication is successful, or Unauthorized if authentication fails.
        /// </returns>
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] Login userInfo)
        {
            // Validate admin credentials
            if (userInfo.UserName == "admin" && userInfo.Password == "admin@123")
            {
                var token = GenerateJwtToken(userInfo.UserName, "Admin");
                return Ok(new { Token = token });
            }

            // Validate editor credentials
            if (userInfo.UserName == "editor" && userInfo.Password == "editor@123")
            {
                var token = GenerateJwtToken(userInfo.UserName, "Editor");
                return Ok(new { Token = token });
            }

            // Validate user credentials
            if (userInfo.UserName == "user" && userInfo.Password == "user@123")
            {
                var token = GenerateJwtToken(userInfo.UserName, "User");
                return Ok(new { Token = token });
            }

            // Return unauthorized response if credentials are invalid
            return Unauthorized();
        }

        #endregion

        #region Token Generation Helper

        /// <summary>
        /// Generates a JSON Web Token (JWT) for a specific user with a role.
        /// </summary>
        /// <param name="username">The username for which the token is generated.</param>
        /// <param name="role">The role of the user.</param>
        /// <returns>A JWT token as a string.</returns>
        private string GenerateJwtToken(string username, string role)
        {
            // Security key used for signing the JWT
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your-32-Character-Secret-Key1234"));

            // Signing credentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims associated with the token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: "YourIssuer", // Issuer of the token
                audience: "YourAudience", // Audience of the token
                claims: claims, // Claims contained in the token
                expires: DateTime.Now.AddMinutes(60), // Expiration time of the token
                signingCredentials: credentials // Signing credentials
            );

            // Return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
