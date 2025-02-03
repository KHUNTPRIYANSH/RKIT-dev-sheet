using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TPA_Handler.Models;
using TPA_Handler.Services;

namespace TPA_Handler.Controllers
{
    public class ConfigController : ApiController
    {
        [HttpPost]
        [Route("api/config/update")]
        public IHttpActionResult UpdateUserConfig([FromBody] UserDashboardDTO updatedDTO)
        {
            if (updatedDTO == null)
                return BadRequest("Invalid data");

            // 🚀 Process the updated DTO (save to DB, log changes, etc.)
            ConfigService.ProcessUpdatedDTO(updatedDTO);

            return Ok("Configuration updated successfully");
        }

    }
}
