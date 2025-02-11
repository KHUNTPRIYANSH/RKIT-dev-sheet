using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using RangeAttribute = ServiceStack.DataAnnotations.RangeAttribute;
using RequiredAttribute = ServiceStack.DataAnnotations.RequiredAttribute;
using StringLengthAttribute = ServiceStack.DataAnnotations.StringLengthAttribute;

namespace Final_Core.Models.DTO
{
    /// <summary>
    /// Employee Data Transfer Object
    /// </summary>
    public class DTOEmp01
    {
        /// <summary>P01F01 = Employee ID</summary>
        public int P01F01 { get; set; }

        /// <summary>P01F02 = Employee Name</summary>
        [Required]
        [StringLength(100)]
        public string P01F02 { get; set; }

        /// <summary>P01F03 = Employee Email</summary>
        [Required]
        [StringLength(150)]
        [EmailAddress]
        public string P01F03 { get; set; }

        /// <summary>P01F04 = Employee Phone Number</summary>
        [Required]
        [StringLength(15)]
        public string P01F04 { get; set; }


        /// <summary>P01F05 = Created At</summary>
        [IgnoreOnUpdate]
        public DateTime P01F05 { get; set; }

        /// <summary>P01F06 = Updated At</summary>
        public DateTime? P01F06 { get; set; }

        /// <summary>P01F07 = Is Active</summary>
        public bool P01F07 { get; set; } = true;

        /// <summary>P01F08 = Salary</summary>
        [Range(0, 1000000)]
        public decimal P01F08 { get; set; } = 0;

        /// <summary>P01F09 = Department</summary>
        [StringLength(50)]
        public string P01F09 { get; set; }
    }
}
