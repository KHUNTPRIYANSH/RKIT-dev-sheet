using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adv_Final.Models.POCO
{
    /// <summary>
    /// Represents the Employee entity for employee management.
    /// </summary>
    [Table("EMP01")] // Specifies the table name in the database.
    public class EMP01
    {
        #region Properties

        /// <summary>
        /// P01F01 = EmployeeId
        /// </summary>
        
        [Key] // Indicates this is the primary key.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int P01F01 { get; set; }

        /// <summary>
        /// P01F02 = FirstName
        /// </summary>
        [Required]
        [StringLength(100)]
        public string P01F02 { get; set; }

        /// <summary>
        /// P01F03 = LastName
        /// </summary>
        [Required]
        [StringLength(100)]
        public string P01F03 { get; set; }

        /// <summary>
        /// P01F04 = DateOfBirth
        /// </summary>
        [Required]
        public DateTime P01F04 { get; set; }

        /// <summary>
        /// P01F05 = DepartmentId
        /// </summary>
        [ForeignKey("Department")]
        public int P01F05 { get; set; }

        /// <summary>
        /// P01F06 = Position
        /// </summary>
        [Required]
        [StringLength(100)]
        public string P01F06 { get; set; }

        /// <summary>
        /// P01F07 = IsActive
        /// </summary>
        [Required]
        public bool P01F07 { get; set; } = true;

        #endregion
    }
}
