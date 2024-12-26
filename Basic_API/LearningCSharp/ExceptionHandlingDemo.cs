using System;

namespace LearningCSharp;

/// <summary>
/// Demonstrates various exception handling techniques in C#, including basic exception handling,
/// custom exceptions, exception filters, and logging.
/// </summary>
public class ExceptionHandlingDemo
{
    /// <summary>
    /// Executes different exception handling scenarios, demonstrating try-catch-finally,
    /// custom exceptions, exception properties, and other features.
    /// </summary>
    public static void RunExceptionHandlingDemo()
    {
        #region Basic Exception Handling with Finally Block

        try
        {
            string input = "123a";
            int parsedNumber = int.Parse(input); // This will cause a FormatException
            Console.WriteLine("Parsed number: " + parsedNumber);
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Exception: Invalid format - " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed for Basic Exception Handling.");
        }

        #endregion

        #region Custom Exception Demonstration

        try
        {
            // Throwing a custom exception for invalid operation
            throw new InvalidOperationException("Invalid operation occurred in the process.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Custom Exception: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed for Custom Exception Handling.");
        }

        #endregion

        #region Exception Properties Example

        try
        {
            object obj = null;
            obj.ToString(); // This will cause a NullReferenceException
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            Console.WriteLine("Source: " + ex.Source);
        }
        finally
        {
            Console.WriteLine("Finally block executed for Exception Properties Example.");
        }

        #endregion

        #region Multiple Catch Blocks Example

        try
        {
            int[] array = { 1, 2, 3 };
            int result = 10 / (array[3] - 3); // Causes DivideByZeroException
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("Index out of range: " + ex.Message);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Divide by zero error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("General Exception: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed for Multiple Catch Blocks Example.");
        }

        #endregion

        #region Nested Try-Catch Blocks

        try
        {
            try
            {
                string[] fruits = { "Apple", "Banana" };
                Console.WriteLine(fruits[3]); // Causes IndexOutOfRangeException
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Inner Exception: " + ex.Message);
                throw new Exception("An error occurred while processing fruits.", ex);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Outer Exception: " + ex.Message);
            Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed for Nested Try-Catch Blocks.");
        }

        #endregion

        #region Exception Filters Example

        try
        {
            int age = -5;
            if (age < 0)
            {
                throw new ArgumentOutOfRangeException("Age cannot be negative.");
            }
        }
        catch (ArgumentOutOfRangeException ex) when (ex.Message.Contains("negative"))
        {
            Console.WriteLine("Filtered Exception: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Finally block executed for Exception Filters Example.");
        }

        #endregion
    }
}
