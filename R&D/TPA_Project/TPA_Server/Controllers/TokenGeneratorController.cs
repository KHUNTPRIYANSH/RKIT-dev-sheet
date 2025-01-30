using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using TPA_Server.Helpers;
using TPA_Server.Models;

namespace TPA_Server.Controllers
{
    [RoutePrefix("api/token-generator")]
    public class TokenGeneratorController : ApiController
    {
        private readonly DbHelper _dbHelper;

        public TokenGeneratorController()
        {
            _dbHelper = new DbHelper(); // Initialize the DbHelper class
        }

        // POST: api/token-generator/generate/{id}
        [HttpPost]
        [Route("generate/{id:int}")]
        public IHttpActionResult GenerateToken(int id)
        {
            // Retrieve user details by ID
            AuthModel user = _dbHelper.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            // Generate JWT token with claims
            string token = GenerateJwtToken(user);

            return Ok(new
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(user.TokenExpiryInMinutes)
            });
        }

        private string GenerateJwtToken(AuthModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTHelper.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("TokenExpiry", DateTime.UtcNow.AddMinutes(user.TokenExpiryInMinutes).ToString())
            };

            // Add dependencies as multiple claims
            foreach (var dependency in user.Dependencies)
            {
                claims.Add(new Claim("Dependency", dependency));
            }

            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(user.TokenExpiryInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
