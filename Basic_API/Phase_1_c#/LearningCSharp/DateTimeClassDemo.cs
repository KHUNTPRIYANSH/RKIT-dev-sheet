namespace LearningCSharp;

/// <summary>
/// Demonstrates various functionalities of the DateTime class in C# with practical examples.
/// </summary>
public class DateTimeClassDemo
{
    /// <summary>
    /// Executes the DateTime operations demonstration.
    /// </summary>
    public static void RunDateTimeClassDemo()
    {
        // Get the current date and time
        DateTime currentDateTime = DateTime.Now;
        Console.WriteLine("Current Date and Time: " + currentDateTime);

        // Get today's date (without the time)
        DateTime today = DateTime.Today;
        Console.WriteLine("Today's Date: " + today);

        // Get only the current time (without the date)
        TimeSpan currentTime = currentDateTime.TimeOfDay;
        Console.WriteLine("Current Time: " + currentTime);

        // Add 6 months to the current date
        DateTime sixMonthsLater = currentDateTime.AddMonths(6);
        Console.WriteLine("6 Months Later: " + sixMonthsLater.ToString("dd-MM-yyyy"));

        // Subtract 10 days from today's date
        DateTime tenDaysAgo = today.AddDays(-10);
        Console.WriteLine("10 Days Ago: " + tenDaysAgo.ToString("dd-MM-yyyy"));

        // Format the current date as dd/MM/yyyy
        string formattedDate = currentDateTime.ToString("dd/MM/yyyy");
        Console.WriteLine("Formatted Date: " + formattedDate);

        // Parse a string to a DateTime object
        string independenceDay = "1947-08-15";
        DateTime parsedDate = DateTime.Parse(independenceDay);
        Console.WriteLine("Parsed Date (Independence Day): " + parsedDate.ToString("dd-MM-yyyy"));

        // Get the day of the week for today's date
        DayOfWeek dayOfWeek = today.DayOfWeek;
        Console.WriteLine("Today is: " + dayOfWeek);


        // Compare today's date with a festival date
        DateTime diwaliDate = new DateTime(2024, 11, 12);
        int comparisonResult = DateTime.Compare(today, diwaliDate);


        if (comparisonResult < 0)
        {
            Console.WriteLine("Today's date is before Diwali 2024.");
        }
        else if (comparisonResult == 0)
        {
            Console.WriteLine("Today is Diwali 2024!");
        }
        else
        {
            Console.WriteLine("Today's date is after Diwali 2024.");
        }
        Console.ReadKey();

        
       
    }
}
