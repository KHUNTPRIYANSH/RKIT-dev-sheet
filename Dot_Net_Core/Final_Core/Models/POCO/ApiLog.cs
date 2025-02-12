using ServiceStack.DataAnnotations;
using System;

namespace Final_Core.Models
{
    /// <summary>
    /// Represents an API log entry stored in the database.
    /// </summary>
    [Alias("api_logs")] // Explicitly map this class to the "api_logs" table
    public class ApiLog
    {
        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier for the log entry.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        [Alias("LogID")] // Map the "Id" property to the "LogID" column in the database
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the API request.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method of the request (e.g., GET, POST).
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// Gets or sets the requested API path.
        /// </summary>
        public string RequestPath { get; set; }

        /// <summary>
        /// Gets or sets the route of the API endpoint.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// Gets or sets the request body content.
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// Gets or sets the HTTP response status code.
        /// </summary>
        public int ResponseStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response body content.
        /// </summary>
        public string ResponseBody { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the client making the request.
        /// </summary>
        public string ClientIP { get; set; }

        #endregion
    }
}
