using Microsoft.IdentityModel.Tokens;
using Phase_3_Final_Demo.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace Phase_3_Final_Demo.Controllers
{
        /// <summary>
        /// Controller for handling authentication and secure data retrieval.
        /// </summary>
        public class TokenGenController : ApiController
        {
            #region Login Endpoint

            /// <summary>
            /// Authenticates the user and generates a JWT token.
            /// </summary>
            /// <param name="user">User details including username and password.</param>
            /// <returns>An action result containing the JWT token if authentication is successful.</returns>
            [HttpPost]
            [Route("api/auth/login")]
            public IHttpActionResult Login([FromBody] Login userInfo)
            {
                // Validate user credentials (hardcoded for demonstration purposes)
                if (userInfo.UserName != "root" || userInfo.Password != "admin@123")
                    return Unauthorized();

                // Generate JWT token
                var token = GenerateJwtToken(userInfo.UserName);
                return Ok(new { Token = token });
            }

            #endregion

            #region JWT Token Generation

            /// <summary>
            /// Generates a JWT token for the authenticated user.
            /// </summary>
            /// <param name="username">The username of the authenticated user.</param>
            /// <returns>A JWT token as a string.</returns>
            private string GenerateJwtToken(string username)
            {
                // Define the secret key and signing credentials
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your-32-Character-Secret-Key1234"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Create claims for the token
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

                // Create the token
                var token = new JwtSecurityToken(
                    issuer: "YourIssuer",
                    audience: "YourAudience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials
                );

                // Return the serialized token
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            #endregion

        }
    
}

