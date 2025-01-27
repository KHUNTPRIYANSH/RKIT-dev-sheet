using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BasicAuth.BasicAuth
{
    /// <summary>
    /// BasicAuthAttribute class authenticates the user while making request
    /// </summary>
    public class BasicAuthAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Calls when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using <see cref="T:System.Web.Http.Filters.AuthorizationFilterAttribute" />.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // case 1 : header is empty
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed !!!");
            }
            // case 2 : header is not empty
            else
            {
                try
                {
                    // username:password base64 encoded 
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;
                    // decodedToken = YWRtaW46cGFzc0AxMjM=

                    // step-1 decode authToken from base64 to string using Convert class
                    // Convert.FromBase64String(authToken); this code will decode the authToken but it is still not in the string fromate
                    // Encoding.UTF8.GetString();  this method is used to convert decrypted token into string
                    string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    // decodedToken = admin:pass@123

                    // step-2 now we have username:password we will split it and check them
                    string[] usernamePassword = decodedToken.Split(':');

                    string username = usernamePassword[0];
                    string password = usernamePassword[1];

                    if (ValidateUser.Login(username, password))
                    {
                        // principal means user
                        // if login is successfull then we make new thread and assign one user to it using GenericPrincipal( identity , roles )
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed !!!");
                    }
                }
                catch(Exception ex)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "InternalServerError : Try Again !!!");
                }
            }
        }
    }
}