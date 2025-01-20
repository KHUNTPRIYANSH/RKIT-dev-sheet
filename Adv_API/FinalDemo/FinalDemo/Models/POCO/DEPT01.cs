using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalDemo.Models.POCO
{
      public class DEPT01
    {
        #region Properties

        /// <summary>
        /// T01F01 = DeptId
        /// </summary>
        [PrimaryKey] // Indicates this is the primary key.
        [AutoIncrement]
        public int T01F01 { get; set; } // DepartmentId

        /// <summary>
        /// T01F02 = DeptName
        /// </summary>
        [Required]
        [StringLength(100)] // Defines max length for the department name.
        public string T01F02 { get; set; } // DepartmentName

        /// <summary>
        /// T01F03 = IsActive
        /// </summary>
        [Required]
        public bool T01F03 { get; set; } = true; // IsActive (Default: active department)

        #endregion
    }
}