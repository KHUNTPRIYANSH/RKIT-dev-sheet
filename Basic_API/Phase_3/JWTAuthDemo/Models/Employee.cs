using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWTAuthDemo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }

        // Method to retrieve employees (dummy data for demonstration)
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, FirstName = "Priyansh", LastName = "Khunt", Gender = "Male", City = "JND", IsActive = false },
                new Employee { Id = 2, FirstName = "Keyur", LastName = "Saradva", Gender = "Male", City = "Morbi", IsActive = true },
                //  more employees as needed
            };
        }
    }
}
