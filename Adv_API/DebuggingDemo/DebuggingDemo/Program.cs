using System;

namespace DebuggingDemo
{


    class SimpleCalculator
    {
        // Function for addition
        static double Add(double a, double b)
        {
            return a + b;
        }

        // Function for subtraction
        static double Subtract(double a, double b)
        {
            return a - b;
        }

        // Function for multiplication
        static double Multiply(double a, double b)
        {
            return a * b;
        }

        // Function for division
        static double Divide(double a, double b)
        {
            if (b != 0)
                return a / b;
            else
                return double.NaN; // Return NaN for division by zero
        }

        // Function for multiplication table
        static void MultiplicationTable(int n)
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{n} x {i} = {n * i}");
            }
        }

        static void Main(string[] args)
        {
            while(true)
            {

            Console.WriteLine("Welcome to the Simple Calculator");

            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Multiply");
            Console.WriteLine("4. Divide");
            Console.WriteLine("5. Multiplication Table");
            Console.WriteLine("6. Exit");

            // Taking operation choice input
            int choice = int.Parse(Console.ReadLine());

             if (choice == 6) break;
            // Take inputs for numbers
            double a = 0, b = 0;
            int n = 0;

            if (choice >= 1 && choice <= 4)
            {
                Console.Write("Enter first number: ");
                a = double.Parse(Console.ReadLine());
                Console.Write("Enter second number: ");
                b = double.Parse(Console.ReadLine());
            }
            else if (choice == 5)
            {
                Console.Write("Enter the number for the multiplication table: ");
                n = int.Parse(Console.ReadLine());
            }

            // Call the appropriate function based on the choice
            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Result: {Add(a, b)}");
                    break;
                case 2:
                    Console.WriteLine($"Result: {Subtract(a, b)}");
                    break;
                case 3:
                    Console.WriteLine($"Result: {Multiply(a, b)}");
                    break;
                case 4:
                    double result = Divide(a, b);
                    if (double.IsNaN(result))
                        Console.WriteLine("Error! Division by zero.");
                    else
                        Console.WriteLine($"Result: {result}");
                    break;
                case 5:
                    MultiplicationTable(n);
                    break; ;
                default:
                    Console.WriteLine("Invalid choice! Please enter a valid operation.");
                    break;
            }
            }
        }
    }
}