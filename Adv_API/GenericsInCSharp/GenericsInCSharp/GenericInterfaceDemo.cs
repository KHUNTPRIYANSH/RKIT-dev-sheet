using System;

namespace GenericsInCSharp
{
    #region IGenericInterface Definition
    /// <summary>
    /// This is a generic interface that defines methods for getting and setting values.
    /// It uses a generic type parameter T, which can be any data type (e.g., int, string, etc.).
    /// </summary>
    /// <typeparam name="T">The type parameter that will be used for the value in the implementing class.</typeparam>
    public interface IGenericInterface<T>
    {
        /// <summary>
        /// Sets a value of type T.
        /// </summary>
        /// <param name="value">The value of type T to be set.</param>
        void SetValue(T value);

        /// <summary>
        /// Gets the value of type T.
        /// </summary>
        /// <returns>The value of type T.</returns>
        T GetValue();
    }
    #endregion

    #region Concrete Implementation for int
    /// <summary>
    /// This class implements the IGenericInterface for the type int.
    /// It provides functionality to set and get an integer value.
    /// </summary>
    public class IntegerValue : IGenericInterface<int>
    {
        private int _value;

        public void SetValue(int value)
        {
            _value = value;
        }

        public int GetValue()
        {
            return _value;
        }
    }
    #endregion

    #region Concrete Implementation for string
    /// <summary>
    /// This class implements the IGenericInterface for the type string.
    /// It provides functionality to set and get a string value.
    /// </summary>
    public class StringValue : IGenericInterface<string>
    {
        private string _value;

        public void SetValue(string value)
        {
            _value = value;
        }

        public string GetValue()
        {
            return _value;
        }
    }
    #endregion

    #region GenericInterfaceDemo Class
    /// <summary>
    /// This class demonstrates how to use the IGenericInterface with different types.
    /// It demonstrates the set and get operations using integer and string values.
    /// </summary>
    public static class GenericInterfaceDemo
    {
        /// <summary>
        /// This method demonstrates how the IGenericInterface can be used with different data types.
        /// It creates instances for int and string, sets their values, and retrieves the values.
        /// </summary>
        public static void RunDemo()
        {
            #region IntegerValue Demonstration
            // Create an instance of IntegerValue (implements IGenericInterface<int>)
            IGenericInterface<int> intValue = new IntegerValue();
            intValue.SetValue(100);
            Console.WriteLine("Integer Value: " + intValue.GetValue()); // Output: Integer Value: 100
            #endregion

            #region StringValue Demonstration
            // Create an instance of StringValue (implements IGenericInterface<string>)
            IGenericInterface<string> stringValue = new StringValue();
            stringValue.SetValue("Hello, C#!");
            Console.WriteLine("String Value: " + stringValue.GetValue()); // Output: String Value: Hello, C#!
            #endregion
        }
    }
    #endregion
}
