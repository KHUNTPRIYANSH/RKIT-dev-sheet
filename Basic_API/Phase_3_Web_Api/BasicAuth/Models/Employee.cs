using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuth.Models
{
    /// <summary>
    /// Emoloyee class is used to store the employee information
    /// </summary>
    public class Employee
    {
        // prop + tab + tab to generate the property        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }
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

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <returns>static list of predefined employees</returns>
        public static List<Employee> GetEmployees()
        {
            var employees = new List<Employee>() { 
                new Employee{Id = 1, FirstName = "Priyansh", LastName = "Khunt", Gender="Male", City = "JND", IsActive=false},
                new Employee{Id = 2, FirstName = "Keyur", LastName = "Saradva", Gender="Male", City = "Morbi", IsActive=true},
                new Employee{Id = 3, FirstName = "Parth", LastName = "Patel", Gender="Male", City = "RJK", IsActive=false},
                new Employee{Id = 4, FirstName = "Hetvi", LastName = "Shah", Gender="Female", City = "AHM", IsActive=false},
                new Employee{Id = 5, FirstName = "Raj", LastName = "Joshi", Gender="Male", City = "Goa", IsActive=true},
            };

            return employees;
            
        }

    }
}