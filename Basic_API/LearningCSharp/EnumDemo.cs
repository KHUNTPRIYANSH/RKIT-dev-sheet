using System;

namespace LearningCSharp
{
    #region Enum DaysOfWeek

    /// <summary>
    /// Enum DaysOfWeek to represent the days of the week.
    /// </summary>
    public enum DaysOfWeek
    {
        Sunday = 1,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    #endregion

    #region Enum Demonstration Class

    /// <summary>
    /// Class to demonstrate how to use enums in C#.
    /// </summary>
    public class EnumDemo
    {
        /// <summary>
        /// Demonstrates the usage of enum DaysOfWeek.
        /// </summary>
        public static void RunEnumDemo()
        {
            #region Enum Initialization and Usage

            // Initialize a variable of type DaysOfWeek enum
            DaysOfWeek today = DaysOfWeek.Wednesday;

            // Display the value of today
            Console.WriteLine($"Today is: {today}");

            // Get the integer value of the enum
            int dayNumber = (int)today;
            Console.WriteLine($"Numeric value of {today} is: {dayNumber}");

            // Use the enum in a switch statement
            switch (today)
            {
                case DaysOfWeek.Sunday:
                    Console.WriteLine("It's a relaxing Sunday.");
                    break;

                case DaysOfWeek.Monday:
                    Console.WriteLine("Start of the work week.");
                    break;

                case DaysOfWeek.Wednesday:
                    Console.WriteLine("It's Wednesday, mid-week.");
                    break;

                case DaysOfWeek.Friday:
                    Console.WriteLine("It's Friday, the weekend is near.");
                    break;

                default:
                    Console.WriteLine("It's just another day.");
                    break;
            }

            #endregion

        }

    }

    #endregion
}
