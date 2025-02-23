using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Text.Json;
using TPA_Server.Helpers;
using TPA_Server.Modals;
using TPA_Server.Services;

namespace TPA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Services.WebSocketManager _webSocketManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            Services.WebSocketManager webSocketManager,
            ILogger<AuthController> logger)
        {
            _webSocketManager = webSocketManager;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            _logger.LogInformation("Login attempt for {Username}", model.Username);

            if (model.Username == "string" && model.Password == "string")
            {
                var token = JwtConfig.GenerateToken(model.Username);
                _logger.LogInformation("Generated JWT token for {Username}", model.Username);

                var tcs = new TaskCompletionSource<UserDashboardDTO>();
                _webSocketManager.AddPendingToken(token, tcs);

                // Send token through WebSocket with prefix
                _webSocketManager.SendTokenToClients($"TOKEN:{token}");

                var timeout = Task.Delay(TimeSpan.FromSeconds(30));
                var completedTask = await Task.WhenAny(tcs.Task, timeout);

                _webSocketManager.RemovePendingToken(token);

                if (completedTask == tcs.Task)
                {
                    return Ok(new
                    {
                        message = "Full login flow completed",
                        token,
                        dto = await tcs.Task
                    });
                }

                return Ok(new
                {
                    message = "Login successful but DTO confirmation timeout",
                    token,
                    warning = "GUI didn't confirm DTO processing within 30 seconds"
                });
            }
            return Unauthorized();
        }

    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}