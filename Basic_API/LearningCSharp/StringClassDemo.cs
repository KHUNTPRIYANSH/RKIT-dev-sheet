namespace LearningCSharp;
/// <summary>
/// Demonstrates various string manipulation techniques in C#,
/// including concatenation, interpolation, comparison, case conversion, and more.
/// </summary>
public class StringClassDemo
{
    /// <summary>
    /// Executes a series of string manipulation examples, showcasing string operations like 
    /// concatenation, interpolation, comparison, case conversion, substring extraction, 
    /// and more.
    /// </summary>
    public static void RunStringClassDemo()
    {
        // Example 1: Concatenating strings
        string firstName = "Amit";
        string lastName = "Sharma";
        // Concatenate first and last name with a space in between
        string fullName = string.Concat(firstName, " ", lastName);
        Console.WriteLine("Full Name: " + fullName);

        // Example 2: String Interpolation
        // Interpolate strings to include variables in the output
        string greeting = $"Hello, {firstName} {lastName}!";
        Console.WriteLine(greeting);

        // Example 3: String Comparison
        string str1 = "mango";
        string str2 = "Mango";
        // Compare strings ignoring case sensitivity
        bool areEqual = string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        Console.WriteLine($"Are strings equal (ignoring case)? {areEqual}");

        // Example 4: Changing case (ToUpper / ToLower)
        string lowerCaseString = "namaste india";
        // Convert the string to uppercase
        string upperCaseString = lowerCaseString.ToUpper();
        Console.WriteLine("Uppercase: " + upperCaseString);

        // Example 5: Substring Extraction
        string sentence = "Welcome to the Taj Mahal!";
        // Extract a substring starting from index 11 with length 7 (i.e., "the Taj")
        string subString = sentence.Substring(11, 7);
        Console.WriteLine("Substring: " + subString);

        // Example 6: Checking if a string contains a substring
        // Check if the string contains the word "Taj"
        bool containsTaj = sentence.Contains("Taj");
        Console.WriteLine($"Contains 'Taj': {containsTaj}");

        // Example 7: Trimming whitespace
        string messyString = "   Welcome to India!   ";
        // Trim leading and trailing whitespace from the string
        string trimmedString = messyString.Trim();
        Console.WriteLine("Trimmed: " + trimmedString);

        // Example 8: String Replacement
        string sentenceWithReplacement = sentence.Replace("Taj Mahal", "Red Fort");
        // Replace occurrences of "Taj Mahal" with "Red Fort" in the string
        Console.WriteLine("Replaced: " + sentenceWithReplacement);

        // Example 9: Splitting a string
        string csv = "Raj,Neha,Anjali";
        // Split the string by commas into an array
        string[] names = csv.Split(',');
        Console.WriteLine("Names:");
        // Loop through the array and print each name
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }

        // Example 10: String Length
        // Output the length of the sentence string
        Console.WriteLine("Length of the sentence: " + sentence.Length);
    }
}
