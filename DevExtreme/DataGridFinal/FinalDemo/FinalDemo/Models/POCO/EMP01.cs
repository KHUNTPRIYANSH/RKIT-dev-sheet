using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FinalDemo.Models.ENUM;
using ServiceStack.DataAnnotations;

namespace FinalDemo.Models.POCO
{
    public class EMP01
    {
        #region Properties

        /// <summary>
        /// P01F01 = EmployeeId
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int P01F01 { get; set; }

        /// <summary>
        /// P01F02 = FirstName
        /// </summary>

      
        public string P01F02 { get; set; }

        /// <summary>
        /// P01F03 = LastName
        /// </summary>
       
        public string P01F03 { get; set; }

        /// <summary>
        /// P01F04 = DateOfBirth
        /// </summary>
        
        public DateTime P01F04 { get; set; }

        /// <summary>
        /// P01F05 = DepartmentId
        /// </summary>
        
        public int P01F05 { get; set; }

        /// <summary>
        /// P01F06 = Position
        /// </summary>
       
        public string P01F06 { get; set; }

        /// <summary>
        /// P01F07 = IsActive
        /// </summary>
       
        public bool P01F07 { get; set; } = true;

        /// <summary>
        /// P01F06 = Salary
        /// </summary>

        public decimal P01F08 { get; set; }

        #endregion
    }
}