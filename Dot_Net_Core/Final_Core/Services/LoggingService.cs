using Final_Core.Data;
using Final_Core.Models;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Final_Core.Services
{
    public class LoggingService
    {
        private readonly OrmLiteDbContext _dbContext;

        public LoggingService(OrmLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogApiRequestAsync(string method, string path, string route, string requestBody, int statusCode, string responseBody, string clientIp)
        {
            try
            {
                using (IDbConnection db = _dbContext.OpenDbConnection())
                {
                    var logEntry = new ApiLog
                    {
                        Timestamp = DateTime.UtcNow,
                        RequestMethod = method,
                        RequestPath = path,
                        Route = route,
                        RequestBody = requestBody,
                        ResponseStatusCode = statusCode,
                        ResponseBody = responseBody,
                        ClientIP = clientIp
                    };

                    await db.InsertAsync(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging API request: {ex.Message}");
            }
        }
    }
}