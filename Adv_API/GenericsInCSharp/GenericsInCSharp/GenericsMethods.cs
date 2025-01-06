using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsInCSharp
{
    #region Helper Class
    /// <summary>
    /// The Helper class contains overloaded methods for printing arrays of different types.
    /// These methods are not generic and are specific to the data types: int, float, char, string, and bool.
    /// </summary>
    internal class Helper
    {
        #region Print Array - int
        /// <summary>
        /// Prints the elements of an integer array.
        /// </summary>
        /// <param name="arr">The integer array to be printed.</param>
        public static void printArr(int[] arr)
        {
            Console.Write("\n[ ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                if (i != arr.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" ]");
        }
        #endregion

        #region Print Array - float
        /// <summary>
        /// Prints the elements of a float array.
        /// </summary>
        /// <param name="arr">The float array to be printed.</param>
        public static void printArr(float[] arr)
        {
            Console.Write("\n[ ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                if (i != arr.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" ]");
        }
        #endregion

        #region Print Array - char
        /// <summary>
        /// Prints the elements of a char array.
        /// </summary>
        /// <param name="arr">The char array to be printed.</param>
        public static void printArr(char[] arr)
        {
            Console.Write("\n[ ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                if (i != arr.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" ]");
        }
        #endregion

        #region Print Array - string
        /// <summary>
        /// Prints the elements of a string array.
        /// </summary>
        /// <param name="arr">The string array to be printed.</param>
        public static void printArr(string[] arr)
        {
            Console.Write("\n[ ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                if (i != arr.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" ]");
        }
        #endregion

        #region Print Array - bool
        /// <summary>
        /// Prints the elements of a boolean array.
        /// </summary>
        /// <param name="arr">The boolean array to be printed.</param>
        public static void printArr(bool[] arr)
        {
            Console.Write("\n[ ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                if (i != arr.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" ]");
        }
        #endregion
    }
    #endregion

    #region GenericsHelper Class
    /// <summary>
    /// The GenericsHelper class contains a generic method that can print arrays of any type.
    /// </summary>
    internal class GenericsHelper
    {
        #region Generic Print Array
        /// <summary>
        /// Prints the elements of a generic array.
        /// </summary>
        /// <typeparam name="T">The type of the array (e.g., int, float, char, etc.).</typeparam>
        /// <param name="arr">The array to be printed.</param>
        public static void printArr<T>(T[] arr)
        {
            Console.Write("\nUsing Generic Method: [ ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                if (i != arr.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" ]");
        }
        #endregion
    }
    #endregion

    #region GenericsMethods Class
    /// <summary>
    /// The GenericsMethods class is used to run the demonstration of both non-generic and generic methods
    /// for printing arrays of different types.
    /// </summary>
    internal class GenericsMethods
    {
        #region RunGenericsMethods
        /// <summary>
        /// This method runs various methods to demonstrate printing arrays using both non-generic and generic approaches.
        /// </summary>
        public static void RunGenericsMethods()
        {
            // Initialize arrays of different types
            int[] arr = { 1, 2, 3 };
            float[] arr2 = { 1.0f, 2.2f, 3.3f };
            char[] arr3 = { 'a', 'b', 'c' };
            string[] arr4 = { "ABC", "DEF" };
            bool[] arr5 = { true, false };

            // Using non-generic methods from Helper class
            Helper.printArr(arr);
            Helper.printArr(arr2);
            Helper.printArr(arr3);
            Helper.printArr(arr4);
            Helper.printArr(arr5);

            // Using generic methods from GenericsHelper class
            GenericsHelper.printArr(arr);
            GenericsHelper.printArr(arr2);
            GenericsHelper.printArr(arr3);
            GenericsHelper.printArr(arr4);
            GenericsHelper.printArr(arr5);
        }
        #endregion
    }
    #endregion
}
