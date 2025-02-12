using Final_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Final_Core.Middlewares
{
    /// <summary>
    /// Middleware for logging API requests and responses.
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="RequestResponseLoggingMiddleware"/>.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="serviceProvider">Service provider for resolving dependencies.</param>
        public RequestResponseLoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Middleware Execution

        /// <summary>
        /// Invokes the middleware to log request and response data.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                #region Read Request Body

                // Enable request body buffering to allow multiple reads
                context.Request.EnableBuffering();

                // Read the request body content
                var requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true).ReadToEndAsync();

                // Reset request body stream position for further processing
                context.Request.Body.Position = 0;

                #endregion

                #region Capture Response

                // Store the original response body stream
                var originalResponseBodyStream = context.Response.Body;

                // Create a memory stream to capture the response
                using var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;

                // Call the next middleware in the pipeline
                await _next(context);

                #endregion

                #region Read Response Body

                // Reset response body stream position to read the response content
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

                // Reset position again for further processing
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                #endregion

                #region Retrieve Route Information

                // Extract route details (controller/action) if available
                var routeData = context.GetRouteData();
                string route = routeData?.Values["controller"] + "/" + routeData?.Values["action"];

                #endregion

                #region Log API Request and Response

                // Create a service scope to resolve LoggingService
                using (var scope = _serviceProvider.CreateScope())
                {
                    var loggingService = scope.ServiceProvider.GetRequiredService<LoggingService>();

                    // Log API request and response details
                    await loggingService.LogApiRequestAsync(
                        method: context.Request.Method,
                        path: context.Request.Path,
                        route: route,
                        requestBody: requestBody,
                        statusCode: context.Response.StatusCode,
                        responseBody: responseBody,
                        clientIp: context.Connection.RemoteIpAddress?.ToString()
                    );
                }

                #endregion

                #region Copy Response Back to Original Stream

                // Copy the captured response back to the original response stream
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);

                #endregion
            }
            catch (Exception ex)
            {
                // Handle exceptions that occur during logging
                Console.WriteLine("Error logging request/response: " + ex.Message);
            }
        }

        #endregion
    }
}
