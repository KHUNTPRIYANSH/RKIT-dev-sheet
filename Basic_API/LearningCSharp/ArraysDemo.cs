using System;

namespace LearningCSharp
{
    internal class ArraysDemo
    {
        public static void RunArrayDemo()
        {
            #region Single-Dimensional Array
         
            int[] singleArray = new int[] { 1, 2, 3, 4, 5 };
            Console.WriteLine("=== Single-Dimensional Array ===");
            PrintArray(singleArray); 

            #endregion

            #region Multi-Dimensional Array

            int[,] multiArray = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Console.WriteLine("\n=== Multi-Dimensional Array ===");
            PrintMultiArray(multiArray); 

            #endregion

            #region Jagged Array

            // A jagged array (array of arrays)
            int[][] jaggedArray = new int[3][];
            jaggedArray[0] = new int[] { 1, 2 };
            jaggedArray[1] = new int[] { 3, 4, 5 };
            jaggedArray[2] = new int[] { 6, 7, 8, 9 };

            Console.WriteLine("\n=== Jagged Array ===");
            PrintJaggedArray(jaggedArray); 

            #endregion
        }

        #region print array method
 
        public static void PrintArray(int[] array)
        {
            foreach (int item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine(); 
        }

        public static void PrintMultiArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine(); 
            }
        }

        public static void PrintJaggedArray(int[][] array)
        {
            foreach (int[] subArray in array)
            {
                foreach (int item in subArray)
                {
                    Console.Write(item + " "); 
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
