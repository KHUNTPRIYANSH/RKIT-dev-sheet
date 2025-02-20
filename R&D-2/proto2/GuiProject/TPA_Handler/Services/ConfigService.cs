using System;
using System.Diagnostics;
using TPA_Handler.Models;

namespace TPA_Handler.Services
{
    public class ConfigService
    {
        public static void ProcessUpdatedDTO(UserDashboardDTO dto)
        {
            Debug.WriteLine($"User: {dto.Username} setup complete: {dto.IsSetupComplete}");
            Debug.WriteLine($"Loaded Modules: {string.Join(", ", dto.LoadedModules)}");
            Debug.WriteLine($"User Preference: {dto.UserPreference}");
        }
    }
}
