using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Web_Api.Models
{
    /// <summary>
    /// Employee class is a model class which is used to represent the employee entity.
    /// </summary>
    public class Employee
    {
        // followings are the cols of employee table
        // to generate getter setter write : prop + tab + tab

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}