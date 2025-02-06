namespace MiddlewareDemo.Middleware
{
    public class ExtClassMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("[Req] Middleware using IMiddleware Extension method\n");
            await next(context);
            await context.Response.WriteAsync("[Res] Middleware using IMiddleware Extension method\n");
        }
    }

    public static class ClassMiddlewareExtensions
    {
        public static void AddClassMiddleware(this IServiceCollection services)
        {
            services.AddTransient<ExtClassMiddleware>();
        }

        public static IApplicationBuilder UseClassMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ClassMiddleware>();
        }
    }

}
