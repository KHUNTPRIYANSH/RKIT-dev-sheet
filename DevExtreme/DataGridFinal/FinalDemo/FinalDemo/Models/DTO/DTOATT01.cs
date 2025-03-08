using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace FinalDemo.Models.DTO
{
    public class DTOATT01
    {
        /// <summary>
        /// ATT01F01 = AttendanceId (Primary Key)
        /// </summary>
        public int ATT01F01 { get; set; }

        /// <summary>
        /// ATT01F02 = EmployeeId (Foreign Key to EMP01)
        /// </summary>
        public int ATT01F02 { get; set; }

        /// <summary>
        /// ATT01F03 = DepartmentId (Foreign Key to Department Table)
        /// </summary>
        public int ATT01F03 { get; set; }

        /// <summary>
        /// ATT01F04 = Attendance Date
        /// </summary>
        public DateTime ATT01F04 { get; set; }

        /// <summary>
        /// ATT01F05 = Check-In Time
        /// </summary>
        public string ATT01F05 { get; set; }

        /// <summary>
        /// ATT01F06 = Check-Out Time
        /// </summary>
        public string ATT01F06 { get; set; }

        /// <summary>
        /// ATT01F08 = Status (Present, Absent, Half-Day, Leave, etc.)
        /// </summary>
        public string ATT01F08 { get; set; }

        /// <summary>
        /// ATT01F09 = Remarks (Optional Notes)
        /// </summary>
        public string ATT01F09 { get; set; }
    }

}