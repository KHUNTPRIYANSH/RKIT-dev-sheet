using System;

namespace LearningCSharp
{
    /// <summary>
    /// Demonstrates various flow control statements in C#.
    /// </summary>
    internal class FlowControlDemo
    {
        /// <summary>
        /// Runs the flow control demonstration code.
        /// </summary>
        public static void RunFlowControlDemo()
        {
            #region If-Else Statement

            int number = 10;
            Console.WriteLine("=== If-Else Statement ===");
            if (number > 0)
            {
                Console.WriteLine("Number is positive.");
            }
            else
            {
                Console.WriteLine("Number is not positive.");
            }
            #endregion

            #region If-Else If Ladder

            Console.WriteLine("\n=== If-Else If Ladder ===");
            int score = 85;
            if (score >= 90)
            {
                Console.WriteLine("Grade: A");
            }
            else if (score >= 70)
            {
                Console.WriteLine("Grade: B");
            }
            else if (score >= 50)
            {
                Console.WriteLine("Grade: C");
            }
            else
            {
                Console.WriteLine("Grade: F");
            }
            #endregion

            #region Nested If-Else Statement

            Console.WriteLine("\n=== Nested If-Else Statement ===");
            int age = 20;
            if (age >= 18)
            {
                if (age >= 21)
                {
                    Console.WriteLine("You are an adult and can drink alcohol.");
                }
                else
                {
                    Console.WriteLine("You are an adult but cannot drink alcohol.");
                }
            }
            else
            {
                Console.WriteLine("You are a minor.");
            }
            #endregion

            #region Switch Statement

            Console.WriteLine("\n=== Switch Statement ===");
            string day = "Monday";
            switch (day)
            {
                case "Monday":
                    Console.WriteLine("Start of the work week.");
                    break;
                case "Wednesday":
                    Console.WriteLine("Midweek day.");
                    break;
                case "Friday":
                    Console.WriteLine("End of the work week.");
                    break;
                default:
                    Console.WriteLine("Just another day.");
                    break;
            }
            #endregion

            #region For Loop

            Console.WriteLine("\n=== For Loop ===");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"For loop iteration: {i}");
            }
            #endregion

            #region While Loop

            Console.WriteLine("\n=== While Loop ===");
            int count = 0;
            while (count < 3)
            {
                Console.WriteLine($"While loop iteration: {count}");
                count++;
            }
            #endregion

            #region Do-While Loop

            Console.WriteLine("\n=== Do-While Loop ===");
            int n = 0;
            do
            {
                Console.WriteLine($"Do-While loop iteration: {n}");
                n++;
            }
            while (n < 2);
            #endregion

            #region Break Statement

            Console.WriteLine("\n=== Break Statement ===");
            for (int i = 0; i < 5; i++)
            {
                if (i == 3)
                {
                    break; // Exit loop when i is 3
                }
                Console.WriteLine($"Iteration: {i}");
            }
            #endregion

            #region Continue Statement

            Console.WriteLine("\n=== Continue Statement ===");
            for (int i = 0; i < 5; i++)
            {
                if (i == 2)
                {
                    continue; // Skip when i is 2
                }
                Console.WriteLine($"Iteration: {i}");
            }
            #endregion

            #region Goto Statement

            Console.WriteLine("\n=== Goto Statement ===");
            int number2 = 3;
            if (number2 == 3)
            {
                goto SkipMessage; // Jump to the label SkipMessage
            }
            Console.WriteLine("This will be skipped.");

        SkipMessage:
            Console.WriteLine("Goto statement was used!");
            #endregion
        }
    }
}
