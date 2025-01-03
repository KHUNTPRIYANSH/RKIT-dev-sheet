using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.Application;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Web.Http;

namespace JWT_Based_Token_Final
{
    /// <summary>
    /// Web API configuration class for setting up routes, handlers, and formatters.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the Web API configuration.
        /// </summary>
        /// <param name="config">The HTTP configuration object.</param>
        public static void Register(HttpConfiguration config)
        {
            #region Route Configuration

            // Enable attribute routing
            config.MapHttpAttributeRoutes();

            // Add JwtAuthenticationHandler to the message handlers
            config.MessageHandlers.Add(new JwtAuthenticationHandler());

            // Define the default route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Redirect root URL to Swagger UI
            config.Routes.MapHttpRoute(
                name: "SwaggerRedirect",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(message => message.RequestUri.ToString(), "swagger")
            );

            #endregion

            #region Formatter Configuration

            // Configure JSON formatter for better readability
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Newtonsoft.Json.Formatting.Indented;

            #endregion
        }
    }

    /// <summary>
    /// Custom handler for validating JWT tokens in incoming requests.
    /// </summary>
    public class JwtAuthenticationHandler : DelegatingHandler
    {
        /// <summary>
        /// Processes the HTTP request and validates the JWT token if present.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An HTTP response message.</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Check if the Authorization header exists
            if (request.Headers.Authorization != null)
            {
                // Extract the token from the Authorization header
                var token = request.Headers.Authorization.Parameter;

                try
                {
                    #region Token Validation

                    // Create a token handler
                    var tokenHandler = new JwtSecurityTokenHandler();

                    // Define the secret key (ensure it is a 32-character string for symmetric security)
                    var key = Encoding.UTF8.GetBytes("Your-32-Character-Secret-Key1234");

                    // Set up token validation parameters
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "YourIssuer",  // Replace with your issuer
                        ValidAudience = "YourAudience",  // Replace with your audience
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    // Validate the token and extract the principal
                    var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                    // Set the principal to the current thread
                    Thread.CurrentPrincipal = principal;

                    // For Web API, set the principal to the request's context
                    if (request.GetRequestContext() != null)
                    {
                        request.GetRequestContext().Principal = principal;
                    }

                    #endregion
                }
                catch (Exception)
                {
                    // Return Unauthorized if the token validation fails
                    return request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid token.");
                }
            }

            // Proceed with the request if no Authorization header or if token validation succeeds
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
