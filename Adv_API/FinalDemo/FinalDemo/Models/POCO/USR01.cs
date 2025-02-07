using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalDemo.Models.ENUM;
using System.ComponentModel.DataAnnotations;

namespace FinalDemo.Models.POCO
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string R01F02 { get; set; }

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// Gets or sets the role of the user. The role is ignored during update operations.
        /// </summary>
        [IgnoreOnUpdate]
        [EnumDataType(typeof(EnmRoleType))]
        public EnmRoleType R01F04 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is active.
        /// Default is true.
        /// </summary>
        public bool R01F05 { get; set; } = true;

        /// <summary>
        /// Gets or sets the date and time when the user was created.
        /// This field is ignored during update operations.
        /// </summary>
        [IgnoreOnUpdate]
        public DateTime R01F06 { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the date and time when the user was last updated.
        /// </summary>
        public DateTime R01F07 { get; set; } = DateTime.Now;

        /// <summary>
        /// Updates the timestamp for the last update.
        /// </summary>
        public void UpdateTimestamp()
        {
            R01F07 = DateTime.Now;
        }
    }
}
