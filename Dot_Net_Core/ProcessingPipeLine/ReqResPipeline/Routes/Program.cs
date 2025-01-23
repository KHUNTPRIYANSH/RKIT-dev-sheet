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

            app.UseRouting(); // Ensure this is called before MapControllers()

            app.Use(async (context, next) =>
            {
                Endpoint endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    await context.Response.WriteAsync("DisplayName : " + endpoint.DisplayName + "\n");
                    await context.Response.WriteAsync("MetaData :  " + endpoint.Metadata + "\n");
                    await context.Response.WriteAsync("Req Delegate :  " + endpoint.RequestDelegate + "\n");
                    await context.Response.WriteAsync("Type :  " + endpoint.GetType().Name + "\n");
                }
                await next(context);
            });

            app.UseAuthorization();

            app.MapControllers(); // Map controller routes

            // Use Conventional Routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("Home", async (context) =>
                {
                    await context.Response.WriteAsync("Hi from Map()");
                });
                endpoints.MapGet("Product/{id}", async (context) =>
                {
                    var id = Convert.ToInt32(context.Request.RouteValues["id"]);
                    await context.Response.WriteAsync("Hi from MapGet() , ID:" + id);
                });
                endpoints.MapGet("library/{author}/{bookid = 0}", async (context) =>
                {
                    var author = context.Request.RouteValues["Author"].ToString();
                    var bookId = Convert.ToInt32(context.Request.RouteValues["BookId"]);
                    await context.Response.WriteAsync("Hi from MapGet() , \nAuthor: " + author + "\nBookID: " + bookId);
                });
                endpoints.MapGet("Employee/{id?}", async (context) =>
                {
                    var id = Convert.ToInt32(context.Request.RouteValues["id"]);
                    if (id == null)
                    {
                        await context.Response.WriteAsync("Employee : id not given ");

                    }
                    else
                    {
                        await context.Response.WriteAsync("Employee [ID]: " + id);
                    }
                });
                endpoints.MapPost("Product", async (context) =>
                {
                    await context.Response.WriteAsync("Hi from MapPost()");
                });
                endpoints.MapPut("Product", async (context) =>
                {
                    await context.Response.WriteAsync("Hi from MapPut()");
                });
                endpoints.MapDelete("Product", async (context) =>
                {
                    await context.Response.WriteAsync("Hi from MapDelete()");
                });
                endpoints.MapPatch("Product", async (context) =>
                {
                    await context.Response.WriteAsync("Hi from MapPatch()");
                });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // Route template
            });

            app.Run();
        }
    }
}
