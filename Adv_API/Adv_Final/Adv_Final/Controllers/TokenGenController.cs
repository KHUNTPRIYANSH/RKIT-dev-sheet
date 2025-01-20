using Microsoft.IdentityModel.Tokens;
using Adv_Final.Models.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Adv_Final.Models;
using Adv_Final.Extensions;
using System.Data.Odbc;
using ServiceStack.Data;
using System.Web;
using Adv_Final.Models.POCO;
using Adv_Final.Helpers;
using ServiceStack.OrmLite;

namespace Adv_Final.Controllers
{
    /// <summary>
    /// Controller responsible for generating JWT tokens for user authentication.
    /// </summary>
    public class TokenGenController : ApiController
    {
        private readonly IDbConnectionFactory _dbFactory;
        public TokenGenController()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

        }

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
        public IHttpActionResult Login([FromBody] DTOUserLogin userInfo)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Fetch user from the database based on username
                var user = db.Single<USR01>(u => u.R01F02 == userInfo.UserName);

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



        #endregion

        //#region Token Generation Helper

        ///// <summary>
        ///// Generates a JSON Web Token (JWT) for a specific user with a role.
        ///// </summary>
        ///// <param name="username">The username for which the token is generated.</param>
        ///// <param name="role">The role of the user.</param>
        ///// <returns>A JWT token as a string.</returns>
        //private string GenerateJwtToken(string username, string role)
        //{
        //    // Security key used for signing the JWT
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your-32-Character-Secret-Key1234"));

        //    // Signing credentials
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    // Claims associated with the token
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, username),
        //        new Claim(ClaimTypes.Role, role)
        //    };

        //    // Create the JWT token
        //    var token = new JwtSecurityToken(
        //        issuer: "YourIssuer", // Issuer of the token
        //        audience: "YourAudience", // Audience of the token
        //        claims: claims, // Claims contained in the token
        //        expires: DateTime.Now.AddMinutes(60), // Expiration time of the token
        //        signingCredentials: credentials // Signing credentials
        //    );

        //    // Return the token as a string
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //#endregion
    }
}
