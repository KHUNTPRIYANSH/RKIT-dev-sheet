using System.Data;
namespace LearningCSharp
{
    internal class DataTables
    {
        public static void RunDataTablesDemo()
        {
            try
            {
                // creating the object of datatable using DataTable class from System.Data namespace
                DataTable employees = new DataTable("employees");

                //employees.Columns.Add("ID", typeof(int));
                DataColumn id = new DataColumn("ID")
                {
                    DataType = typeof(int),
                    Unique = true,
                    AllowDBNull = false,  // if false => not null ; default value is true
                    AutoIncrement = true,
                    AutoIncrementSeed = 0, // initial value
                    AutoIncrementStep = 5, // increment by 
                };

                DataColumn name = new DataColumn("NAME");
                name.DataType = typeof(string);
                name.AllowDBNull = false;
                name.MaxLength = 100;
                name.DefaultValue = "Anonymous";

                DataColumn dept_id = new DataColumn("DID");
                id.DataType = typeof(int);
                id.AllowDBNull = false;

                // adding column to datatable
                employees.Columns.Add(id);
                employees.Columns.Add(name);
                employees.Columns.Add(dept_id);


                employees.Rows.Add(null, "Mr. abcde", 7);
                employees.Rows.Add(null, "Mr. pqrst", 3);
                employees.Rows.Add(null, "Mr. uvwxyz", 2);
                employees.Rows.Add(null, "Mr. mnopq", 1);

                // other way

                DataRow dataRow1 = employees.NewRow();
                //dataRow1["ID"] = 4; 
                dataRow1["ID"] = 3;
                dataRow1["DID"] = 7;


                // adding row to employee table
                employees.Rows.Add(dataRow1);

                // primary key 
                employees.PrimaryKey = new DataColumn[] { id };

                // printing all the rows in table
                DisplayDataTable(employees);

                Console.WriteLine("\nFilter the table data by the department");
                DataRow[] filteredRows = employees.Select("DID = 7");
                foreach (DataRow row in filteredRows)
                {
                    Console.WriteLine("ID: " + row["ID"] + "\tName: " + row["NAME"] + "\tDept_Id: " + row["DID"]);
                }

                // Accessing specific data from a specific row (Row 2, Name column)
                Console.WriteLine("\nAccessing specific data:");
                Console.WriteLine($"Employee Name (Row 2): {employees.Rows[1]["NAME"]}");
                Console.WriteLine();


                // another table belong to dataset called compnay
                DataTable departments = new DataTable("departments");
                DataColumn did = new DataColumn("DID")
                {
                    DataType = typeof(int),
                };
                DataColumn dept_Name = new DataColumn("DEPT_NAME")
                {
                    DataType = typeof(string),
                    MaxLength = 50,
                    AllowDBNull = true,
                };
                departments.Columns.Add(did);
                departments.Columns.Add(dept_Name);
                departments.Rows.Add(1, "HR.");
                departments.Rows.Add(2, "QA.");
                departments.Rows.Add(3, "Design");
                departments.Rows.Add(7, "Dev");

                // Demonstrate DataSet containing multiple tables
                DataSet dataSet = new DataSet("Company");
                dataSet.Tables.Add(employees);
                dataSet.Tables.Add(departments);

                // display all tables in dataset compnay
                foreach (DataTable table in dataSet.Tables)
                {
                    Console.WriteLine("Table : " + table);
                    DisplayDataTable(table);
                    Console.WriteLine();
                }

                // Use Clone and Copy methods of DataTable
                DataTable clonedTable = employees.Clone(); // Structure only
                Console.WriteLine("\nCloned Table Structure (No Rows):");
                DisplayDataTable(clonedTable);

                DataTable copiedTable = employees.Copy(); // Structure and data
                Console.WriteLine("\nCopied Table Structure and Data:");
                DisplayDataTable(copiedTable);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        private static void DisplayDataTable(DataTable table)
        {
            Console.WriteLine("Columns:");
            foreach (DataColumn column in table.Columns)
            {
                Console.Write(column.ColumnName + "\t");
            }
            Console.WriteLine("\nRows:");
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
