
using Microsoft.AspNetCore.Diagnostics;
using MiddlewareDemo.Middleware;

namespace MiddlewareDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Use of Authentication Middleware
            /*
            builder.Services.AddAuthentication("Bearer")
                            .AddJwtBearer("Bearer", options =>
                            {
                                options.Authority = "https://example.com";
                                options.Audience = "api";
                            });
            */

            // Use of Authorization Middleware
            /*
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
            builder.Services.AddControllers();
            */

            var app = builder.Build();

            // Use Static File Middleware
            app.UseStaticFiles();

            // Use Custom Middleware
             app.UseCustomMiddlewareDemo();

            // Example of custom middleware using app.Use
           
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Request Path : {context.Request.Path}");
                if (context.Request.Path == "/api/Demo")
                {
                    Console.WriteLine("Stop");
                    return;
                }
                await next(context);
                Console.WriteLine($"Status Code : {context.Response.StatusCode}");
            });
        

            // UseWhen to conditionally apply middleware
       
            //app.UseWhen(context => context.Request.Path.StartsWithSegments("/admin"), adminApp =>
            //{
            //    adminApp.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("Admin Middleware: Before Request");
            //        await next();
            //        Console.WriteLine("Admin Middleware: After Request");
            //    });
            //});
          

            // Example of custom middleware using app.Use
         
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Before Middleware 1 using Use().\n");
                await next(context);
                await context.Response.WriteAsync("After Middleware 1 using Use().\n");
            });
           

            // Another example of custom middleware using app.Use
         
            app.Use(async (context, next) =>
            {
                context.Response.WriteAsync("Before Middleware 2 using Use().\n");
                await next(context);
                context.Response.WriteAsync("After Middleware 2 using Use().\n");
            });
        

            // Example of custom middleware using app.Run
         
            app.Run(async context =>
            {
                await context.Response.WriteAsync("This is Middleware 3 Using Run()\n");
                context.Response.WriteAsync("Middleware 3 - Request Handled.\n");
            });
         

            // Use Routing Middleware
            // app.UseRouting();

            // Configure endpoints
            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello from the root endpoint!");
                });
            });
            */

            // Use Exception Handler Middleware
            //app.UseExceptionHandler("/error");
            //app.Map("/error", (HttpContext context) =>
            //{
            //    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            //    return Results.Problem(title: "An error occurred", detail: exception?.Message);
            //});

            // Another example of Exception Handler Middleware
            /*
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An error occurred!");
                });
            });
            */

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}