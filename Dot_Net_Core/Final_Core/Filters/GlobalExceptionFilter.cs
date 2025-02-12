using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using ServiceStack;

namespace Final_Core.Filters
{
    /// <summary>
    /// Global exception filter to handle and log unhandled exceptions.
    /// Returns a standardized error response.
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        #region Fields

        private readonly ILogger<GlobalExceptionFilter> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="GlobalExceptionFilter"/>.
        /// </summary>
        /// <param name="logger">Logger instance for logging exceptions.</param>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Exception Handling

        /// <summary>
        /// Handles unhandled exceptions, logs them, and returns a standardized error response.
        /// </summary>
        /// <param name="context">Exception context containing details of the error.</param>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // Log the exception
            _logger.LogError(exception, "Unhandled exception occurred.");

            // Create error response object
            var response = new ErrorResponse
            {
                Message = "An unexpected error occurred."
            };

            // Determine environment (Development/Production)
            var env = context.HttpContext.RequestServices.GetService<IHostEnvironment>();

            if (env.IsDevelopment())
            {
                // In development, include detailed error information
                response.Details = exception.Message;
                response.StackTrace = exception.StackTrace;
            }
            else
            {
                // In production, avoid exposing detailed error messages
                response.Message = "An unexpected error occurred. Please try again later.";
            }

            // Set response result
            context.Result = new ObjectResult(response)
            {
                StatusCode = 500 // Internal Server Error
            };

            // Mark the exception as handled
            context.ExceptionHandled = true;
        }

        #endregion
    }

    /// <summary>
    /// Standardized error response model.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// General error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Detailed error message (included only in development mode).
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Stack trace information (included only in development mode).
        /// </summary>
        public string StackTrace { get; set; }
    }
}
