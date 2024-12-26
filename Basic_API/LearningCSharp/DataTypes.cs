using System;

namespace LearningCSharp
{
    internal class DataTypes
    {
        /// <summary>
        /// Runs the data types code, demonstrating selected data types in C#.
        /// </summary>
        public static void RunDataTypesCode()
        {
            #region Value Types
            // Value types directly hold data in memory. They are stored on the stack.

            // Integer type (Default: 0, Range: -2,147,483,648 to 2,147,483,647, Size: 4 bytes)
            int integerExample = 42;

            // Floating-point type (Default: 0.0f, Approx. Range: -3.4E38 to 3.4E38, Size: 4 bytes)
            float floatExample = 3.14f;

            // Double type (Default: 0.0, Approx. Range: -1.7E308 to 1.7E308, Size: 8 bytes)
            double doubleExample = 42.42;

            // Character type (Default: '\0', Range: Unicode characters, Size: 2 bytes)
            char charExample = 'C';

            // Boolean type (Default: false, Values: true or false, Size: 1 byte)
            bool boolExample = true;

            /*
             * other value types :
             * 
             * byte byteExample = 255; // Default: 0, Range: 0 to 255, Size: 1 byte
             * short shortExample = -32768; // Default: 0, Range: -32,768 to 32,767, Size: 2 bytes
             * long longExample = 9223372036854775807L; // Default: 0, Range: Huge, Size: 8 bytes
             * decimal decimalExample = 3.1415926535M; // Default: 0.0M, High precision, Size: 16 bytes
             * sbyte sbyteExample = -128; // Default: 0, Range: -128 to 127, Size: 1 byte
             * ushort ushortExample = 65535; // Default: 0, Range: 0 to 65,535, Size: 2 bytes
             * uint uintExample = 4294967295; // Default: 0, Range: 0 to 4,294,967,295, Size: 4 bytes
             * ulong ulongExample = 18446744073709551615UL; // Default: 0, Huge range, Size: 8 bytes
             */

            #endregion

            #region Reference Types
            // Reference types store references to their data (objects) in memory (Heap).

            // String type (Default: null, Holds sequences of characters)
            string stringExample = "Hello, C#!";

            // Object type (Default: null, Base type for all data types)
            object objectExample = 100;

            // Dynamic type (Default: null, Resolves type at runtime)
            dynamic dynamicExample = "I am dynamic";

            /* 
             * Array example: int[] intArray = new int[] {1, 2, 3}; // Default: null, Size depends on elements
             * Class example: MyClass myClassInstance = new MyClass(); // Default: null
             * Interface example: IMyInterface myInterfaceInstance = null; // Default: null
             */

            #endregion

            #region Nullable Types
            // Nullable types allow value types to hold null values.

            // Nullable integer (Default: null)
            int? nullableInt = null;

            // Nullable boolean (Default: null)
            bool? nullableBool = null;

            #endregion

            #region Display Values
            // Displaying the values of the demonstrated data types.

            Console.WriteLine("=== Value Types ===");
            Console.WriteLine($"Integer: {integerExample} (Size: 4 bytes)");
            Console.WriteLine($"Float: {floatExample} (Size: 4 bytes)");
            Console.WriteLine($"Double: {doubleExample} (Size: 8 bytes)");
            Console.WriteLine($"Character: {charExample} (Size: 2 bytes)");
            Console.WriteLine($"Boolean: {boolExample} (Size: 1 byte)");

            Console.WriteLine("\n=== Reference Types ===");
            Console.WriteLine($"String: {stringExample}");
            Console.WriteLine($"Object: {objectExample}");
            Console.WriteLine($"Dynamic: {dynamicExample}");

            Console.WriteLine("\n=== Nullable Types ===");
            Console.WriteLine($"Nullable Integer normal output: {nullableInt} ");
            Console.WriteLine($"Nullable Integer: {nullableInt ?? 0} (Defaults to 0 if null)");
            Console.WriteLine($"Nullable Boolean: {nullableBool ?? false} (Defaults to false if null)");

            #endregion
        }
    }
}
