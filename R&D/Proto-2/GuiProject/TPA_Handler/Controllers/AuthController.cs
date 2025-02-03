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
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/auth/login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            var userDashboard = AuthService.Authenticate(request.Username, request.Password);
            if (userDashboard != null)
            {
                return Ok(userDashboard); // ✅ GUI will now process this DTO
            }
            return Unauthorized();
        }

    }
}
