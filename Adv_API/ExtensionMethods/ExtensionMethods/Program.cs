using System;
using System.Collections.Generic;

namespace ExtensionMethods
{
    /// <summary>
    /// A static class containing simple extension methods for various types.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts a numeric string into its word representation.
        /// </summary>
        public static string DescribeNumber(this string numberString)
        {
            // Array to map digits to words
            string[] digitWords = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            // Initialize an empty list to store word representations of each digit
            List<string> wordList = new List<string>();

            // Loop through each character in the input string (which represents a number)
            foreach (char digit in numberString)
            {
                // Convert the character to its integer value and use that as an index to fetch the word
                int index = digit - '0'; // Convert char digit to an integer
                string word = digitWords[index].ToUpper(); // Get the word corresponding to the digit and make it uppercase

                // Add the word to the list
                wordList.Add(word);
            }

            // Join the list of words with spaces in between and return the result
            return string.Join(" ", wordList);
        }


        /// <summary>
        /// Checks if an integer is even.
        /// </summary>
        public static bool IsEven(this int number) => number % 2 == 0;

        /// <summary>
        /// Negates a boolean value.
        /// </summary>
        public static bool Negate(this bool value) => !value;


        /// <summary>
        /// Converts a DateTime to a short date string.
        /// </summary>
        public static string ToShortDate(this DateTime dateTime) => dateTime.ToString("d");

        }

    /// <summary>
    /// A demo program to showcase simplified extension methods.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point for the demo.
        /// </summary>
        private static void Main()
        {
            // String: DescribeNumber
            string number = "123456";
            Console.WriteLine($"DescribeNumber: {number.DescribeNumber()}");

            // Integer: IsEven
            int num = 10;
            Console.WriteLine($"{num} is even: {num.IsEven()}");

            // Boolean: Negate
            bool flag = true;
            Console.WriteLine($"Negated value: {flag.Negate()}");

            // DateTime: ToShortDate
            DateTime today = DateTime.Now;
            Console.WriteLine($"Short date: {today.ToShortDate()}");

           

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
