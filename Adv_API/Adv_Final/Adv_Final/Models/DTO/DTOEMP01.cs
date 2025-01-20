using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adv_Final.Models.DTO
{
    /// <summary>
    /// Represents the Employee entity for employee management.
    /// </summary>
    public class DTOEMP01
    {
        #region Properties

        /// <summary>
        /// P01F01 = EmployeeId
        /// </summary>
        [Key] // Indicates this is the primary key.
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

        #endregion
    }
}
