using Microsoft.Extensions.Logging;

namespace Logging.Services
{
    public class LoggingService
    {
        private readonly ILogger<LoggingService> _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public void PerformTask()
        {
            _logger.LogInformation("LoggingService: Performing a task.");

            try
            {
                throw new Exception("Simulated exception in LoggingService.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in LoggingService.");
            }
        }
    }
}
