using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase_3_Final_Demo.Models
{
    /// <summary>
    /// It is a model class that represents a Login entity.
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}