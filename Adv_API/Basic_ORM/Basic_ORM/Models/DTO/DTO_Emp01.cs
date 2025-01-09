using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Basic_ORM.Models.DTO
{
    public class DTO_Emp01
    {
        public string Name { get; set; } // Employee's name
        public string Department { get; set; } // Department name
        public decimal Salary { get; set; } // Employee's salary
        public string Email { get; set; } // Contact email
        public bool IsActive { get; set; } // Active or inactive status
    }
}