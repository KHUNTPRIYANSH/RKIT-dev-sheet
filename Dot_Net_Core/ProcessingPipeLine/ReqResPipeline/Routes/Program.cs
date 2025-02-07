namespace Routes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(); // Register controller services
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting(); // Ensure this is called before mapping routes

            // Middleware to log endpoint details
            app.Use(async (context, next) =>
            {
                var endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    await context.Response.WriteAsync($"DisplayName: {endpoint.DisplayName}\n");
                    await context.Response.WriteAsync($"MetaData: {endpoint.Metadata}\n");
                    await context.Response.WriteAsync($"Req Delegate: {endpoint.RequestDelegate}\n");
                    await context.Response.WriteAsync($"Type: {endpoint.GetType().Name}\n");
                }
                await next(); // Corrected: No need to pass `context` to `next()`
            });

            app.UseAuthorization();

            app.MapControllers(); // Maps attribute-routed controllers

            // Use Conventional Routing
            app.Map("/Home", async (context) =>
            {
                await context.Response.WriteAsync("Hi from Map()");
            });

            app.MapGet("Product/{id}", async (context) =>
            {
                if (context.Request.RouteValues.TryGetValue("id", out var idObj) && int.TryParse(idObj?.ToString(), out int id))
                {
                    await context.Response.WriteAsync($"Hi from MapGet(), ID: {id}");
                }
                else
                {
                    await context.Response.WriteAsync("Invalid ID format.");
                }
            });

            app.MapGet("library/{author}/{bookid?}", async (context) =>
            {
                if (context.Request.RouteValues.TryGetValue("author", out var author) &&
                    context.Request.RouteValues.TryGetValue("bookid", out var bookIdObj) &&
                    int.TryParse(bookIdObj?.ToString(), out int bookId))
                {
                    await context.Response.WriteAsync($"Hi from MapGet(),\nAuthor: {author}\nBookID: {bookId}");
                }
                else
                {
                    await context.Response.WriteAsync("Invalid Author or BookID.");
                }
            });

            app.MapGet("Employee/{id?}", async (context) =>
            {
                if (context.Request.RouteValues.TryGetValue("id", out var idObj) && int.TryParse(idObj?.ToString(), out int id))
                {
                    await context.Response.WriteAsync($"Employee [ID]: {id}");
                }
                else
                {
                    await context.Response.WriteAsync("Employee: ID not given.");
                }
            });

            app.MapPost("Product", async (context) =>
            {
                await context.Response.WriteAsync("Hi from MapPost()");
            });

            app.MapPut("Product", async (context) =>
            {
                await context.Response.WriteAsync("Hi from MapPut()");
            });

            app.MapDelete("Product", async (context) =>
            {
                await context.Response.WriteAsync("Hi from MapDelete()");
            });

            app.MapPatch("Product", async (context) =>
            {
                await context.Response.WriteAsync("Hi from MapPatch()");
            });

            // Default controller route mapping
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
