using TPA_Server.Modals;

namespace TPA_Server.Services
{
    public class UserService
    {
        public UserDashboardDTO? GetUserDashboard(string username)
        {
            var roles = new Dictionary<string, string>
            {
                ["priyansh"] = "User",
                ["romil"] = "Editor",
                ["string"] = "Admin"
            };

            return !roles.TryGetValue(username, out var role) ? null : new UserDashboardDTO
            {
                Username = username,
                Role = role,
                Page = role switch
                {
                    "Admin" => "/Pages/admin-dashboard.aspx",
                    "Editor" => "/Pages/editor-dashboard.aspx",
                    _ => "/Pages/home.aspx"
                },
                AllowedSections = role switch
                {
                    "Admin" => new List<string> { "Settings" },
                    "Editor" => new List<string> { "Posts", "Analytics" },
                    _ => new List<string> { "Profile", "Settings" }
                },
                AsyncScripts = role switch
                {
                    "Admin" => new List<string> { "/scripts/admin-controls.js" },
                    "Editor" => new List<string> { "/scripts/editor-tools.js" },
                    _ => new List<string> { "/scripts/dashboard.js" }
                },
                SidebarOptions = role switch
                {
                    "Admin" => new List<string> { "Dashboard", "Users", "Settings", "Reports" },
                    "Editor" => new List<string> { "Dashboard", "Posts", "Analytics" },
                    _ => new List<string> { "Home", "Settings" }
                },
                IsSetupComplete = false,
                LoadedModules = new List<string>(),
                UserPreference = ""
            };
        }
    }
}
