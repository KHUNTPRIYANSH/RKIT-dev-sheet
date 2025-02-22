using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using TPA_Server.Modals;

namespace TPA_Server.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class GetDTOController : ControllerBase
    {
        [HttpGet("getdto")]
        //[Authorize] // Ensures only authenticated users can access
        public IActionResult GetDTO()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(new { message = "Invalid token or user not found." });
            }

            var userDto = GetUserDashboard(username);

            if (userDto == null)
            {
                return NotFound(new { message = "User DTO not found." });
            }

            return Ok(userDto);
        }

        private UserDashboardDTO GetUserDashboard(string username)
        {
            // Mock user roles for simplicity
            var userRoles = new Dictionary<string, string>
            {
                { "priyansh", "User" },
                { "romil", "Editor" },
                { "string", "Admin" }
            };

            if (!userRoles.ContainsKey(username))
            {
                return null;
            }

            var role = userRoles[username];
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
