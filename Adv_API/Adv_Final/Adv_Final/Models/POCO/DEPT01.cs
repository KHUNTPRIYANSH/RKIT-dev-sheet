using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adv_Final.Models.POCO
{
    /// <summary>
    /// Represents the Department entity for department management.
    /// </summary>
    [Table("DEPT01")] // Specifies the table name in the database.
    public class DEPT01
    {
        #region Properties

        /// <summary>
        /// T01F01 = DeptId
        /// </summary>
        [Key] // Indicates this is the primary key.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
