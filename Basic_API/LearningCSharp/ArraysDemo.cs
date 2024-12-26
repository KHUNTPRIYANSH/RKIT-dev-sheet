using System;

namespace LearningCSharp
{
    internal class ArraysDemo
    {
        public static void RunArrayDemo()
        {
            #region Single-Dimensional Array

            // A single-dimensional array
            int[] singleArray = new int[] { 1, 2, 3, 4, 5 };
            Console.WriteLine("=== Single-Dimensional Array ===");
            PrintArray(singleArray); // Using the PrintArray method

            #endregion

            #region Multi-Dimensional Array

            // A multi-dimensional array (2D array)
            int[,] multiArray = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Console.WriteLine("\n=== Multi-Dimensional Array ===");
            PrintMultiArray(multiArray); // Using the PrintMultiArray method

            #endregion

            #region Jagged Array

            // A jagged array (array of arrays)
            int[][] jaggedArray = new int[3][];
            jaggedArray[0] = new int[] { 1, 2 };
            jaggedArray[1] = new int[] { 3, 4, 5 };
            jaggedArray[2] = new int[] { 6, 7, 8, 9 };

            Console.WriteLine("\n=== Jagged Array ===");
            PrintJaggedArray(jaggedArray); // Using the PrintJaggedArray method

            #endregion
        }

        #region print array method

       

        // method to print single-dimensional arrays
        public static void PrintArray(int[] array)
        {
            // Loop through the array and print each element
            foreach (int item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine(); // To move to the next line after printing the array
        }

        // method to print multi-dimensional arrays (2D arrays)
        public static void PrintMultiArray(int[,] array)
        {
            // Loop through each row of the 2D array
            for (int i = 0; i < array.GetLength(0); i++)
            {
                // Loop through each column in the current row
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " "); // Print each element
                }
                Console.WriteLine(); // New line after each row
            }
        }

        // method to print jagged arrays (array of arrays)
        public static void PrintJaggedArray(int[][] array)
        {
            // Loop through each sub-array in the jagged array
            foreach (int[] subArray in array)
            {
                // Loop through each element in the sub-array
                foreach (int item in subArray)
                {
                    Console.Write(item + " "); // Print each element
                }
                Console.WriteLine(); // New line after each sub-array
            }
        }
        #endregion
    }
}
