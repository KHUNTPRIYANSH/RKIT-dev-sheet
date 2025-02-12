using Final_Core.Data;
using Final_Core.Models;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Final_Core.Services
{
    /// <summary>
    /// Service for logging API requests to the database.
    /// </summary>
    public class LoggingService
    {
        private readonly OrmLiteDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingService"/> class.
        /// </summary>
        /// <param name="dbContext">Database context for handling database operations.</param>
        public LoggingService(OrmLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Logs an API request asynchronously.
        /// </summary>
        /// <param name="method">HTTP request method (GET, POST, etc.).</param>
        /// <param name="path">The request path.</param>
        /// <param name="route">The API route.</param>
        /// <param name="requestBody">The request body content.</param>
        /// <param name="statusCode">The HTTP response status code.</param>
        /// <param name="responseBody">The response body content.</param>
        /// <param name="clientIp">The IP address of the client making the request.</param>
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

                    // Insert log entry into the database asynchronously
                    await db.InsertAsync(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Log error to console (consider using a logging framework instead)
                Console.WriteLine($"Error logging API request: {ex.Message}");
            }
        }
    }
}
