using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Controllers
{
    /// <summary>
    /// Controller to handle authorization-related actions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// Endpoint to get a message if the user is authorized as an Admin.
        /// </summary>
        /// <returns>Returns a welcome message for Admin users.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")] // This attribute enforces that only users with the "Admin" role can access this endpoint.
        public IActionResult Get()
        {
            // Return a welcome message for Admin users.
            return Ok("Welcome, Admin!");
        }
    }
}
