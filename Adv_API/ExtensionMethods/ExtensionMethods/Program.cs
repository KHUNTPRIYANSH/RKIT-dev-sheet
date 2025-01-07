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
            string[] digitWords = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            return string.Join(" ", numberString.ToCharArray().Select(c => digitWords[c - '0'].ToUpper()));
            // in above line we converting string number into char array so "1234" become '1' , '2' , '3' , '4'.
            // then we take each char from char array using lambda expression we find index of it's word from digitWords and
            // return array pf string as a result
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

        /// <summary>
        /// Returns the first item in a list, or default if empty.
        /// </summary>
        public static T FirstOrDefault<T>(this List<T> list) => list.Count > 0 ? list[0] : default;

        /// <summary>
        /// Returns the top item in a stack, or default if empty.
        /// </summary>
        public static T PeekOrDefault<T>(this Stack<T> stack) => stack.Count > 0 ? stack.Peek() : default;
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
            string number = "1234";
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

            // List: FirstOrDefault
            List<int> numbers = new List<int> { 1, 2, 3 };
            Console.WriteLine($"First in list: {numbers.FirstOrDefault()}");

            // Empty list example
            List<int> emptyList = new List<int>();
            Console.WriteLine($"First in empty list: {emptyList.FirstOrDefault()}");

            // Stack: PeekOrDefault
            Stack<string> stack = new Stack<string>();
            stack.Push("Hello");
            Console.WriteLine($"Top of stack: {stack.PeekOrDefault()}");

            // Empty stack example
            Stack<string> emptyStack = new Stack<string>();
            Console.WriteLine($"Top of empty stack: {emptyStack.PeekOrDefault()}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
