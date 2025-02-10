using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Final_Core.Middlewares
{
    public class RateLimitingMiddleware
    {
        private static readonly ConcurrentDictionary<string, DateTime> _userRequestTimes = new ConcurrentDictionary<string, DateTime>();
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip rate limiting for Swagger UI and Swagger JSON endpoint
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            // Get the user's IP address
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (ipAddress != null)
            {
                // Check if the user IP already exists in the dictionary
                if (_userRequestTimes.ContainsKey(ipAddress))
                {
                    var lastRequestTime = _userRequestTimes[ipAddress];
                    var timeDiff = DateTime.Now - lastRequestTime;

                    // Log the calculated time difference for debugging
                    _logger.LogInformation($"IP {ipAddress} made a request. Time difference: {timeDiff.TotalSeconds} seconds.");

                    // If the request is within 10 seconds, return a TooManyRequests status
                    if (timeDiff.TotalSeconds < 10)
                    {
                        // Log the rate limit exceeded message
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
                    // If it's the first request, just add the time and continue
                    _userRequestTimes[ipAddress] = DateTime.Now;
                }

                // Log successful request processing
                _logger.LogInformation($"Request allowed for IP {ipAddress} at {DateTime.Now}. Request time updated.");
            }
            else
            {
                // Log if the IP address is not found
                _logger.LogWarning("IP address could not be determined.");
            }

            // Continue processing the request if the rate limit is not exceeded
            await _next(context);
        }
    }
}
