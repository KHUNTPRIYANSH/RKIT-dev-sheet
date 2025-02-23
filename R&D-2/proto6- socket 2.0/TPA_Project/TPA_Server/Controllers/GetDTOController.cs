using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using TPA_Server.Modals;
using TPA_Server.Services;

namespace TPA_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GetDTOController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly ILogger<GetDTOController> _logger;

        public GetDTOController(UserService userService, ILogger<GetDTOController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetUserDashboard()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            _logger.LogInformation("DTO request for {Username}", username);

            if (string.IsNullOrEmpty(username))
            {
                _logger.LogInformation("Unauthorized DTO request");
                return Unauthorized();
            }

            var userDto = _userService.GetUserDashboard(username);
            return userDto != null
                ? Ok(userDto)
                : NotFound(new { message = "User not found" });
        }
    }
}



