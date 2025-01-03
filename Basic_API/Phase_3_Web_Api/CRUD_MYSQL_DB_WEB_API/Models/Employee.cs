using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_MYSQL_DB_WEB_API.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public bool IsActive { get; set; }
    }
}