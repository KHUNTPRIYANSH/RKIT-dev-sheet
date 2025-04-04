using Final_Core.Services;
using Final_Core.Middlewares;
using Final_Core.Data;
using ServiceStack.OrmLite;
using NLog;
using NLog.Web;
using ServiceStack.Data;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Final_Core.BL.Operations;
using Final_Core.Filters;
using Final_Core.BL.Interfaces;
using System.Reflection;

namespace Final_Core
{
    /// <summary>
    /// Entry point for the Final_Core application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that initializes and starts the web application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                #region Configure Logging
                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(LogLevel.Information);
                builder.Host.UseNLog();
                #endregion

                #region Configure Services
                // Register services in the dependency injection container
                builder.Services.AddControllers(options =>
                {
                    options.Filters.Add<GlobalExceptionFilter>(); // Register global exception filter
                });
                builder.Services.AddEndpointsApiExplorer();
                // Enable XML comments in Swagger
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

                builder.Services.AddSwaggerGen(c =>
                {
                    c.IncludeXmlComments(xmlPath); // Load XML file
                });
                    builder.Services.AddAuthorization();

                // Register the database connection factory
                builder.Services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                    builder.Configuration.GetConnectionString("MyDbConnection"), MySqlDialect.Provider));

                // Register the email service
                builder.Services.AddHttpClient();

                // Register transient services
                builder.Services.AddTransient<EmailService>();
                builder.Services.AddTransient<SendEmailActionFilter>();

                // Register scoped services
                builder.Services.AddScoped<OrmLiteDbContext>(); // Database context for OrmLite
                builder.Services.AddScoped<LoggingService>(); // Logging service
                builder.Services.AddScoped<BLEmp01>(); // Employee service
                builder.Services.AddScoped<ICompany, Company>(); // Company service

                // Bank service - currently using ICICIBankService
                builder.Services.AddScoped<IBank, ICICIBankService>();
                //builder.Services.AddScoped<IBank, HDFCBankService>(); // Alternative bank service
                #endregion

                var app = builder.Build();

                #region Configure Middleware
                // Register custom logging middleware
                app.UseMiddleware<RequestResponseLoggingMiddleware>();

                // Register the rate limiting middleware
                app.UseMiddleware<RateLimitingMiddleware>();
                #endregion

                #region Configure HTTP Request Pipeline
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseCors(); // Ensure CORS policies are applied

                // Map controller endpoints
                app.MapControllers();

                // Run the application
                app.Run();
                #endregion
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
