using System;

namespace LearningCSharp
{
    /// <summary>
    /// Demonstrates various operators supported in C#.
    /// </summary>
    internal class OperatorsDemo
    {
        /// <summary>
        /// Runs the operators demonstration code.
        /// </summary>
        public static void RunOperatorsDemo()
        {
            #region Arithmetic Operators
            // Arithmetic operators are used to perform basic mathematical operations.

            int a = 10, b = 5;
            Console.WriteLine("=== Arithmetic Operators ===");
            Console.WriteLine($"a + b = {a + b}"); // Addition
            Console.WriteLine($"a - b = {a - b}"); // Subtraction
            Console.WriteLine($"a * b = {a * b}"); // Multiplication
            Console.WriteLine($"a / b = {a / b}"); // Division
            Console.WriteLine($"a % b = {a % b}"); // Modulus (Remainder)
            #endregion

            #region Relational Operators
            // Relational operators are used to compare two values.

            int x = 10, y = 20;
            Console.WriteLine("\n=== Relational Operators ===");
            Console.WriteLine($"x == y: {x == y}"); // Equal to
            Console.WriteLine($"x != y: {x != y}"); // Not equal to
            Console.WriteLine($"x > y: {x > y}"); // Greater than
            Console.WriteLine($"x < y: {x < y}"); // Less than
            Console.WriteLine($"x >= y: {x >= y}"); // Greater than or equal to
            Console.WriteLine($"x <= y: {x <= y}"); // Less than or equal to
            #endregion

            #region Logical Operators
            // Logical operators are used to perform logical operations.

            bool condition1 = true, condition2 = false;
            Console.WriteLine("\n=== Logical Operators ===");
            Console.WriteLine($"condition1 && condition2: {condition1 && condition2}"); // AND
            Console.WriteLine($"condition1 || condition2: {condition1 || condition2}"); // OR
            Console.WriteLine($"!condition1: {!condition1}"); // NOT
            #endregion

            #region Bitwise Operators
            // Bitwise operators perform operations on the binary representation of numbers.

            int num1 = 5, num2 = 3; // Binary: 101, 011
            Console.WriteLine("\n=== Bitwise Operators ===");
            Console.WriteLine($"num1 & num2: {num1 & num2}"); // AND
            Console.WriteLine($"num1 | num2: {num1 | num2}"); // OR
            Console.WriteLine($"num1 ^ num2: {num1 ^ num2}"); // XOR
            Console.WriteLine($"~num1: {~num1}"); // NOT (inverts bits)
            Console.WriteLine($"num1 << 1: {num1 << 1}"); // Left shift
            Console.WriteLine($"num1 >> 1: {num1 >> 1}"); // Right shift
            #endregion

            #region Assignment Operators
            // Assignment operators are used to assign values to variables.

            int z = 10;
            Console.WriteLine("\n=== Assignment Operators ===");
            z += 5; // Add and assign
            Console.WriteLine($"z += 5: {z}");
            z -= 3; // Subtract and assign
            Console.WriteLine($"z -= 3: {z}");
            z *= 2; // Multiply and assign
            Console.WriteLine($"z *= 2: {z}");
            z /= 4; // Divide and assign
            Console.WriteLine($"z /= 4: {z}");
            z %= 3; // Modulo and assign
            Console.WriteLine($"z %= 3: {z}");
            #endregion

            #region Unary Operators
            // Unary operators operate on a single operand.

            int count = 10;
            Console.WriteLine("\n=== Unary Operators ===");
            Console.WriteLine($"++count: {++count}"); // Pre-increment
            Console.WriteLine($"count++: {count++}"); // Post-increment
            Console.WriteLine($"--count: {--count}"); // Pre-decrement
            Console.WriteLine($"count--: {count--}"); // Post-decrement
            Console.WriteLine($"~count: {~count}"); // Bitwise NOT (invert bits)
            #endregion

            #region Conditional (Ternary) Operator
            // The ternary operator is a shorthand for if-else.

            int result = (x > y) ? x : y; // If x is greater than y, result = x, otherwise result = y
            Console.WriteLine("\n=== Conditional (Ternary) Operator ===");
            Console.WriteLine($"(x > y) ? x : y = {result}");
            #endregion

            #region Null Coalescing Operator
            // The null-coalescing operator is used to assign a default value if the variable is null.

            string name = null;
            Console.WriteLine("\n=== Null Coalescing Operator ===");
            string defaultName = name ?? "Unknown"; // If name is null, use "Unknown"
            Console.WriteLine($"Name: {defaultName}");
            #endregion


            #region Is and As Operators
            // The "is" operator checks if an object is of a specific type.
            // The "as" operator attempts to cast an object to a specified type.

            object obj = "Hello, C#";
            Console.WriteLine("\n=== Is and As Operators ===");
            Console.WriteLine($"obj is string: {obj is string}"); // Checks if obj is a string
            string strValue = obj as string; // Casts obj to string, returns null if not possible
            Console.WriteLine($"obj as string: {strValue}");
            #endregion
        }
    }
}
