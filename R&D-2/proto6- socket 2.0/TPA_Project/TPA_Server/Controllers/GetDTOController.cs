using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using TPA_Server.Modals;
using TPA_Server.Services;

namespace TPA_Server.Controllers
{
    /// <summary>
    /// Controller responsible for fetching the user's dashboard DTO (Data Transfer Object).
    /// This endpoint requires authentication and authorization.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GetDTOController : ControllerBase
    {
        #region Dependencies
        private readonly UserService _userService;
        private readonly ILogger<GetDTOController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDTOController"/> class.
        /// </summary>
        /// <param name="userService">Service for fetching user-specific data.</param>
        /// <param name="logger">Logger for capturing runtime events and errors.</param>
        public GetDTOController(UserService userService, ILogger<GetDTOController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Fetches the user's dashboard DTO based on their authenticated identity.
        /// </summary>
        /// <returns>
        /// Returns the user's dashboard DTO if found, otherwise returns a 404 Not Found response.
        /// If the user is unauthorized, returns a 401 Unauthorized response.
        /// </returns>
        [HttpGet]
        public IActionResult GetUserDashboard()
        {
            // Extract the username from the authenticated user's claims
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            _logger.LogInformation("DTO request for {Username}", username);

            // Validate the username
            if (string.IsNullOrEmpty(username))
            {
                _logger.LogInformation("Unauthorized DTO request");
                return Unauthorized();
            }

            // Fetch the user's dashboard DTO from the service
            var userDto = _userService.GetUserDashboard(username);

            // Return the DTO if found, otherwise return a 404 response
            return userDto != null
                ? Ok(userDto)
                : NotFound(new { message = "User not found" });
        }
        #endregion
    }
}