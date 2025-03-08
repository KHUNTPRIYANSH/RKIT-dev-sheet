using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;

namespace FinalDemo.Models.POCO
{
    public class ATT01
    {
        #region Properties

        /// <summary>
        /// ATT01F01 = AttendanceId (Primary Key)
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int ATT01F01 { get; set; }

        /// <summary>
        /// ATT01F02 = UserId (Foreign Key to USR01)
        /// </summary>
        [ForeignKey(typeof(USR01))]
        public int ATT01F02 { get; set; }

        /// <summary>
        /// ATT01F03 = DepartmentId (Foreign Key to Department Table)
        /// </summary>
        [ForeignKey(typeof(DEPT01))] // Assuming DEP01 is your Department table
        public int ATT01F03 { get; set; }

        /// <summary>
        /// ATT01F04 = Attendance Date
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime ATT01F04 { get; set; }

        /// <summary>
        /// ATT01F05 = CheckInTime
        /// </summary>
        public string ATT01F05 { get; set; }

        /// <summary>
        /// ATT01F06 = CheckOutTime
        /// </summary>
        public string ATT01F06 { get; set; }

        /// <summary>
        /// ATT01F07 = Work Hours (Auto-calculated)
        /// </summary>
        [Computed] // Or [Ignore]
        public double ATT01F07
        {
            get
            {
                if (string.IsNullOrEmpty(ATT01F05) || string.IsNullOrEmpty(ATT01F06))
                    return 0;

                var checkIn = TimeSpan.Parse(ATT01F05);
                var checkOut = TimeSpan.Parse(ATT01F06);
                return (checkOut - checkIn).TotalHours;
            }
        }

        /// <summary>
        /// ATT01F08 = Status (Present, Absent, Half-Day, Leave, etc.)
        /// </summary>

        public string ATT01F08 { get; set; } = "Present";

        /// <summary>
        /// ATT01F09 = Remarks (Optional Notes)
        /// </summary>

        public string ATT01F09 { get; set; }

        /// <summary>
        /// ATT01F10 = IsLate (Auto-calculated based on company policy)
        /// </summary>
        [Computed] // Or [Ignore]
        public bool ATT01F10
        {
            get
            {
                if (string.IsNullOrEmpty(ATT01F05)) return false;
                var checkIn = TimeSpan.Parse(ATT01F05);
                return checkIn > new TimeSpan(9, 0, 0);
            }
        }


        /// <summary>
        /// ATT01F11 = CreatedDate (Record Creation Timestamp)
        /// </summary>
        public DateTime ATT01F11 { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// ATT01F12 = UpdatedDate (Last Modification Timestamp)
        /// </summary>
        public DateTime? ATT01F12 { get; set; }

        #endregion
    }
}
