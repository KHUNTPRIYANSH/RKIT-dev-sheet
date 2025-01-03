using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuth.Models
{
    public class Employee
    {
        // prop + tab + tab to generate the property
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }

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