namespace TPA_Server.Modals
{
    using System.Text.Json.Serialization;

    public class UserDashboardDTO
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";

        [JsonPropertyName("role")]
        public string Role { get; set; } = "";

        [JsonPropertyName("page")]
        public string Page { get; set; } = "";

        [JsonPropertyName("allowedSections")]
        public List<string> AllowedSections { get; set; } = new();

        [JsonPropertyName("asyncScripts")]
        public List<string> AsyncScripts { get; set; } = new();

        [JsonPropertyName("sidebarOptions")]
        public List<string> SidebarOptions { get; set; } = new();

        [JsonPropertyName("isSetupComplete")]
        public bool IsSetupComplete { get; set; }

        [JsonPropertyName("loadedModules")]
        public List<string> LoadedModules { get; set; } = new();

        [JsonPropertyName("userPreference")]
        public string UserPreference { get; set; } = "";
    }

}
