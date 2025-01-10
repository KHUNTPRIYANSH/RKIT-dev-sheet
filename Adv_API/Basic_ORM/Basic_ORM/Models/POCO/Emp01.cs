using System;
using ServiceStack.DataAnnotations;

namespace Basic_ORM.Models.POCO
{
    /// <summary>
    /// Represents an employee record in the system.
    /// </summary>
    public class Emp01
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the employee.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Employee's name.
        /// </summary>
        [Required]  // Makes the Name property mandatory
        [StringLength(100)] // Limits the length to 100 characters
        public string Name { get; set; }

        /// <summary>
        /// Department name where the employee works.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        /// <summary>
        /// Employee's salary.
        /// </summary>
        [DecimalLength(10, 2)] // Defines precision and scale for decimal values
        public decimal Salary { get; set; }

        /// <summary>
        /// Date the employee joined the organization.
        /// Defaults to the current date and time.
        /// </summary>
        [Default(typeof(DateTime), "CURRENT_TIMESTAMP")]
        public DateTime DateOfJoining { get; set; } = DateTime.Now;

        /// <summary>
        /// Contact email address of the employee.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Unique]  // Ensures uniqueness of the email address
        public string Email { get; set; }

        /// <summary>
        /// Contact phone number of the employee.
        /// </summary>
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }

        /// <summary>
        /// Indicates whether the employee is active.
        /// </summary>
        [Default(1)]  // Sets the default value to true
        public bool IsActive { get; set; }

        #endregion
    }
}
