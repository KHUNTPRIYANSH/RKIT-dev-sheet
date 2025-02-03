using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TPA_Handler.Models;

namespace TPA_Handler.Services
{
    public class AuthService
    {
        public static UserDashboardDTO Authenticate(string username, string password)
        {
            var users = new Dictionary<string, (string password, string role)>
        {
            { "priyansh", ("password123", "User") },
            { "romil", ("password123", "Editor") },
            { "admin", ("admin123", "Admin") }
        };

            if (users.TryGetValue(username, out var userData) && userData.password == password)
            {
                string role = userData.role ?? "User"; // ✅ Default to "User" if null

                // ✅ Define defaults
                string page = "/Pages/home.aspx";
                List<string> allowedSections = new List<string>();
                List<string> asyncScripts = new List<string>();
                List<string> sidebarOptions = new List<string>();

                // ✅ Assign based on role
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
                    allowedSections = new List<string> { "Users", "Settings", "Reports" };
                    asyncScripts = new List<string> { "/scripts/admin-controls.js" };
                    sidebarOptions = new List<string> { "Dashboard", "Users", "Settings", "Reports" };
                }

                var UserDTO = new UserDashboardDTO
                {
                    Username = username,
                    Role = role,
                    Page = page,
                    AllowedSections = allowedSections,
                    AsyncScripts = asyncScripts,
                    SidebarOptions = sidebarOptions,

                    // 🚀 Fields GUI will modify
                    IsSetupComplete = false,  // GUI will update this to true after processing
                    LoadedModules = new List<string>(),  // GUI will update based on what is loaded
                    UserPreference = "" // Example: GUI can set "dark" or "light"
                };

                Debug.WriteLine($"DTO [before]" + UserDTO);

                return UserDTO;
            }

            return null;
        }
    }
}