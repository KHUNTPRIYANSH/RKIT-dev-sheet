using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adv_Final.Models.Enum
{
    /// <summary>
    /// Enum for defining fixed roles of the user.
    /// </summary>
    public enum EnmRole
    {
        /// <summary>
        /// Represents a Normal user with limited privileges.
        /// </summary>
        Normal = 1,


        /// <summary>
        /// Represents an Editor user with editing privileges.
        /// </summary>
        Editor = 2,

        /// <summary>
        /// Represents an Admin user with full privileges.
        /// </summary>
        Admin = 3

    }
}