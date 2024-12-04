using System;

namespace VariableDemoApp
{
    // Demonstrates different types of variables in C#
    public class VariableTypesDemo
    {
        #region Private Members

        // Private field to hold a sample string value
        private string _privateString;

        #endregion

        #region Public Properties

        // Public property to access the private string
        public string PrivateString
        {
            get { return _privateString; }
            set { _privateString = value; }
        }

        #endregion

        #region Constructors

        // Default constructor
        public VariableTypesDemo()
        {
            _privateString = "Default Value";
        }

        #endregion

        #region Public Methods

        // Method to demonstrate all variable types
        public void ShowVariableTypes()
        {
            // Local variable (declared inside a method)
            int localVariable = 10;

            // Constant variable (value cannot be changed after initialization)
            const double Pi = 3.14159;

            // Read-only variable (can only be assigned in the constructor or declaration)
            Console.WriteLine($"Read-only variable (set in constructor): {_privateString}");

            // Value types (store data directly)
            int integerValue = 42;                // Integer type
            double doubleValue = 42.42;           // Double-precision floating-point
            char charValue = 'A';                 // Character type
            bool booleanValue = true;             // Boolean type

            // Reference types (store references to memory locations)
            string stringValue = "Hello, C#";     // String type
            object objectValue = 123;             // Object type (can store any data type)

            // Nullable types (allow variables to hold null)
            int? nullableInt = null;

            // Displaying variable values
            Console.WriteLine("=== Value Types ===");
            Console.WriteLine($"Integer: {integerValue}");
            Console.WriteLine($"Double: {doubleValue}");
            Console.WriteLine($"Character: {charValue}");
            Console.WriteLine($"Boolean: {booleanValue}");

            Console.WriteLine("=== Reference Types ===");
            Console.WriteLine($"String: {stringValue}");
            Console.WriteLine($"Object: {objectValue}");

            Console.WriteLine("=== Nullable Types ===");
            Console.WriteLine($"Nullable Integer: {nullableInt ?? 0} (Defaulted to 0 if null)");
        }

        // Static method to demonstrate a static variable
        public static void ShowStaticVariable()
        {
            // Static variable (shared across all instances of the class)
            static int staticCounter = 0;

            staticCounter++;
            Console.WriteLine($"Static Counter: {staticCounter}");
        }

        #endregion

        #region Private Methods

        // Private helper method to demonstrate internal usage
        private void PrivateHelper()
        {
            Console.WriteLine("This is a private helper method.");
        }

        #endregion
    }

    // Main program to test the class
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of VariableTypesDemo
            VariableTypesDemo demo = new VariableTypesDemo();

            // Display variable types
            demo.ShowVariableTypes();

            // Show the static variable
            VariableTypesDemo.ShowStaticVariable();

            // Modify and display the private string via the public property
            demo.PrivateString = "Modified Value";
            Console.WriteLine($"Updated PrivateString Property: {demo.PrivateString}");
        }
    }
}
