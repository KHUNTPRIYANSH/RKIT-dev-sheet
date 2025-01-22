using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddleWare.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ConventionalCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public ConventionalCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsJsonAsync("[Req] Conventional Custom Middleware");
            await _next(httpContext);
            await httpContext.Response.WriteAsJsonAsync("[Res] Conventional Custom Middleware");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ConventionalCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseConventionalCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConventionalCustomMiddleware>();
        }
    }
}
