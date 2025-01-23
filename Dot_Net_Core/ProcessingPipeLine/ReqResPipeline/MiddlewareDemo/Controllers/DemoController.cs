using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Controllers
{
    /// <summary>
    /// Controller to handle demo-related actions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        /// <summary>
        /// Endpoint to get a greeting message from the Demo Controller.
        /// </summary>
        /// <returns>Returns a greeting message.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            // Return a greeting message.
            return Ok("Hello from Demo Controller!");
        }

        /// <summary>
        /// Endpoint to get information about the demo.
        /// </summary>
        /// <returns>Returns an about message.</returns>
        [HttpGet("about")]
        public IActionResult About()
        {
            // Return an about message.
            return Ok("About page");
        }
    }
}
