using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Basic_ORM.BL.Interface;
using Basic_ORM.Models;
using Basic_ORM.Models.DTO;
using Basic_ORM.Models.Enum;
using Basic_ORM.Models.POCO;
using ServiceStack.Data;

namespace Basic_ORM.BL.Operations
{
    public class BL_Employees  : I_Data_Handler<DTO_Emp01>
    {
        private Emp01 _objemp01; // Holds the employee object.
        private int _id; // Employee ID for update operations.
        private Response _objResponse; // Holds obj for response
        private readonly IDbConnectionFactory _dbFactory; // Factory to manage database connections.

    }
}