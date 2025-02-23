using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TPA_Server.Helpers
{
    public static class JwtConfig
    {
        private static readonly string _secretKey = "Priyansh Khunt's Secret-Key12345";
        public static readonly SymmetricSecurityKey SecurityKey = new(Encoding.UTF8.GetBytes(_secretKey));

        public static void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "your_app",
                        ValidAudience = "your_users",
                        IssuerSigningKey = SecurityKey
                    };
                });

            services.AddAuthorization();
        }

        public static string GenerateToken(string username)
        {
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "your_app",
                audience: "your_users",
                claims: new[] { new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, username) },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
