
using Microsoft.AspNetCore.Diagnostics;
using MiddlewareDemo.Middleware;

namespace MiddlewareDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register middleware as a service
            builder.Services.AddTransient<ClassMiddleware>();
            builder.Services.AddClassMiddleware();


            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


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
            //        Console.WriteLine("Admin Middleware: [Req] Request");
            //        await next();
            //        Console.WriteLine("Admin Middleware: [Res] Request");
            //    });
            //});
          

            // Example of custom middleware using app.Use
         
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("[Req] Middleware 1 using Use().\n");
                await next(context);
                await context.Response.WriteAsync("[Res] Middleware 1 using Use().\n");
            });

            app.UseMiddleware<ClassMiddleware>();
            // Another example of custom middleware using app.Use

            app.Use(async (context, next) =>
            {
                context.Response.WriteAsync("[Req] Middleware 2 using Use().\n");
                await next(context);
                context.Response.WriteAsync("[Res] Middleware 2 using Use().\n");
            });

            app.UseClassMiddleware();
            // Example of custom middleware using app.Run

            app.Use(async (context, next) =>
            {
                context.Response.WriteAsync("[Req] Middleware 3 using Use().\n");
                await next(context);
                context.Response.WriteAsync("[Res] Middleware 3 using Use().\n");
            });

            app.UseCustomMiddlewareDemo();

            app.Run(async context =>
            {
                await context.Response.WriteAsync("\nThis is Middleware 3 Using Run()\n");
                context.Response.WriteAsync("Middleware 3 - Request Handled.\n\n");
            });
         

            // Use Routing Middleware
             app.UseRouting();

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
            app.UseExceptionHandler("/error");
            app.Map("/error", (HttpContext context) =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                return Results.Problem(title: "An error occurred", detail: exception?.Message);
            });

         

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