using TPA_Server.Modals;

namespace TPA_Server.Services
{
    /// <summary>
    /// Service responsible for fetching user-specific dashboard data (DTO).
    /// </summary>
    public class UserService
    {
        #region User Roles and Dashboard Configuration
        /// <summary>
        /// Fetches the user's dashboard DTO based on their username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>
        /// Returns a <see cref="UserDashboardDTO"/> object containing the user's dashboard configuration.
        /// If the username is not found, returns null.
        /// </returns>
        public UserDashboardDTO? GetUserDashboard(string username)
        {
            // Define user roles and their corresponding dashboard configurations
            var roles = new Dictionary<string, string>
            {
                ["priyansh"] = "User",
                ["romil"] = "Editor",
                ["string"] = "Admin"
            };

            // Check if the username exists in the roles dictionary
            if (!roles.TryGetValue(username, out var role))
            {
                return null; // Return null if the username is not found
            }

            // Return the user's dashboard DTO based on their role
            return new UserDashboardDTO
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
        #endregion
    }
}