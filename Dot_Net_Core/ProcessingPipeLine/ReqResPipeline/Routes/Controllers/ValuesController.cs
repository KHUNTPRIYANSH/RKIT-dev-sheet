using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Routes.Controllers
{
    /// <summary>
    /// Handles operations related to values in the API.
    /// </summary>
    [Route("api/[controller]")] // Base route for the controller, where [controller] is replaced with the controller name (Values).
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Retrieves a list of predefined values.
        /// </summary>
        /// <returns>An array of strings representing values.</returns>
        [HttpGet] // Matches GET requests to "api/values".
        public ActionResult<IEnumerable<string>> GetAllValues()
        {
            // Returning a static array of values.
            return Ok(new[] { "value1", "value2", "value3" });
        }

        /// <summary>
        /// Retrieves a specific value by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the value.</param>
        /// <returns>The corresponding value or a Not Found result if invalid.</returns>
        [HttpGet("{id:int:min(1):max(50)}")] // Matches GET requests to "api/values/{id}" with ID restricted to 1-50.
        public ActionResult<string> GetValueById(int id)
        {
            // Simulating retrieval of a value by ID.
            return Ok($"value{id}");
        }

        /// <summary>
        /// Adds a new value to the system.
        /// </summary>
        /// <param name="value">The value to be added.</param>
        [HttpPost]
        [Route("~/api/custom-post-route")] // Matches POST requests to "api/custom-post-route" instead of the default route.
        public IActionResult CreateValue([FromBody] string value)
        {
            // Simulating creation logic.
            return CreatedAtAction(nameof(GetValueById), new { id = 1 }, value);
        }

        /// <summary>
        /// Updates an existing value based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the value to update.</param>
        /// <param name="value">The updated value.</param>
        [HttpPut("{id:int?}")] // Matches PUT requests to "api/values/{id}", where ID is optional.
        public IActionResult UpdateValue(int? id, [FromBody] string value)
        {
            if (id == null)
            {
                return BadRequest("ID is required.");
            }

            // Simulating update logic.
            return NoContent(); // Indicates successful update with no content to return.
        }

        /// <summary>
        /// Deletes a value using its ID.
        /// </summary>
        /// <param name="id">The ID of the value to delete. Defaults to 1 if not specified.</param>
        [HttpDelete("{id:int=1}")] // Matches DELETE requests to "api/values/{id}", with default ID as 1.
        public IActionResult DeleteValue(int id)
        {
            // Simulating delete logic.
            return Ok($"Value with ID {id} has been deleted.");
        }
    }
}
