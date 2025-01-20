using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Adv_Final.Extensions
{
    public class RoleAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (principal == null || !_roles.Any(role => principal.IsInRole(role)))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "You do not have permission to access this resource.");
            }
        }
    }
}