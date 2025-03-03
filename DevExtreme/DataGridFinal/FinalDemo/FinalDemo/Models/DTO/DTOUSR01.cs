using FinalDemo.Models.ENUM;
using FinalDemo.Models.POCO;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalDemo.Models.DTO
{
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
        /// R01F03 = User Password
        /// </summary>

        public string R01F03 { get; set; }

        /// <summary>
        /// R01F04 = Role (e.g., Admin, Editor, User).
        /// </summary>

        public EnmRoleType R01F04 { get; set; } = EnmRoleType.User; // Enum type for fixed roles.


        #endregion


    }
}