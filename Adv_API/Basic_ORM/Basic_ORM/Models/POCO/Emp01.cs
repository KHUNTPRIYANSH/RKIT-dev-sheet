using System;

namespace Basic_ORM.Models.POCO
{
    public class Emp01
    {
        public int Id { get; set; } // Unique identifier
        public string Name { get; set; } // Employee's name
        public string Department { get; set; } // Department name
        public decimal Salary { get; set; } // Employee's salary
        public DateTime DateOfJoining { get; set; } // Date the employee joined
        public string Email { get; set; } // Contact email
        public string Phone { get; set; } // Contact phone number
        public bool IsActive { get; set; } // Active or inactive status
    }
}
