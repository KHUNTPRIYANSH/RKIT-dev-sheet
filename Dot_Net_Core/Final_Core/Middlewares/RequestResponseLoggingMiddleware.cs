using Final_Core.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Final_Core.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public RequestResponseLoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Read request body
                context.Request.EnableBuffering();
                var requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true).ReadToEndAsync();
                context.Request.Body.Position = 0;

                // Capture response
                var originalResponseBodyStream = context.Response.Body;
                using var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;

                await _next(context);

                // Read response body
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                // Get route information
                var routeData = context.GetRouteData();
                string route = routeData?.Values["controller"] + "/" + routeData?.Values["action"];

                // Resolve LoggingService from the service provider
                using (var scope = _serviceProvider.CreateScope())
                {
                    var loggingService = scope.ServiceProvider.GetRequiredService<LoggingService>();
                    // Log API request and response
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

                // Copy response back to the original stream
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
            catch (Exception ex)
            {
                // Handle logging failure gracefully
                // Optionally, log this exception too
                Console.WriteLine("Log error :" + ex);
            }
        }
    }
}