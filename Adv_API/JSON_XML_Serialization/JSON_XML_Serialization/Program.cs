using System;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace JSON_XML_Serialization
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

    #region JSON and XML Conversion Demo Class

    /// <summary>
    /// Class to demonstrate the conversion of an object to JSON, XML, and vice versa.
    /// </summary>
    public class JsonXMLConversionDemo
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
                Name = "Priyansh Khunt",
                Age = 21
            };

            // Convert the Person object to a JSON string
            string jsonString = JsonConvert.SerializeObject(person);

            // Display the JSON string
            Console.WriteLine("Object to JSON:");
            Console.WriteLine(jsonString);

            #endregion

            #region JSON to Object Conversion

            // JSON string representing a Person object
            string json = "{\"Name\":\"Priyansh Khunt\",\"Age\":21}";

            // Convert the JSON string back to a Person object
            Person deserializedPerson = JsonConvert.DeserializeObject<Person>(json);

            // Display the deserialized object
            Console.WriteLine("\nJSON to Object:");
            Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");

            #endregion
        }

        /// <summary>
        /// Demonstrates converting an object to XML and XML back to an object.
        /// </summary>
        public static void RunXmlConversionDemo()
        {
            #region Object to XML Conversion

            // Create a new Person object
            Person person = new Person
            {
                Name = "Priyansh Khunt",
                Age = 21
            };

            // Convert the Person object to XML
            XElement xml = new XElement("Person",
                new XElement("Name", person.Name),
                new XElement("Age", person.Age));

            // Display the XML string
            Console.WriteLine("\nObject to XML:");
            Console.WriteLine(xml);

            #endregion

            #region XML to Object Conversion

            // XML string representing a Person object
            string xmlString = "<Person><Name>Priyansh Khunt</Name><Age>21</Age></Person>";

            // Parse the XML string
            XElement parsedXml = XElement.Parse(xmlString);

            // Convert the XML back to a Person object
            Person deserializedPerson = new Person
            {
                Name = parsedXml.Element("Name")?.Value,
                Age = int.Parse(parsedXml.Element("Age")?.Value ?? "0")
            };

            // Display the deserialized object
            Console.WriteLine("\nXML to Object:");
            Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");

            #endregion
        }
    }

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            JsonXMLConversionDemo.RunJsonConversionDemo();
            JsonXMLConversionDemo.RunXmlConversionDemo();
        }
    }
}
