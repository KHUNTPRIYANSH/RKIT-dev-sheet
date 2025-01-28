namespace Logging.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public bool isActive { get; set; }

        public static List<Employee> employees = new List<Employee>()
        {
            new Employee { id = 1, name = "Priyansh", city = "Ahmedabad", isActive = true },
            new Employee { id = 2, name = "Keyur", city = "Surat", isActive = true },
            new Employee { id = 3, name = "Maulik", city = "Vadodara", isActive = false },
            new Employee { id = 4, name = "Parth", city = "Rajkot", isActive = true },
            new Employee { id = 5, name = "Yash", city = "Gandhinagar", isActive = false }
        };
    }
}
