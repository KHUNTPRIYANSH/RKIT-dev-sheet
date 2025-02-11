using Final_Core.Services;
using Final_Core.Middlewares;
using Final_Core.Data;
using ServiceStack.OrmLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;
using Microsoft.Extensions.Logging;
using ServiceStack.Data;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Final_Core.BL.Operations;
using Final_Core.Filters;
using Final_Core.BL.Interfaces;

namespace Final_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Configure NLog
                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(LogLevel.Information);
                builder.Host.UseNLog();

              

                // Register services to the container
                builder.Services.AddControllers(options =>
                {
                    options.Filters.Add<GlobalExceptionFilter>(); // Register global exception filter
                });
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddAuthorization();

                // Register the database connection factory
                builder.Services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                    builder.Configuration.GetConnectionString("MyDbConnection"), MySqlDialect.Provider));
                // Register the email service
                builder.Services.AddHttpClient();
                // Register other services
                builder.Services.AddTransient<EmailService>();
                builder.Services.AddTransient<SendEmailActionFilter>();
                // Register scoped services
                builder.Services.AddScoped<OrmLiteDbContext>(); // Register OrmLiteDbContext
                builder.Services.AddScoped<LoggingService>(); // Register LoggingService
                                                              // Register the BLEmp01 service
                builder.Services.AddScoped<BLEmp01>(); // Register BLEmp01
                                                       // Register company and bank services
                builder.Services.AddScoped<ICompany, Company>();
                //builder.Services.AddScoped<IBank, ICICIBankService>(); 
                builder.Services.AddScoped<IBank, HDFCBankService>();

                var app = builder.Build();

                // Register custom logging middleware
                app.UseMiddleware<RequestResponseLoggingMiddleware>();

                // Register the IP logging middleware
                app.UseMiddleware<RateLimitingMiddleware>();

                // Configure HTTP request pipeline
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }


                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseCors();
                app.MapControllers();
                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application startup failed!");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}