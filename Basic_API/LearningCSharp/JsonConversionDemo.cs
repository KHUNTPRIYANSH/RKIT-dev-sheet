using System;
using Newtonsoft.Json;

namespace LearningCSharp
{
    #region Person Class

    /// <summary>
    /// Represents a Person with Name and Age properties.
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    #endregion

    #region JSON Conversion Demo Class

    /// <summary>
    /// Class to demonstrate the conversion of an object to JSON and vice versa using Newtonsoft.Json.
    /// </summary>
    public class JsonConversionDemo
    {
        /// <summary>
        /// Demonstrates converting an object to JSON and JSON back to an object.
        /// </summary>
        public static void RunJsonConversionDemo()
        {
            #region Object to JSON Conversion

            // Create a new Person object
            Person person = new Person
            {
                Name = "John Doe",
                Age = 30
            };

            // Convert the Person object to a JSON string
            string jsonString = JsonConvert.SerializeObject(person);

            // Display the JSON string
            Console.WriteLine("Object to JSON:");
            Console.WriteLine(jsonString);

            #endregion

            #region JSON to Object Conversion

            // JSON string representing a Person object
            string json = "{\"Name\":\"Jane Smith\",\"Age\":25}";

            // Convert the JSON string back to a Person object
            Person deserializedPerson = JsonConvert.DeserializeObject<Person>(json);

            // Display the deserialized object
            Console.WriteLine("\nJSON to Object:");
            Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");

            #endregion
        }
    }

    #endregion
}
