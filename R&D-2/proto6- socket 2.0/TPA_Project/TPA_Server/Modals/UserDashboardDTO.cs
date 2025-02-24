namespace TPA_Server.Modals
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the user's dashboard configuration (DTO).
    /// </summary>
    public class UserDashboardDTO
    {
        #region Properties
        /// <summary>
        /// The username of the user.
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";

        /// <summary>
        /// The role of the user (e.g., Admin, Editor, User).
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = "";

        /// <summary>
        /// The main page URL for the user's dashboard.
        /// </summary>
        [JsonPropertyName("page")]
        public string Page { get; set; } = "";

        /// <summary>
        /// List of sections the user is allowed to access.
        /// </summary>
        [JsonPropertyName("allowedSections")]
        public List<string> AllowedSections { get; set; } = new();

        /// <summary>
        /// List of asynchronous scripts to load for the user's dashboard.
        /// </summary>
        [JsonPropertyName("asyncScripts")]
        public List<string> AsyncScripts { get; set; } = new();

        /// <summary>
        /// List of sidebar options available to the user.
        /// </summary>
        [JsonPropertyName("sidebarOptions")]
        public List<string> SidebarOptions { get; set; } = new();

        /// <summary>
        /// Indicates whether the user's dashboard setup is complete.
        /// </summary>
        [JsonPropertyName("isSetupComplete")]
        public bool IsSetupComplete { get; set; }

        /// <summary>
        /// List of modules that have been successfully loaded.
        /// </summary>
        [JsonPropertyName("loadedModules")]
        public List<string> LoadedModules { get; set; } = new();

        /// <summary>
        /// The user's preference settings (if any).
        /// </summary>
        [JsonPropertyName("userPreference")]
        public string UserPreference { get; set; } = "";
        #endregion
    }
}