using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ActionMethodDemo.Controllers
{
    /// <summary>
    /// Demonstrates various Action Method Results in ASP.NET Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ActionMethodController : ControllerBase
    {
        #region Public Methods

        /// <summary>
        /// Returns JSON data, useful for client-side JavaScript.
        /// </summary>
        /// <returns>JSON data.</returns>
        [HttpGet("JsonResult")]
        public IActionResult GetJsonData()
        {
            var data = new
            {
                Key1 = "Value1",
                Key2 = "Value2"
            };
            return Ok(data);
        }

        /// <summary>
        /// Redirects the user to Google.
        /// </summary>
        /// <returns>Redirect result.</returns>
        [HttpGet("RedirectToGoogle")]
        public IActionResult RedirectToGoogle()
        {
            return Redirect("https://www.google.com/");
        }

        /// <summary>
        /// Returns plain text content.
        /// </summary>
        /// <returns>Plain text content.</returns>
        [HttpGet("TextContent")]
        public IActionResult TextContent()
        {
            const string message = "This is plain text content.";
            return Content(message, "text/plain");
        }

        /// <summary>
        /// Demonstrates file download functionality.
        /// </summary>
        /// <returns>File download response.</returns>
        [HttpGet("DownloadFile")]
        public IActionResult DownloadFile()
        {
            var relativePath = Path.Combine("Docs", "Resume.pdf");
            var filePath = Path.GetFullPath(relativePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "Resume.pdf");
        }

        /// <summary>
        /// Demonstrates returning HTTP status codes.
        /// </summary>
        /// <returns>404 Not Found status code.</returns>
        [HttpGet("HttpStatusCodes")]
        public IActionResult HttpStatusCodes()
        {
            return NotFound(); // Status code 404
        }

        /// <summary>
        /// Demonstrates using ActionResult for specific status codes.
        /// </summary>
        /// <returns>401 Unauthorized status code.</returns>
        [HttpGet("FromActionResultClass")]
        public ActionResult FromActionResultClass()
        {
            return Unauthorized(); // Status code 401
        }

        /// <summary>
        /// Demonstrates returning a created result with a URI.
        /// </summary>
        /// <returns>201 Created result with location URI.</returns>
        [HttpPost("CreatedExample")]
        public IActionResult CreatedExample()
        {
            var newResource = new
            {
                Id = 1,
                Name = "Sample Resource"
            };

            return CreatedAtAction(nameof(GetJsonData), new { id = newResource.Id }, newResource);
        }

        /// <summary>
        /// Demonstrates returning a custom error response.
        /// </summary>
        /// <returns>400 Bad Request with error message.</returns>
        [HttpGet("CustomError")]
        public IActionResult CustomError()
        {
            var errorResponse = new
            {
                ErrorCode = "ERR001",
                ErrorMessage = "Invalid operation"
            };

            return BadRequest(errorResponse);
        }

        /// <summary>
        /// Demonstrates returning a file stream.
        /// </summary>
        /// <returns>File stream result.</returns>
        [HttpGet("DownloadFileStream")]
        public IActionResult DownloadFileStream()
        {
            var relativePath = Path.Combine("Docs", "Resume.pdf");
            var filePath = Path.GetFullPath(relativePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, "application/pdf", "Resume.pdf");
        }

        /// <summary>
        /// Demonstrates returning a No Content response.
        /// </summary>
        /// <returns>204 No Content status code.</returns>
        [HttpDelete("NoContentResponse")]
        public IActionResult NoContentResponse()
        {
            // Simulating a successful delete operation
            return NoContent(); // Status code 204
        }

       
        #endregion
    }
}
