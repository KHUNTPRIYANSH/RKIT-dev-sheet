using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalDemo.Models.DTO
{
    public class DTOEMP01
    {
        #region Properties

        /// <summary>
        /// P01F01 = EmployeeId
        /// </summary>
    
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
        /// P01F08 = Salary
        /// </summary>

        public decimal P01F08 { get; set; }

        /// <summary>
        /// P01F09 = User Id
        /// </summary>

        public decimal P01F09 { get; set; }
        #endregion
    }
}