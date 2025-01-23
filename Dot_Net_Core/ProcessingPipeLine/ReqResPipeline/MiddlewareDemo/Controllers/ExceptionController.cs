using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Controllers
{
    /// <summary>
    /// Controller to handle exception-related actions.
    /// </summary>
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        /// <summary>
        /// Endpoint to trigger a test exception.
        /// </summary>
        /// <returns>Throws an exception to demonstrate error handling.</returns>
        [HttpGet]
        [Route("error")]
        public IActionResult Get()
        {
            // Throw an exception to demonstrate error handling.
            throw new Exception("This is a test exception!");
        }
    }
}
