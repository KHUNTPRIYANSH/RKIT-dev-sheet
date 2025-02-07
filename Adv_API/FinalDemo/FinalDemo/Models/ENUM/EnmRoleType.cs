using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalDemo.Models.ENUM
{
    /// <summary>
    /// Enumeration representing different user roles in the system.
    /// </summary>
    public enum EnmRoleType
    {
        /// <summary>
        /// Admin role with the highest level of access (value = 3).
        /// </summary>
        Admin = 3,

        /// <summary>
        /// Editor role with limited access (value = 2).
        /// </summary>
        Editor = 2,

        /// <summary>
        /// User role with basic access (value = 1).
        /// </summary>
        User = 1
    }
}
