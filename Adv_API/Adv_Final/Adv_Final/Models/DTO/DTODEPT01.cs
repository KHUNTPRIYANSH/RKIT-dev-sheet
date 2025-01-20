using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adv_Final.Models.DTO
{
    /// <summary>
    /// Represents the Department entity for department management.
    /// </summary>

    public class DTODEPT01
    {
        #region Properties

        /// <summary>
        /// T01F01 = DeptId
        /// </summary>

        public int T01F01 { get; set; } // DepartmentId

        /// <summary>
        /// T01F02 = DeptName
        /// </summary>
       public string T01F02 { get; set; } // DepartmentName

        #endregion
    }
}
