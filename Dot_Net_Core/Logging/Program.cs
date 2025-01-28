using NLog;
using NLog.Web;

namespace Logging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure NLog and build the logger
            var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("Starting Web API application");

                var builder = WebApplication.CreateBuilder(args);

                // Use NLog as the logging provider
                builder.Logging.ClearProviders();
                builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.Host.UseNLog();

                // Register services in the DI container
                builder.Services.AddControllers();
                builder.Services.AddScoped<Logging.Services.LoggingService>();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseAuthorization();
                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Web API application stopped due to an unhandled exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}
