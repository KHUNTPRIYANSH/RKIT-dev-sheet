using FinalDemo.Helpers;
using FinalDemo.Models.ENUM;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System;
using System.Net.Http;
using System.Security.Claims;

namespace FinalDemo.Filters
{
    /// <summary>
    /// Custom Authorization Filter to handle JWT token validation and role-based access control.
    /// </summary>
    public class JWTAuthorizationFilter : AuthorizationFilterAttribute
    {
        private readonly EnmRoleType[] _allowedRoles;

        /// <summary>
        /// Initializes a new instance of the <see cref="JWTAuthorizationFilter"/> class with allowed roles.
        /// </summary>
        /// <param name="roles">Array of roles allowed to access the API endpoint.</param>
        public JWTAuthorizationFilter(params EnmRoleType[] roles)
        {
            _allowedRoles = roles;
        }

        /// <summary>
        /// Called when authorization is performed to validate the JWT token and role.
        /// </summary>
        /// <param name="actionContext">The context of the HTTP action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the Authorization header is missing or invalid
            if (actionContext.Request.Headers.Authorization == null ||
                string.IsNullOrWhiteSpace(actionContext.Request.Headers.Authorization.Parameter))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Missing or invalid token.");
                return;
            }

            string token = actionContext.Request.Headers.Authorization.Parameter;

            try
            {
                // Validate the JWT token and retrieve the principal
                var principal = JWTHelper.ValidateJwtToken(token);

                if (principal == null)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid or expired token.");
                    return;
                }

                // Get the user's role from the claims
                var userRole = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userRole))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Role information missing in token.");
                    return;
                }

                // Convert the role string to the Enum type
                if (!Enum.TryParse(userRole, out EnmRoleType roleEnum))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid role in token.");
                    return;
                }

                // Check if the user's role is allowed
                if (_allowedRoles != null && _allowedRoles.Length > 0 && !_allowedRoles.Contains(roleEnum))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Access denied.");
                    return;
                }

            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, $"Invalid token. Error: {ex.Message}");
            }
        }
    }
}
