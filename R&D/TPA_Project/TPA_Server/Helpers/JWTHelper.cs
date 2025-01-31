using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TPA_Server.Models;

namespace TPA_Server.Helpers
{
    /// <summary>
    /// Helper class for generating and validating JWT tokens.
    /// </summary>
    public class JWTHelper
    {
        public const string SecretKey = "Priyansh Khunt's Secret-Key12345"; // The secret key used for token validation.
        private static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        /// <summary>
        /// Generates a JWT token with user details from AuthModel.
        /// </summary>
        /// <param name="user">The AuthModel containing user details.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        public static string GenerateJwtToken(AuthModel user)
        {
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

            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(user.TokenExpiryInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validates a JWT token and returns the ClaimsPrincipal if valid.
        /// </summary>
        /// <param name="token">The JWT token to be validated.</param>
        /// <returns>A ClaimsPrincipal if the token is valid, otherwise null.</returns>
        public static ClaimsPrincipal ValidateJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience",
                    IssuerSigningKey = Key
                };

                SecurityToken validatedToken;
                var principal = handler.ValidateToken(token, parameters, out validatedToken);

                return principal;
            }
            catch
            {
                return null; // Invalid token
            }
        }

        public static AuthModel ExtractAuthModelFromToken(string token)
        {
            var principal = ValidateJwtToken(token);
            if (principal == null) return null;

            var claims = principal.Claims.ToList();

            var authModel = new AuthModel
            {
                Id = int.TryParse(claims.FirstOrDefault(c => c.Type == "Id")?.Value, out int id) ? id : 0,
                Username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                Password = claims.FirstOrDefault(c => c.Type == "Password")?.Value, // Extract password
                Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                TokenExpiryInMinutes = int.TryParse(claims.FirstOrDefault(c => c.Type == "TokenExpiry")?.Value, out int expiry) ? expiry : 0,
                Dependencies = claims.Where(c => c.Type == "Dependency").Select(c => c.Value).ToList()
            };

            return authModel;
        }


    }
}
