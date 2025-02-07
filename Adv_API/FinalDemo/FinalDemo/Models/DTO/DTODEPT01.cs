using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalDemo.Models.DTO
{
    public class DTODEPT01
    {
        #region Properties

        /// <summary>
        /// T01F01 = DeptId
        /// </summary>

        public int T01F01 { get; set; } = 0;// DepartmentId

        /// <summary>
        /// T01F02 = DeptName
        /// </summary>
        public string T01F02 { get; set; } // DepartmentName

        #endregion
    }
}