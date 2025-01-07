using System;
using System.Linq.Expressions;

namespace LambdaDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Lambda expressions in C# are used like anonymous functions, with the difference that in Lambda  expressions you don’t need to specify the type of the value that you input thus making it more flexible to use. 
            // The ‘=>’ is the lambda operator which is used in all lambda expressions.The Lambda expression  is divided into two parts, the left side is the input and the right is the expression.

            // The Lambda Expressions can be of two types: 

            // Expression Lambda: Consists of the input and the expression.
            // Syntax:
            //          input => expression;
            List<int> nums = new List<int>() { 1,2,3,4,5,6,7,8};
            var square = nums.Select(n => n*n);
            foreach(var n in nums) 
            {
                Console.Write(n + " ");
            }
            Console.WriteLine(  );


            // Statement Lambda: Consists of the input and a set of statements to be executed.
            // Syntax:
            //          input => { statements };
            //          input => { statements };
            //          input => { statements };

            List<string> names = new List<string>() { "Parth", "Keyur", "Priyansh" };

            // Using Select to create a new list of greeting strings
            List<string> greet = names.Select(nm =>
            {
                string greeting = "Hello, " + nm + " !!!";
                Console.WriteLine("Intermediate result current from lambda statemt : " +greeting);  // Print each greeting
                return greeting;  // Return the greeting string
            }).ToList();  // To force evaluation and store the results in a list

            // Optional: You can print the result of the 'greet' list if needed
            Console.WriteLine("\nAll greetings:");
            foreach (var item in greet)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine();
        }
    }
}