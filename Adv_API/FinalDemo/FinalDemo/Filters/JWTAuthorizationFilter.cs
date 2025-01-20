using FinalDemo.Helpers;
using FinalDemo.Models.ENUM;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System;
using System.Net.Http;
using System.Security.Claims;

public class JWTAuthorizationFilter : AuthorizationFilterAttribute
{
    private readonly EnmRoleType[] _allowedRoles;

    public JWTAuthorizationFilter(params EnmRoleType[] roles)
    {
        _allowedRoles = roles;
    }

    public override void OnAuthorization(HttpActionContext actionContext)
    {
        if (actionContext.Request.Headers.Authorization == null ||
            string.IsNullOrWhiteSpace(actionContext.Request.Headers.Authorization.Parameter))
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Missing or invalid token.");
            return;
        }

        string token = actionContext.Request.Headers.Authorization.Parameter;

        try
        {
            var principal = JWTHelper.ValidateJwtToken(token);

            if (principal == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid or expired token.");
                return;
            }

            var userRole = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userRole))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Role information missing in token.");
                return;
            }

            // Convert role string to Enum
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
