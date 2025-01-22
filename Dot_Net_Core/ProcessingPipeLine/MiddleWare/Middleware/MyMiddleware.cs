namespace MiddleWare.Middleware
{
    public class MyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("[Req] middleware class \n");
            await next(context);
            await context.Response.WriteAsync("[Res] middleware class \n");
        }
    }
    public static class Extension{
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyMiddleware>();
        }
    }
}
