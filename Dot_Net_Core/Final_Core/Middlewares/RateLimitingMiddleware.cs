using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Final_Core.Middlewares
{
    /// <summary>
    /// Middleware for rate limiting requests based on IP address.
    /// Limits each IP to one request every 10 seconds.
    /// </summary>
    public class RateLimitingMiddleware
    {
        #region Fields

        // Stores the last request time for each IP address
        private static readonly ConcurrentDictionary<string, DateTime> _userRequestTimes = new ConcurrentDictionary<string, DateTime>();

        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="RateLimitingMiddleware"/>.
        /// </summary>
        /// <param name="next">Next middleware in the pipeline.</param>
        /// <param name="logger">Logger for logging request details.</param>
        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        #endregion

        #region Middleware Execution

        /// <summary>
        /// Invokes the middleware to apply rate limiting on requests.
        /// </summary>
        /// <param name="context">HTTP context of the request.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            #region Bypass Swagger Endpoints

            // Skip rate limiting for Swagger UI and API documentation endpoints
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            #endregion

            #region Get Client IP Address

            // Retrieve the user's IP address
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            #endregion

            if (ipAddress != null)
            {
                #region Check Rate Limit

                // Check if the IP address exists in the dictionary
                if (_userRequestTimes.ContainsKey(ipAddress))
                {
                    var lastRequestTime = _userRequestTimes[ipAddress];
                    var timeDiff = DateTime.Now - lastRequestTime;

                    // Log the time difference since the last request
                    _logger.LogInformation($"IP {ipAddress} made a request. Time difference: {timeDiff.TotalSeconds} seconds.");

                    // If the request is within the 10-second limit, return a 429 response
                    if (timeDiff.TotalSeconds < 10)
                    {
                        _logger.LogWarning($"Rate limit exceeded for IP {ipAddress}. Next allowed request in {10 - (int)timeDiff.TotalSeconds} seconds.");

                        context.Response.StatusCode = 429; // Too Many Requests
                        await context.Response.WriteAsync($"Too many requests. Please try again after {10 - (int)timeDiff.TotalSeconds} seconds.");
                        return;
                    }
                    else
                    {
                        // Update the last request time after 10 seconds have passed
                        _userRequestTimes[ipAddress] = DateTime.Now;
                    }
                }
                else
                {
                    // If it's the first request from this IP, store the request time
                    _userRequestTimes[ipAddress] = DateTime.Now;
                }

                #endregion

                // Log successful request processing
                _logger.LogInformation($"Request allowed for IP {ipAddress} at {DateTime.Now}. Request time updated.");
            }
            else
            {
                // Log missing IP address scenario
                _logger.LogWarning("IP address could not be determined.");
            }

            #region Continue Request Processing

            // Pass the request to the next middleware in the pipeline
            await _next(context);

            #endregion
        }

        #endregion
    }
}
