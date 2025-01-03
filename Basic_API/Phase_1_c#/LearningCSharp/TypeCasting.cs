using System;

namespace LearningCSharp
{
    /// <summary>
    /// Demonstrates various types of type casting in C#, including differences between Convert, Parse, and TryParse methods.
    /// </summary>
    internal class TypeCasting
    {
        /// <summary>
        /// Runs the type casting demonstrations.
        /// </summary>
        public static void RunTypeCastingDemo()
        {
            #region Implicit Casting (Safe Casting)
            int num1 = 42; // Integer (4 bytes)
            double num2 = num1; // Implicit casting to double (8 bytes)
            Console.WriteLine("=== Implicit Casting ===");
            Console.WriteLine($"Integer value: {num1} (4 bytes)");
            Console.WriteLine($"Implicitly casted to double: {num2} (8 bytes)");
            #endregion

            #region Explicit Casting (Manual Casting)
            double num3 = 42.58; // Double value (8 bytes)
            int num4 = (int)num3; // Explicit casting to int (4 bytes)
            Console.WriteLine("\n=== Explicit Casting ===");
            Console.WriteLine($"Double value: {num3} (8 bytes)");
            Console.WriteLine($"Explicitly casted to integer (truncated): {num4} (4 bytes)");
            #endregion

            #region Convert Class
            
            // Convert class safely converts between types.
            
            string numericString = "12345";
            int convertedInt = Convert.ToInt32(numericString); // Converts string to integer

            Console.WriteLine("\n=== Convert Class ===");
            Console.WriteLine($"String value: \"{numericString}\"");
            Console.WriteLine($"Converted to integer using Convert.ToInt32: {convertedInt}");

            // Converts string to other types
            
            string floatString = "42.42";
            double convertedDouble = Convert.ToDouble(floatString);
            Console.WriteLine($"Converted to double using Convert.ToDouble: {convertedDouble}");

            // Note: Convert handles null input gracefully by returning default values

            string nullString = null;
            int defaultInt = Convert.ToInt32(nullString);
            Console.WriteLine($"Convert.ToInt32(null) returns: {defaultInt} (default value for int)");
            
            #endregion

            #region Parse Method
            // The Parse method is used to convert strings to specific types.
            // Throws an exception if the input is invalid or null.
            string parseString = "456";
            int parsedInt = int.Parse(parseString); // Parses string to integer
            Console.WriteLine("\n=== Parse Method ===");
            Console.WriteLine($"String value: \"{parseString}\"");
            Console.WriteLine($"Parsed to integer using int.Parse: {parsedInt}");

            // Parse supports other data types
            string parseDoubleString = "123.45";
            double parsedDouble = double.Parse(parseDoubleString); // Parses string to double
            Console.WriteLine($"Parsed to double using double.Parse: {parsedDouble}");

            // Uncomment below to see exceptions for invalid input
            // string invalidParseString = "NotANumber";
            // int errorInt = int.Parse(invalidParseString); // Throws FormatException
            
            #endregion

            #region TryParse Method
            // TryParse is a safer alternative to Parse as it avoids exceptions.
            
            string tryParseString = "789";
            int tryParsedInt;
            bool parseSuccess = int.TryParse(tryParseString, out tryParsedInt); // Safely attempts to parse

            Console.WriteLine("\n=== TryParse Method ===");
            Console.WriteLine($"String value: \"{tryParseString}\"");
            Console.WriteLine($"Parse successful: {parseSuccess}, Parsed integer: {tryParsedInt}");

            // Handles invalid input gracefully without throwing exceptions
            string invalidTryParseString = "Invalid123";
            bool invalidParseSuccess = int.TryParse(invalidTryParseString, out int invalidResult);
            Console.WriteLine($"Invalid input \"{invalidTryParseString}\" parse success: {invalidParseSuccess}, Result: {invalidResult}");
            #endregion

            #region Difference Between Convert, Parse, and TryParse
            // Convert:
            // - Converts null values to default type value (e.g., null to 0 for int).
            // - Handles various input types (e.g., bool to int, double to string).
            // - Throws exceptions for invalid formats.
            //
            // Parse:
            // - Converts strings to specific types.
            // - Throws exceptions for invalid or null input.
            //
            // TryParse:
            // - Safe alternative to Parse.
            // - Does not throw exceptions; returns false for invalid input.
            //
            // When to Use:
            // - Use Convert for versatile conversions with null handling.
            // - Use Parse if input validity is guaranteed.
            // - Use TryParse when input validity is uncertain.
            #endregion

            #region Boxing and Unboxing
            // Boxing converts a value type to an object.
            // Unboxing retrieves the value type from the object.

            int boxValue = 99;
            object boxed = boxValue; // Boxing
            int unboxed = (int)boxed; // Unboxing
            Console.WriteLine("\n=== Boxing and Unboxing ===");
            Console.WriteLine($"Original value (int): {boxValue}");
            Console.WriteLine($"Boxed into object: {boxed}");
            Console.WriteLine($"Unboxed back to int: {unboxed}");
            #endregion
        }
    }
}
