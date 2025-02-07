using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalDemo.Models
{
    /// <summary>
    /// Represents a response object that is returned from service operations.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets a value indicating whether an error occurred.
        /// Default is false, meaning no error.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Gets or sets the message associated with the response.
        /// This could describe the success or failure of an operation.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data associated with the response.
        /// This field is dynamic and can hold any type of data.
        /// </summary>
        public dynamic Data { get; set; }
    }
}
