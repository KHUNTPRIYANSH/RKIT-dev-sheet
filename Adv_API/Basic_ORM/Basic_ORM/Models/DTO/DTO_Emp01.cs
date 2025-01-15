using System;

namespace Basic_ORM.Models.DTO
{
    /// <summary>
    /// Data Transfer Object for Employee entity.
    /// </summary>
    public class DTO_Emp01
    {
        #region Properties

        /// <summary>
        /// Unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Employee's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the department where the employee works.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Employee's salary.
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Contact email of the employee.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Indicates whether the employee is active.
        /// </summary>
        public bool IsActive { get; set; }

        // Phone , date of joining in not their in dto
        #endregion
    }
}
