using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalDemo.Models.ENUM;
using System.ComponentModel.DataAnnotations;
namespace FinalDemo.Models.POCO
{
    public class USR01
    {
        [AutoIncrement]
        [PrimaryKey]
        // id
        public int R01F01 { get; set; }

        // name
        public string R01F02 { get; set; }

     // pass
        public string R01F03 { get; set; }

        // role
        [IgnoreOnUpdate]
        [EnumDataType(typeof(EnmRoleType))]
        public EnmRoleType R01F04 { get; set; }


        // is active
        public bool R01F05 { get; set; } = true;

        // created at
        [IgnoreOnUpdate]
        public DateTime R01F06 { get; set; } = DateTime.Now;

        public DateTime R01F07 { get; set; } = DateTime.Now;

        public void UpdateTimestamp()
        {
            R01F07 = DateTime.Now;
        }
    }
}