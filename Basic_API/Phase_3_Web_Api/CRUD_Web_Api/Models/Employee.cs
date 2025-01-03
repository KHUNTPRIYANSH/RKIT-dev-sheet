using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Web_Api.Models
{
    public class Employee
    {
        // followings are the cols of employee table
        // to generate getter setter write : prop + tab + tab
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }
    }
}