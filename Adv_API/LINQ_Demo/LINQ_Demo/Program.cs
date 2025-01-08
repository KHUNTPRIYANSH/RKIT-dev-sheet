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
    public class Order
    {
        public int OrderID { get; set; }
        public int PersonId { get; set; }
        public int TotalAmount { get; set; }
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

        public List<Order> OrderList = new List<Order>()
        {
            new Order { OrderID = 101, PersonId = 1, TotalAmount = 500 },
            new Order { OrderID = 102, PersonId = 2, TotalAmount = 700 },
            new Order { OrderID = 103, PersonId = 3, TotalAmount = 300 },
            new Order { OrderID = 104, PersonId = 4, TotalAmount = 800 }
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

            // Run set methods demo
            SetMethods(dataSource);

            // Run join methods demo
            JoinMethods(dataSource);

            // Run aggregate methods demo
            AggregateMethods(dataSource);

            // Run Group method demo
            GroupMethods(dataSource);

            // Run conversion methods demo
            ConversionMethods(dataSource);
        }

        // Filter Methods Demo
        public static void FilterMethods(DataSource dataSource)
        {
            Console.WriteLine("\n##### Filter Methods ####");
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

            Console.WriteLine("\n##### Projection  Methods ####");
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

            Console.WriteLine("\n##### Partition  Methods ####");
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

            Console.WriteLine("\n##### Sorting  Methods ####");
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

            Console.WriteLine("\n##### Quantifier  Methods ####");
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
        /// <summary>
        /// Demonstrates set operations like Distinct, Union, Intersect, and Except.
        /// </summary>
        public static void SetMethods(DataSource dataSource)
        {

            Console.WriteLine("\n##### Set  Methods ####");
            List<int> set1 = new List<int> { 1, 2, 3 };
            List<int> set2 = new List<int> { 3, 4, 5 };

            var distinctSet1 = set1.Distinct().ToList();
            Console.WriteLine("Distinct Set1: " + string.Join(", ", distinctSet1));

            var union = set1.Union(set2).ToList();
            Console.WriteLine("Union: " + string.Join(", ", union));

            var intersect = set1.Intersect(set2).ToList();
            Console.WriteLine("Intersect: " + string.Join(", ", intersect));

            var except = set1.Except(set2).ToList();
            Console.WriteLine("Except: " + string.Join(", ", except));

            var concat = set1.Concat(set2).ToList();
            Console.WriteLine("Concat: " + string.Join(", ", concat));
        }

        /// <summary>
        /// Demonstrates join operations like Join and GroupJoin.
        /// </summary>
        public static void JoinMethods(DataSource dataSource)
        {

            Console.WriteLine("\n##### Join  Methods ####");
            var personOrders = dataSource.PersonList.Join(dataSource.OrderList,
                person => person.PersonID,
                order => order.PersonId,
                (person, order) => new { person.Name, order.TotalAmount });

            foreach (var item in personOrders)
            {
                Console.WriteLine($"Person: {item.Name}, Order Amount: {item.TotalAmount}");
            }

            var groupedOrders = dataSource.PersonList.GroupJoin(dataSource.OrderList,
                person => person.PersonID,
                order => order.PersonId,
                (person, orders) => new { person.Name, Orders = orders.Select(o => o.TotalAmount) });

            foreach (var item in groupedOrders)
            {
                Console.WriteLine($"Person: {item.Name}, Orders: {string.Join(", ", item.Orders)}");
            }
        }

        /// <summary>
        /// Demonstrates aggregate methods like Sum, Average, Min, Max, and Aggregate.
        /// </summary>
        public static void AggregateMethods(DataSource dataSource)
        {

            Console.WriteLine("\n##### Aggregate  Methods ####");
            int totalAmount = dataSource.OrderList.Sum(o => o.TotalAmount);
            Console.WriteLine("Total Order Amount: " + totalAmount);

            double averageAmount = dataSource.OrderList.Average(o => o.TotalAmount);
            Console.WriteLine("Average Order Amount: " + averageAmount);

            int minAmount = dataSource.OrderList.Min(o => o.TotalAmount);
            Console.WriteLine("Minimum Order Amount: " + minAmount);

            int maxAmount = dataSource.OrderList.Max(o => o.TotalAmount);
            Console.WriteLine("Maximum Order Amount: " + maxAmount);

            int totalSum = dataSource.OrderList.Select(o => o.TotalAmount).Aggregate((sum, next) => sum + next);
            Console.WriteLine("Aggregate Total Amount: " + totalSum);
        }

        /// <summary>
        /// Demonstrates grouping methods like GroupBy.
        /// </summary>
        public static void GroupMethods(DataSource dataSource)
        {

            Console.WriteLine("\n##### Group  Methods ####");
            var groupedByAge = dataSource.PersonList.GroupBy(p => p.Age);

            foreach (var group in groupedByAge)
            {
                Console.WriteLine($"Age: {group.Key}, People: {string.Join(", ", group.Select(p => p.Name))}");
            }
        }

        /// <summary>
        /// Demonstrates conversion methods like ToList, ToArray, ToDictionary, and OfType.
        /// </summary>
        public static void ConversionMethods(DataSource dataSource)
        {

            Console.WriteLine("\n##### Conversion  Methods ####");
            var namesList = dataSource.PersonList.Select(p => p.Name).ToList();
            Console.WriteLine("Names List: " + string.Join(", ", namesList));

            var namesArray = dataSource.PersonList.Select(p => p.Name).ToArray();
            Console.WriteLine("Names Array: " + string.Join(", ", namesArray));

            var personDictionary = dataSource.PersonList.ToDictionary(p => p.PersonID, p => p.Name);
            foreach (var kvp in personDictionary)
            {
                Console.WriteLine($"PersonID: {kvp.Key}, Name: {kvp.Value}");
            }

            List<object> mixedList = new List<object> { 1, "hello", 2.5, "world" };
            var strings = mixedList.OfType<string>();
            Console.WriteLine("Strings: " + string.Join(", ", strings));
        }
    }
}
