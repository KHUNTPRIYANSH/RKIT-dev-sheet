using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TPA_Handler.Models;

namespace TPA_Handler.Services
{
    public class AuthService
    {
        public const string SecretKey = "Priyansh Khunt's Secret-Key12345";

        private static readonly Dictionary<string, (string password, string role)> users = new Dictionary<string, (string password, string role)>
        {
            { "priyansh", ("password123", "User") },
            { "romil", ("password123", "Editor") },
            { "string", ("string", "Admin") }
        };

        public static (string Username, string Role)? GetUserDetails(string username, string password)
        {
            if (users.TryGetValue(username, out var userData) && userData.password == password)
            {
                return (username, userData.role);
            }
            return null;
        }

        public static string GenerateJwtToken(string username, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: "yourapp.com",
                audience: "yourapp.com",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Token expires in 2 hours
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static UserDashboardDTO GetUserDashboard(string username)
        {
            if (!users.ContainsKey(username))
                return null;

            var role = users[username].role;
            string page = "/Pages/home.aspx";
            List<string> allowedSections = new List<string>();
            List<string> asyncScripts = new List<string>();
            List<string> sidebarOptions = new List<string>();

            if (role == "User")
            {
                page = "/Pages/home.aspx";
                allowedSections = new List<string> { "Profile", "Settings" };
                asyncScripts = new List<string> { "/scripts/dashboard.js" };
                sidebarOptions = new List<string> { "Home", "Settings" };
            }
            else if (role == "Editor")
            {
                page = "/Pages/editor-dashboard.aspx";
                allowedSections = new List<string> { "Posts", "Analytics" };
                asyncScripts = new List<string> { "/scripts/editor-tools.js" };
                sidebarOptions = new List<string> { "Dashboard", "Posts", "Analytics" };
            }
            else if (role == "Admin")
            {
                page = "/Pages/admin-dashboard.aspx";
                allowedSections = new List<string> { "Settings" };
                asyncScripts = new List<string> { "/scripts/admin-controls.js" };
                sidebarOptions = new List<string> { "Dashboard", "Users", "Settings", "Reports" };
            }

            return new UserDashboardDTO
            {
                Username = username,
                Role = role,
                Page = page,
                AllowedSections = allowedSections,
                AsyncScripts = asyncScripts,
                SidebarOptions = sidebarOptions,
                IsSetupComplete = false,
                LoadedModules = new List<string>(),
                UserPreference = ""
            };
        }
    }
}