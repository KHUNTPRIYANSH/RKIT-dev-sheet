using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using CRUD_MYSQL_DB_WEB_API.Models;
using System.Configuration;
using System.Web.Http.Cors;

namespace CRUD_Web_Api.Controllers
{
    // Option : 2 Add the [EnableCors] attribute to your controller or specific actions
    //[DisableCors]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeesController : ApiController
    {
        #region Connection String
        // Connection string to Employees [MySQL database]
        //private string connectionString = "Server=localhost;Database=BasicWebApi;User ID=root;Password=PRIYANS0605K;SslMode=None;";
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        #endregion

        #region GET Methods

        /// <summary>
        /// Retrieves all employees from the database.
        /// </summary>
        /// <returns>List of Employee objects</returns>
        // GET: api/Employees
        public IHttpActionResult Get()
        {
            List<Employee> employees = new List<Employee>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Employees", conn);
                var reader = cmd.ExecuteReader(); // reader reads each row from table row by row

                while (reader.Read()) // if we have row fetch it as obj & go to next row
                {
                    employees.Add(new Employee
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        City = reader["City"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }
            }

            if (employees.Count == 0)
            {
                return NotFound(); // Return 404 if no employees found
            }


            return Ok(employees); // Return 200 OK with list of employees
        }

        /// <summary>
        /// Retrieves a specific employee by ID.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee object</returns>
        // GET: api/Employees/3
        public IHttpActionResult Get(int id)
        {
            Employee employee = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Employees WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                var reader = cmd.ExecuteReader(); // reader reads each row as obj from table one by one

                if (reader.Read()) // if we found row then
                {
                    employee = new Employee
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        City = reader["City"].ToString(),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    };
                }
            }

            if (employee == null)
            {
                return NotFound(); // Return 404 if employee not found
            }

            return Ok(employee); // Return 200 OK with the employee object
        }

        #endregion

        #region POST Method

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="employee">Employee object containing the data to add</param>
        /// <returns>HTTP response with a success message</returns>
        // POST: api/Employees
        public IHttpActionResult Post(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data"); // Return 400 BadRequest if employee data is invalid
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Way 1 : string interpolation can make more readable code but 
                // it's not recommended for database operations.
                // Using string interpolation directly in SQL queries can lead to SQL injection attacks
                // if the input data is not properly sanitized.

                //MySqlCommand cmd = new MySqlCommand($"INSERT INTO Employees (Name, City, IsActive) VALUES ('{employee.Name}', '{employee.City}', {employee.IsActive})", conn);

                // Way 2 :
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Employees (Name, City, IsActive) VALUES (@Name, @City, @IsActive)", conn);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                cmd.ExecuteNonQuery();
            }

            return CreatedAtRoute("DefaultApi", new { id = employee.ID }, employee); // Return 201 Created with location of the new resource
        }

        #endregion

        #region PUT Method

        /// <summary>
        /// Updates an existing employee's details in the database.
        /// </summary>
        /// <param name="id">Employee ID to update</param>
        /// <param name="employee">Employee object with updated data</param>
        /// <returns>HTTP response with a success message</returns>
        // PUT: api/Employees/3
        
        public IHttpActionResult Put(int id, Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data"); // Return 400 BadRequest if employee data is invalid
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE Employees SET Name = @Name, City = @City, IsActive = @IsActive WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound(); // Return 404 if employee not found
                }
            }

            return Ok("Employee updated successfully!"); // Return 200 OK with success message
        }

        #endregion

        #region DELETE Method

        /// <summary>
        /// Deletes an employee from the database by ID.
        /// </summary>
        /// <param name="id">Employee ID to delete</param>
        /// <returns>HTTP response with a success message</returns>
        // DELETE: api/Employees/3
        public IHttpActionResult Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Employees WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound(); // Return 404 if employee not found
                }
            }

            return Ok("Employee deleted successfully!"); // Return 200 OK with success message
        }

        #endregion
    }
}
