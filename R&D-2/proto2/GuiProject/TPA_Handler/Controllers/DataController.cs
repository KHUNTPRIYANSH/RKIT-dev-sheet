using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using TPA_Handler.Services;

namespace TPA_Handler.Controllers
{
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("api/data/getdto")]
        public IHttpActionResult GetDTO()
        {
            var authHeader = Request.Headers.Authorization;
            if (authHeader == null || !authHeader.Scheme.Equals("Bearer") || string.IsNullOrEmpty(authHeader.Parameter))
            {
                return Content(HttpStatusCode.Unauthorized, "Missing or invalid Authorization header");
            }

            var principal = AuthService.ValidateToken(authHeader.Parameter);
            if (principal == null)
            {
                return Content(HttpStatusCode.Unauthorized, "Invalid token");
            }

            var username = principal.Identity.Name;
            var userDto = AuthService.GetUserDashboard(username);

            if (userDto == null)
            {
                return Content(HttpStatusCode.Unauthorized, "User not found");
            }

            return Ok(userDto);
        }
    }
}