using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Controllers
{/// <summary>
 /// Controller to handle authentication-related actions.
 /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Endpoint to get a message if the user is authenticated.
        /// </summary>
        /// <returns>Returns a message indicating the user is authenticated.</returns>
        [HttpGet]
        [Authorize] // This attribute enforces that the user must be authenticated to access this endpoint.
        public IActionResult Get()
        {
            // Return a message indicating successful authentication.
            return Ok("You are authenticated!");
        }
    }
}
