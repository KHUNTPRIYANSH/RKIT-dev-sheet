using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Phase_3_Final_Demo.Helpers
{
    /// <summary>
    /// A helper class for generating and validating JSON Web Tokens (JWTs).
    /// </summary>
    public static class JwtHelper
    {
        // Secret key used for signing the JWT (keep this secure and do not expose it in your code)
        private const string SecretKey = "Priyansh Khunt's Secret-Key12345";

        // Token issuer (who issued the token)
        private const string Issuer = "KPD";

        // Token audience (who the token is intended for)
        private const string Audience = "users";

        #region Generate Token

        /// <summary>
        /// Generates a JWT token for the specified username and role.
        /// </summary>
        /// <param name="username">The username for which the token is generated.</param>
        /// <param name="role">The role of the user (e.g., Admin, Editor, User).</param>
        /// <returns>A string representing the generated JWT token.</returns>
        public static string GenerateJwtToken(string username, string role)
        {
            // Create a symmetric security key using the secret key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            // Define signing credentials using the security key and HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Define the claims (data) to include in the token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username), // The name of the user
                new Claim(ClaimTypes.Role, role)     // The user's role
            };

            // Create the JWT token with issuer, audience, claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60), // Token is valid for 60 minutes
                signingCredentials: credentials
            );

            // Return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        #region Validate Token

        /// <summary>
        /// Validates a JWT token and extracts the claims principal if valid.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>
        /// A <see cref="ClaimsPrincipal"/> containing the claims if the token is valid.
        /// Throws an exception if the token is invalid or expired.
        /// </returns>
        public static ClaimsPrincipal ValidateJwtToken(string token)
        {
            // Create a token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Convert the secret key into a byte array
            var key = Encoding.UTF8.GetBytes(SecretKey);

            // Define token validation parameters
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, // Ensure the token's issuer matches the expected issuer
                ValidateAudience = true, // Ensure the token's audience matches the expected audience
                ValidateIssuerSigningKey = true, // Ensure the token's signature is valid
                ValidIssuer = Issuer, // The expected issuer
                ValidAudience = Audience, // The expected audience
                IssuerSigningKey = new SymmetricSecurityKey(key) // The security key used to sign the token
            };

            // Validate the token and return the claims principal
            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }

        #endregion
    }
}
