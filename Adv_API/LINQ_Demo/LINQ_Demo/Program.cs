using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Demo
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class DataSource
    {
        // Sample list of people
        public List<Person> PersonList = new List<Person>()
        {
            new Person { PersonID = 1, Name = "Priyansh", Age = 25 },
            new Person { PersonID = 2, Name = "Keyur", Age = 30 },
            new Person { PersonID = 3, Name = "Parth", Age = 35 },
            new Person { PersonID = 4, Name = "Yash", Age = 40 },
            new Person { PersonID = 5, Name = "Maulik", Age = 50 }
        };
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Create instance of DataSource
            DataSource dataSource = new DataSource();

            // Run the filter demo method
            FilterMethods(dataSource);

            // Run projection methods demo
            ProjectionMethods(dataSource);

            // Run partitioning methods demo
            PartitionMethods(dataSource);

            // Run sorting methods demo
            SortingMethods(dataSource);

            // Run quantifier methods demo
            QuantifierMethods(dataSource);
        }

        // Filter Methods Demo
        public static void FilterMethods(DataSource dataSource)
        {
            // Where() - Filters adults aged 30 or above
            List<Person> adults = dataSource.PersonList.Where(p => p.Age >= 30).ToList();
            Console.WriteLine("Adults: " + string.Join(", ", adults.Select(p => p.Name)));

            // First() - Gets the first person aged 30 or above
            Person firstAdult = dataSource.PersonList.First(p => p.Age >= 30);
            Console.WriteLine("First adult: " + firstAdult.Name);

            // FirstOrDefault() - Gets the first person aged above 40, or null if none exist
            Person? firstOrDefault = dataSource.PersonList.FirstOrDefault(p => p.Age > 40);
            Console.WriteLine("First or Default (no match): " + (firstOrDefault?.Name ?? "None"));

            // Single() - Gets the only person with PersonID = 1
            try
            {
                Person singlePerson = dataSource.PersonList.Single(p => p.PersonID == 1);
                Console.WriteLine("Single person: " + singlePerson.Name);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Single person exception: No match or  Multiple matches");
            }

            // SingleOrDefault() - Returns null if no person with PersonID = 5
            try
            {

            Person? singleOrDefaultPerson = dataSource.PersonList.SingleOrDefault(p => p.PersonID == 5);
            Console.WriteLine("Single or Default (no match): " + (singleOrDefaultPerson?.Name ?? "None"));
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("SingleOrDefault person exception: Multiple matches");
            }

            // Last() - Gets the last person aged 30 or above
            Person lastPerson = dataSource.PersonList.Last(p => p.Age >= 30);
            Console.WriteLine("Last adult: " + lastPerson.Name);

            // LastOrDefault() - Returns null if no person aged above 40
            Person? lastOrDefaultPerson = dataSource.PersonList.LastOrDefault(p => p.Age > 40);
            Console.WriteLine("Last or Default (no match): " + (lastOrDefaultPerson?.Name ?? "None"));
        }
        /// <summary>
        /// Demonstrates projection methods like Select and SelectMany.
        /// </summary>
        public static void ProjectionMethods(DataSource dataSource)
        {
            // Select - Projects only the names of people
            var names = dataSource.PersonList.Select(p => p.Name);
            Console.WriteLine("Names: " + string.Join(", ", names));

            // SelectMany - merge nested collections
            List<List<int>> numberGroups = new List<List<int>>
            {
                new List<int> { 1, 2 },
                new List<int> { 3, 4 }
            };
            // From :
            // [
            //      [1,2], 
            //      [3,4]
            // ]
            // To: [1,2,3,4]

            // Merging the nested lists into a single collection
            var oneDNumbers = numberGroups.SelectMany(group => group);
            Console.WriteLine("Flattened Numbers: " + string.Join(", ", oneDNumbers));
        }

        /// <summary>
        /// Demonstrates partitioning methods like Take, Skip, and their variants.
        /// </summary>
        public static void PartitionMethods(DataSource dataSource)
        {
            // TakeWhile() - Takes elements while the condition is true
            IEnumerable<Person> PersonListStartingWithA = dataSource.PersonList.TakeWhile(p => p.Name.StartsWith("A"));
            Console.WriteLine("TakeWhile: " + string.Join(", ", PersonListStartingWithA.Select(p => p.Name)));

            // SkipWhile() - Skips elements while the condition is true
            IEnumerable<Person> PersonListNotStartingWithA = dataSource.PersonList.SkipWhile(p => p.Name.StartsWith("A"));
            Console.WriteLine("SkipWhile: " + string.Join(", ", PersonListNotStartingWithA.Select(p => p.Name)));

            // Take() - Takes the first 3 elements
            IEnumerable<Person> first3People = dataSource.PersonList.Take(3);
            Console.WriteLine("First 3 people: " + string.Join(", ", first3People.Select(p => p.Name)));

            // Skip() - Skips the first 3 elements
            IEnumerable<Person> skipFirst3People = dataSource.PersonList.Skip(3);
            Console.WriteLine("Skip first 3 people: " + string.Join(", ", skipFirst3People.Select(p => p.Name)));

            // TakeLast() - Takes the last 2 elements
            IEnumerable<Person> last2People = dataSource.PersonList.TakeLast(2);
            Console.WriteLine("Last 2 people: " + string.Join(", ", last2People.Select(p => p.Name)));

            // SkipLast() - Skips the last 2 elements
            IEnumerable<Person> skipLast2People = dataSource.PersonList.SkipLast(2);
            Console.WriteLine("Skip last 2 people: " + string.Join(", ", skipLast2People.Select(p => p.Name)));
        }

        /// <summary>
        /// Demonstrates sorting methods like OrderBy, ThenBy, and their descending variants.
        /// </summary>
        public static void SortingMethods(DataSource dataSource)
        {
            // OrderBy() - Sorts by age in ascending order
            List<Person> sortedByAgeAsc = dataSource.PersonList.OrderBy(p => p.Age).ToList();
            Console.WriteLine("Sorted by Age Ascending: " + string.Join(", ", sortedByAgeAsc.Select(p => p.Name)));

            // OrderByDescending() - Sorts by age in descending order
            List<Person> sortedByAgeDesc = dataSource.PersonList.OrderByDescending(p => p.Age).ToList();
            Console.WriteLine("Sorted by Age Descending: " + string.Join(", ", sortedByAgeDesc.Select(p => p.Name)));

            // ThenBy() - Secondary sort by name in ascending order
            List<Person> sortedThenByName = dataSource.PersonList.OrderBy(p => p.Age).ThenBy(p => p.Name).ToList();
            Console.WriteLine("Sorted by Age then Name: " + string.Join(", ", sortedThenByName.Select(p => p.Name)));

            // ThenByDescending() - Secondary sort by name in descending order
            List<Person> sortedThenByDescName = dataSource.PersonList.OrderBy(p => p.Age).ThenByDescending(p => p.Name).ToList();
            Console.WriteLine("Sorted by Age then Name Descending: " + string.Join(", ", sortedThenByDescName.Select(p => p.Name)));
        }

        /// <summary>
        /// Demonstrates quantifier methods like Any, All, Contains, and Count.
        /// </summary>
        public static void QuantifierMethods(DataSource dataSource)
        {
            // Any() - Checks if any person is aged 30 or above
            bool anyAdults = dataSource.PersonList.Any(p => p.Age >= 30);
            Console.WriteLine("Any adults: " + anyAdults);

            // All() - Checks if all people are aged 30 or above
            bool allAdults = dataSource.PersonList.All(p => p.Age >= 30);
            Console.WriteLine("All adults: " + allAdults);

            // Contains() - Checks if a specific PersonID is in the list
            bool containsPersonID1 = dataSource.PersonList.Select(p => p.PersonID).Contains(1);
            Console.WriteLine("Contains PersonID 1: " + containsPersonID1);

            // Count() - Counts the total number of people
            int totalPeople = dataSource.PersonList.Count();
            Console.WriteLine("Total people: " + totalPeople);
        }

    }
}
