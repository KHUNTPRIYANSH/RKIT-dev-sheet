using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DynamicType
{
    /// <summary>
    /// Demonstrates the usage of dynamic types in C#.
    /// </summary>
    public class Program
    {
        #region Properties and Fields

        /// <summary>
        /// Dynamic property that can store any type of value.
        /// </summary>
        dynamic randomVar { get; set; }

        /// <summary>
        /// Regular string field to hold a name value.
        /// </summary>
        string name;

        #endregion

        #region Methods

        /// <summary>
        /// A method that demonstrates the use of dynamic types.
        /// Combines a dynamic variable `firstname` and a string `lastname` to form a full name.
        /// </summary>
        /// <param name="d">A dynamic input parameter (not used in this demo).</param>
        /// <returns>A concatenated full name.</returns>
        public dynamic FullNameMethod(dynamic d)
        {
            name = "Priyansh";
            dynamic firstname = name;  // Dynamic variable holding a string value
            string lastname = "Khunt"; // Regular string variable

            return firstname + lastname; // Returns a dynamic result
        }

        #endregion

        #region Main Method

        /// <summary>
        /// Main entry point for the program, demonstrating various assignments to a dynamic variable.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Demonstration of Dynamic Type Usage in C# ===\n");

            // Declare a dynamic variable
            dynamic someVariable;

            #region Assign Different Types to Dynamic Variable

            // Assigning integer value
            someVariable = 5;
            Console.WriteLine($"Value: {someVariable}, Type: {someVariable.GetType()}");

            // Assigning char value
            someVariable = 'a';
            Console.WriteLine($"Value: {someVariable}, Type: {someVariable.GetType()}");

            // Assigning boolean value
            someVariable = false;
            Console.WriteLine($"Value: {someVariable}, Type: {someVariable.GetType()}");

            // Assigning double value
            someVariable = 5.3;
            Console.WriteLine($"Value: {someVariable}, Type: {someVariable.GetType()}");

            // Assigning string value
            someVariable = "Hii this is string";
            Console.WriteLine($"Value: {someVariable}, Type: {someVariable.GetType()}");

            // Assigning array of integers
            someVariable = new int[] { 1, 2, 3 };
            Console.WriteLine($"Value: {string.Join(",", someVariable)}, Type: {someVariable.GetType()}");

            // Assigning an instance of Program class
            someVariable = new Program();
            Console.WriteLine($"Value: Instance of Program class, Type: {someVariable.GetType()}");

            // Assigning a List<int>
            someVariable = new List<int> { 1, 2, 3, 4 };
            Console.WriteLine($"Value: {string.Join(",", someVariable)}, Type: {someVariable.GetType()}");

            #endregion

            #region Dynamic Type Explanation

            /*
             * The `dynamic` type is a feature in C# that allows variables to bypass compile-time type checking.
             * - It resolves the type at runtime.
             * - Useful for scenarios involving COM objects, reflection, dynamic data, or interop with dynamic languages.
             * - Be cautious when using dynamic types as runtime errors may occur if types are mismatched.
             */

            Console.WriteLine("\nDynamic type demonstration complete!");
            #endregion
        }

        #endregion
    }
}
