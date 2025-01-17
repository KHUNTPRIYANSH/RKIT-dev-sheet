using Adv_Final.Models.Enum;
using ServiceStack.DataAnnotations;
using System;
namespace YourNamespace.Models.DTO
{
    /// <summary>
    /// Represents the User entity for login and role management.
    /// </summary>

    public class DTOUSR01
    {
        #region Properties

        /// <summary>
        /// R01F01 = ID
        /// </summary>

        public int R01F01 { get; set; }

        /// <summary>
        /// R01F02 = UserName
        /// </summary>

        public string R01F02 { get; set; }

        /// <summary>
        /// R01F03 = Normal Password
        /// </summary>

        public string R01F03 { get; set; }

        /// <summary>
        /// R01F04 = Role (e.g., Admin, Editor, Normal).
        /// </summary>

        public EnmRole R01F04 { get; set; } = EnmRole.Normal; // Enum type for fixed roles.

     
        #endregion

       
    }


}
