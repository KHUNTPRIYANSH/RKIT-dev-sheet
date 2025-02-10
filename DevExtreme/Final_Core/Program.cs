using Final_Core.BL.Interfaces;
using Final_Core.BL.Operations;
using Final_Core.Data;
using Final_Core.Middlewares;
using Final_Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Final_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthorization();

            // Register the database connection factory
            builder.Services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
            builder.Configuration.GetConnectionString("MyDbConnection"), MySqlDialect.Provider));

            // Register business logic services
            builder.Services.AddScoped<BLEmp01>();
            builder.Services.AddScoped<OrmLiteDbContext>();

            // Register company and bank services
            builder.Services.AddScoped<ICompany, Company>();
            //builder.Services.AddScoped<IBank, ICICIBankService>(); 
            builder.Services.AddScoped<IBank, HDFCBankService>();

            var app = builder.Build();
            // Register the IP logging middleware
            app.UseMiddleware<RateLimitingMiddleware>(); // Add the Rate Limiting Middleware here

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Apply RateLimitingMiddleware for all routes except /swagger
            

            app.UseRouting(); // Ensure routing is added to the pipeline

            app.UseAuthentication(); // Ensure authentication middleware is applied (if necessary)
            app.UseAuthorization();  // Authorization middleware should come after routing

            app.MapControllers(); // Map the controllers (this handles route matching)

            app.Run(); // Start the application
        }
    }
}
