namespace MiddlewareDemo.Middleware
{ /// <summary>
  /// Custom middleware to demonstrate request and response handling.
  /// </summary>
    public class CustomMiddlewareDemo
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomMiddlewareDemo"/> class.
        /// </summary>
        /// <param name="next">Delegate representing the next middleware in the pipeline.</param>
        public CustomMiddlewareDemo(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Method to handle the HTTP request and response.
        /// </summary>
        /// <param name="httpContext">The context of the current HTTP request.</param>
        public async Task Invoke(HttpContext httpContext)
        {
            // Log the request processing.
            Console.WriteLine("Request - MB's Custom Middleware using Request Delegate.");
            // Call the next middleware in the pipeline.
            await _next(httpContext);
            // Log the response processing.
            Console.WriteLine("Response - MB's Custom Middleware using Request Delegate.");
        }
    }

    /// <summary>
    /// Extension method used to add the custom middleware to the HTTP request pipeline.
    /// </summary>
    public static class CustomMiddlewareDemoExtensions
    {
        /// <summary>
        /// Adds the <see cref="CustomMiddlewareDemo"/> to the application's request pipeline.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The updated application builder.</returns>
        public static IApplicationBuilder UseCustomMiddlewareDemo(this IApplicationBuilder builder)
        {
            // Add the custom middleware to the request pipeline.
            return builder.UseMiddleware<CustomMiddlewareDemo>();
        }
    }
}
