using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Text.Json;
using TPA_Server.Helpers;
using TPA_Server.Modals;
using TPA_Server.Services;

namespace TPA_Server.Controllers
{
    /// <summary>
    /// Controller responsible for handling user authentication and login operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Dependencies
        private readonly Services.WebSocketManager _webSocketManager;
        private readonly ILogger<AuthController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="webSocketManager">Service for managing WebSocket connections and communication.</param>
        /// <param name="logger">Logger for capturing runtime events and errors.</param>
        public AuthController(
            Services.WebSocketManager webSocketManager,
            ILogger<AuthController> logger)
        {
            _webSocketManager = webSocketManager;
            _logger = logger;
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Handles user login requests. Validates credentials and generates a JWT token.
        /// </summary>
        /// <param name="model">The login credentials (username and password).</param>
        /// <returns>
        /// Returns a JWT token and user DTO if login is successful.
        /// If the credentials are invalid, returns a 401 Unauthorized response.
        /// If the DTO confirmation times out, returns a warning message.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation("Login attempt for {Username}", model.Username);

            // Validate credentials (hardcoded for demonstration purposes)
            if (model.Username == "string" && model.Password == "string")
            {
                // Generate a JWT token for the authenticated user
                var token = JwtConfig.GenerateToken(model.Username);
                _logger.LogInformation("Generated JWT token for {Username}", model.Username);

                // Create a TaskCompletionSource to await DTO confirmation from the GUI
                var tcs = new TaskCompletionSource<UserDashboardDTO>();
                _webSocketManager.AddPendingToken(token, tcs);

                // Send the token to the GUI via WebSocket
                _webSocketManager.SendTokenToClients($"TOKEN:{token}");

                // Wait for either the DTO confirmation or a timeout (30 seconds)
                var timeout = Task.Delay(TimeSpan.FromSeconds(30));
                var completedTask = await Task.WhenAny(tcs.Task, timeout);

                // Clean up the pending token
                _webSocketManager.RemovePendingToken(token);

                // Check if the DTO confirmation was received
                if (completedTask == tcs.Task)
                {
                    return Ok(new
                    {
                        message = "Full login flow completed",
                        token,
                        dto = await tcs.Task
                    });
                }

                // Handle timeout scenario
                return Ok(new
                {
                    message = "Login successful but DTO confirmation timeout",
                    token,
                    warning = "GUI didn't confirm DTO processing within 30 seconds"
                });
            }

            // Return unauthorized if credentials are invalid
            return Unauthorized();
        }
        #endregion

        #region Models
        /// <summary>
        /// Represents the login credentials provided by the user.
        /// </summary>
        public class LoginModel
        {
            /// <summary>
            /// The username provided by the user.
            /// </summary>
            public string Username { get; set; }

            /// <summary>
            /// The password provided by the user.
            /// </summary>
            public string Password { get; set; }
        }
        #endregion
    }
}