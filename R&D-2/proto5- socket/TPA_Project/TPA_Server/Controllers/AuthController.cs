using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using TPA_Server.Helpers;

namespace TPA_Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        private readonly WebSocketHandler _WebSocketHandler;

        public AuthController(JwtHelper jwtHelper, WebSocketHandler WebSocketHandler)
        {
            _jwtHelper = jwtHelper;
            _WebSocketHandler = WebSocketHandler;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "string" && request.Password == "string") // Replace with DB validation
            {
                var token = _jwtHelper.GenerateToken(request.Username);

                // Send token to GUI via WebSocket
                _WebSocketHandler.SendTokenToGUI(token);

                return Ok(new { Message = "Login successful! Token sent via WebSocket." });
            }

            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
