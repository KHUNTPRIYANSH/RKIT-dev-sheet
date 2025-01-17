using Adv_Final.Models.Enum;
using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceStack.DataAnnotations;
namespace YourNamespace.Models.POCO
{
    /// <summary>
    /// Represents the User entity for login and role management.
    /// </summary>
    [Table("USR01")] // Specifies the table name in the database.
    public class USR01
    {
        #region Properties

        /// <summary>
        /// ROTFO1 = UserName
        /// </summary>
        [PrimaryKey] // Indicates this is the primary key.
        [AutoIncrement] // Enables auto-increment.
        public int R01F01 { get; set; }

        /// <summary>
        /// ROTFO2 = UserName
        /// </summary>
        [Required]
        [StringLength(100)]
        public string R01F02 { get; set; }

        /// <summary>
        /// ROTFO3 = [hashed] Password
        /// </summary>
        [Required]
        [StringLength(255)]
        public string R01F03 { get; set; }

        /// <summary>
        /// ROTFO4 = Role (e.g., Admin, Editor, Normal).
        /// </summary>
        [Required]
        public EnmRole R01F04 { get; set; } // Enum type for fixed roles.

        /// <summary>
        /// ROTFO5 =  IsActive.
        /// </summary>
        [Required]
        public bool R01F05 { get; set; } = true; // Default value: active account.

        /// <summary>
        /// ROTFO6 = CreatedAt.
        /// </summary>
        [Required]
        public DateTime R01F06 { get; set; } = DateTime.Now; // Default to current timestamp.

        /// <summary>
        /// ROTFO7 = UpdatedAt.
        /// </summary>
        [Required]
        public DateTime R01F07 { get; set; } = DateTime.Now; // Default to current timestamp.

        #endregion

        #region Methods (Optional for Future Enhancements)

        /// <summary>
        /// Updates the timestamp for the last updated field.
        /// </summary>
        public void UpdateTimestamp()
        {
            R01F07 = DateTime.Now;
        }

        #endregion
    }

    
}
