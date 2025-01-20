using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Adv_Final.Models.Enum;
using Microsoft.IdentityModel.Tokens;

namespace Adv_Final.Helpers
{
    /// <summary>
    /// Helper class for generating and validating JWT tokens.
    /// </summary>
    public class JWTHelper
    {
        private const string SecretKey = "RuyekAvdaras417107701741SaradvaKeyur"; // The secret key used for token validation.

        /// <summary>
        /// Validates the JWT token and returns the ClaimsPrincipal if valid.
        /// </summary>
        /// <param name="token">The JWT token to be validated.</param>
        /// <returns>A ClaimsPrincipal if the token is valid, otherwise null.</returns>
        public static ClaimsPrincipal ValidateJwtToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var handler = new JwtSecurityTokenHandler();

            try
            {
                // Define token validation parameters
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience",
                    IssuerSigningKey = key,
                  
                };

                SecurityToken validatedToken;
                // Validate the token and extract the principal (user identity)
                var principal = handler.ValidateToken(token, parameters, out validatedToken);

                // Log token claims for debugging
                Console.WriteLine($"Token validated successfully. Claims: {string.Join(", ", principal.Claims.Select(c => c.Type + ": " + c.Value))}");

                return principal;
            }
            catch (SecurityTokenExpiredException)
            {
                // Token is expired
                Console.WriteLine("Token has expired.");
                return null;
            }
            catch (SecurityTokenException ex)
            {
                // Token validation failed for other reasons
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Generates a JWT token for the specified username, with role claim and expiration.
        /// </summary>
        /// <param name="username">The username for which the token is being generated.</param>
        /// <param name="role">The role to assign to the user in the token.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        public static string GenerateJwtToken(string username, EnmRole role)
        {
            // Convert the role to a string
            string roleString = role.ToString();

            // Define claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, roleString) // Dynamically setting the role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Set expiration time (30 minutes)
                signingCredentials: creds
            );

            // Return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
