using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Phase_3_Final_Demo.Helpers;

namespace Phase_3_Final_Demo
{
    /// <summary>
    /// Custom message handler for validating JSON Web Tokens (JWT) in incoming HTTP requests.
    /// </summary>
    public class JwtAuthenticationHandler : DelegatingHandler
    {
        #region Overridden SendAsync Method

        /// <summary>
        /// Processes the incoming HTTP request, validates the JWT token, and sets the user's principal if valid.
        /// </summary>
        /// <param name="request">The incoming HTTP request.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>
        /// An HTTP response message after processing the request.
        /// If the token is invalid, it returns an Unauthorized response.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Check if the Authorization header is present in the request
            if (request.Headers.Authorization != null)
            {
                // Extract the token from the Authorization header
                var token = request.Headers.Authorization.Parameter;

                try
                {
                    // Validate the JWT token using JwtHelper
                    var principal = JwtHelper.ValidateJwtToken(token);

                    // Set the authenticated user to the current thread's principal
                    Thread.CurrentPrincipal = principal;

                    // For Web API, also set the request context's principal
                    if (request.GetRequestContext() != null)
                    {
                        request.GetRequestContext().Principal = principal;
                    }
                }
                catch (Exception)
                {
                    // Return Unauthorized response if token validation fails
                    return request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token.");
                }
            }

            // Continue processing the request if no Authorization header or token is valid
            return await base.SendAsync(request, cancellationToken);
        }

        #endregion
    }
}
