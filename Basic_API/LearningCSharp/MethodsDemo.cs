using System;

namespace LearningCSharp
{
    internal class MethodsDemo
    {
        #region Method Without Return Type
        public static void MethodWithoutReturnType()
        {
            Console.WriteLine("This method doesn't return any value.");
        }
        #endregion

        #region Method With Return Type
        public static int MethodWithReturnType()
        {
            return 42;
        }
        #endregion

        #region Method With Parameters
        public static void MethodWithParameters(int num1, int num2)
        {
            Console.WriteLine($"The sum of {num1} and {num2} is: {num1 + num2}");
        }
        #endregion

        #region Method With Return Type and Parameters
        public static int MethodWithReturnTypeAndParameters(int num1, int num2)
        {
            return num1 * num2;
        }
        #endregion

        #region Method With Ref Keyword
        public static void MethodWithRefKeyword(ref int num)
        {
            num *= 2; // This modifies the original value of the variable
        }
        #endregion

        #region Method With Out Keyword
        public static void MethodWithOutKeyword(out int num)
        {
            num = 100; // Output parameter must be assigned a value before returning
        }
        #endregion
  

        #region Method With Default Parameters
        public static void MethodWithDefaultParameters(int num1, int num2 = 10)
        {
            Console.WriteLine($"The sum is: {num1 + num2}");
        }
        #endregion

        #region Method Using Params Keyword
        public static void MethodWithParams(params int[] numbers)
        {
            Console.WriteLine("Numbers received: ");
            foreach (var num in numbers)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
        #endregion

        #region Main Method to Demonstrate All Other Methods
        public static void RunMethodDemo()
        {
            Console.WriteLine("=== Method Without Return Type ===");
            MethodWithoutReturnType();

            Console.WriteLine("\n=== Method With Return Type ===");
            int result = MethodWithReturnType();
            Console.WriteLine($"Returned value: {result}");

            Console.WriteLine("\n=== Method With Parameters ===");
            MethodWithParameters(5, 10);

            Console.WriteLine("\n=== Method With Return Type and Parameters ===");
            int product = MethodWithReturnTypeAndParameters(6, 7);
            Console.WriteLine($"Product: {product}");

            Console.WriteLine("\n=== Method With Ref Keyword ===");
            int number = 5;
            MethodWithRefKeyword(ref number);
            Console.WriteLine($"Ref value after method call: {number}");

            Console.WriteLine("\n=== Method With Out Keyword ===");
            MethodWithOutKeyword(out int outputNumber);
            Console.WriteLine($"Out value: {outputNumber}");

            Console.WriteLine("\n=== Method With Default Parameters ===");
            MethodWithDefaultParameters(5);
            MethodWithDefaultParameters(5, 15);

            Console.WriteLine("\n=== Method With Params Keyword ===");
            MethodWithParams(1, 2, 3, 4, 5);
        }
        #endregion
    }
}
