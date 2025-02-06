namespace MiddlewareDemo.Middleware
{
    public class ClassMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("[Req] Middleware using IMiddleware\n");
            await next(context);
            await context.Response.WriteAsync("[Res] Middleware using IMiddleware\n");
        }
    }
}
