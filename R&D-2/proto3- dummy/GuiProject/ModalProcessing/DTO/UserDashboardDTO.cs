using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModalProcessing.DTO
{
    public class UserDashboardDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Page { get; set; }
        public List<string> AllowedSections { get; set; }
        public List<string> AsyncScripts { get; set; }
        public List<string> SidebarOptions { get; set; }
        public bool IsSetupComplete { get; set; }
        public List<string> LoadedModules { get; set; }
        public string UserPreference { get; set; }
    }

}