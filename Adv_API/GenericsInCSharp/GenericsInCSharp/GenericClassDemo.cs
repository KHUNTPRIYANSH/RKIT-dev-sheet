using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsInCSharp
{
    #region GenericClass Demo
    /// <summary>
    /// The GenericClass<T> is a generic class that demonstrates the usage of a generic type parameter.
    /// It contains a property and a method that work with the type specified at runtime.
    /// </summary>
    /// <typeparam name="T">The type of the value that this class will work with. 
    /// It could be any data type (e.g., int, string, etc.).</typeparam>
    public class GenericClass<T>
    {
        // Private field to store the value of type T
        private T _value;

        #region Property: Value
        /// <summary>
        /// Gets or sets the value of type T.
        /// This property allows getting or setting the value that the generic class holds.
        /// </summary>
        public T Value{ get; set;}
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that initializes the Value property.
        /// </summary>
        /// <param name="value">The value to initialize the generic class with.</param>
        public GenericClass(T value)
        {
            _value = value;
        }
        #endregion

        #region Method: DisplayValue
        /// <summary>
        /// Displays the value stored in the generic class.
        /// The value can be of any type depending on the type passed when creating the instance.
        /// </summary>
        public void DisplayValue()
        {
            Console.WriteLine($"Value: {_value}");
        }
        #endregion
    }
    #endregion

    #region GenericClassDemo Class
    /// <summary>
    /// The GenericClassDemo class contains a static method that demonstrates how to use the GenericClass<T>.
    /// </summary>
    public static class GenericClassDemo
    {
        /// <summary>
        /// This static method demonstrates the usage of GenericClass<T> with different data types.
        /// It creates instances of the GenericClass with different types (int, string, bool, and double),
        /// displays their values, and modifies the values.
        /// </summary>
        public static void RunDemo()
        {
            #region Demonstrating GenericClass with int
            // Create an instance of GenericClass with int type
            GenericClass<int> intInstance = new GenericClass<int>(10);
            intInstance.DisplayValue(); // Output: Value: 10
            intInstance.Value = 20; // Changing the value using the property
            intInstance.DisplayValue(); // Output: Value: 20
            #endregion

            #region Demonstrating GenericClass with string
            // Create an instance of GenericClass with string type
            GenericClass<string> stringInstance = new GenericClass<string>("Hello, World!");
            stringInstance.DisplayValue(); // Output: Value: Hello, World!
            stringInstance.Value = "Generic Classes in C#"; // Changing the value using the property
            stringInstance.DisplayValue(); // Output: Value: Generic Classes in C#
            #endregion

            #region Demonstrating GenericClass with bool
            // Create an instance of GenericClass with bool type
            GenericClass<bool> boolInstance = new GenericClass<bool>(true);
            boolInstance.DisplayValue(); // Output: Value: True
            boolInstance.Value = false; // Changing the value using the property
            boolInstance.DisplayValue(); // Output: Value: False
            #endregion

            #region Demonstrating GenericClass with double
            // Create an instance of GenericClass with double type
            GenericClass<double> doubleInstance = new GenericClass<double>(3.14);
            doubleInstance.DisplayValue(); // Output: Value: 3.14
            doubleInstance.Value = 2.718; // Changing the value using the property
            doubleInstance.DisplayValue(); // Output: Value: 2.718
            #endregion
        }
    }
    #endregion

}
