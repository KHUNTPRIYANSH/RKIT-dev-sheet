using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using ServiceStack;

namespace Final_Core.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, "Unhandled exception occurred.");

            var response = new ErrorResponse
            {
                Message = "An unexpected error occurred."
            };

            // In development, show detailed error information
            if (context.HttpContext.RequestServices.GetService<IHostEnvironment>().IsDevelopment())
            {
                response.Details = exception.Message;  // Include details in development
                response.StackTrace = exception.StackTrace;  // Include stack trace for debugging
            }
            else
            {
                // In production, don't expose detailed exception information
                response.Message = "An unexpected error occurred. Please try again later.";
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = 500 // Internal Server Error
            };

            context.ExceptionHandled = true; // Mark exception as handled
        }
    }
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public string StackTrace { get; set; }
    }

}
