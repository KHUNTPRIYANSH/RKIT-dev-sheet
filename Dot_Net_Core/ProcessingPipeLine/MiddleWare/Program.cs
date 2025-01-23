
using MiddleWare.Middleware;

namespace MiddleWare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddTransient<MyMiddleware>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // custom middlewares
            // 1. using Use(context,next)
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("[Req] Custom middleware-1 by Use(). \n");
                await next();
                await context.Response.WriteAsync("[Res] Custom middleware-1 by Use(). \n");
            }); 
            
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("[Req] Custom middleware-2 by Use(). \n");
                await next();
                await context.Response.WriteAsync("[Res] Custom middleware-2 by Use(). \n");
            }); 
            
            app.UseMiddleware<MyMiddleware>();


            app.UseConventionalCustomMiddleware();

            app.UseMyMiddleware();
            // 2. using Run(context)
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("[Req] Custom middleware by Run(). \n");
            });
            app.UseAuthorization();
            var summaries = new[]
           {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast");

            app.Run();

            app.MapControllers();

            app.Run();
        }
    }
}