using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPA_Handler.Models
{
    public class UserDashboardDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Page { get; set; }
        public List<string> AllowedSections { get; set; }
        public List<string> AsyncScripts { get; set; }
        public List<string> SidebarOptions { get; set; }

        // ✅ Fields that GUI will update before sending back
        public bool IsSetupComplete { get; set; }  // GUI sets this to true after processing
        public List<string> LoadedModules { get; set; } // GUI will populate based on actual loaded modules
        public string UserPreference { get; set; } // Example: Dark mode, layout selection, etc.

    }
}