using ServiceStack.DataAnnotations;

namespace Final_Core.Models.POCO
{
    /// <summary>
    /// Employee Table
    /// </summary>
   public class Emp01
    {
        /// <summary>P01F01 = Employee ID (Primary Key)</summary>
        [PrimaryKey]
        [AutoIncrement]
        public int P01F01 { get; set; }

        /// <summary>P01F02 = Employee Name</summary>
        [Required]
        [StringLength(100)]
        public string P01F02 { get; set; }

        /// <summary>P01F03 = Employee Email</summary>
        [Required]
       [StringLength(150)]
        public string P01F03 { get; set; }

        /// <summary>P01F04 = Employee Phone Number</summary>
        [Required]
     
        [StringLength(15)]
        public string P01F04 { get; set; }

        /// <summary>P01F05 = Created At</summary>
      
        public DateTime P01F05 { get; set; } = DateTime.UtcNow;

        /// <summary>P01F06 = Updated At</summary>
      
        public DateTime? P01F06 { get; set; }

        /// <summary>P01F07 = Is Active (0 = Inactive, 1 = Active)</summary>
     
        public bool P01F07 { get; set; } = true;

        /// <summary>P01F08 = Salary</summary>
    
        [DecimalLength(10, 2)]
        public decimal P01F08 { get; set; }

        /// <summary>P01F09 = Department</summary>
    
        [StringLength(50)]
        public string P01F09 { get; set; }
    }
}
