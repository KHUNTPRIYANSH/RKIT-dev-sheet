using System;
using System.Collections.Generic;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Configuration;
using Phase_3_Final_Demo.Models;
using System.Data.SqlClient;
using System.Runtime.Caching;
namespace CRUD_Web_Api.Controllers
{


    #region Secure Data Endpoint

    /// <summary>
    /// Retrieves secure data for an authenticated user.
    /// </summary>
    /// <returns>An action result containing a secure message.</returns>
    // [HttpGet]
    // [Authorize]
    // [Route("api/secure-data")]
    // public IHttpActionResult GetSecureData()
    // {
    //     // Get the username from the authenticated user's identity
    //     var username = User.Identity.Name;
    // 
    //     // Return a secure message
    //     return Ok(new { Message = $"Hello {username}, this is a secure message!" });
    // }

    #endregion
    public class EmployeesController : ApiController
    {
        #region Connection String
        // Connection string to Employees [MySQL database]
        //private string connectionString = "Server=localhost;Database=BasicWebApi;User ID=root;Password=PRIYANS0605K;SslMode=None;";
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        #endregion

        // MemoryCache instance for caching
        private ObjectCache cache = MemoryCache.Default;

        #region GET Methods

        /// <summary>
        /// Retrieves all employees from the database or cache.
        /// </summary>
        /// <returns>List of Employee objects</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Editor,User")]
        [Route("api/employees")]
        public IHttpActionResult Get()
        {
            // Check if the employees data is cached
            var employees = cache["AllEmployees"] as List<Employee>;

            if (employees == null)
            {
                // Data is not in cache, fetch from database
                employees = new List<Employee>();
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT * FROM Employees", conn);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
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

                // Cache the employees data for 5 minutes
                CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
                cache.Set("AllEmployees", employees, policy);
            }

            return employees.Count == 0 ? (IHttpActionResult)NotFound() : Ok(employees);
        }

        /// <summary>
        /// Retrieves a specific employee by ID from the database or cache.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee object</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Editor,User")]
        [Route("api/employees/{id}")]
        public IHttpActionResult Get(int id)
        {
            // Check if the employee data is cached
            var employee = cache[$"Employee_{id}"] as Employee;

            if (employee == null)
            {
                // Data is not in cache, fetch from database
                employee = null;
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT * FROM Employees WHERE ID = @ID", conn);
                    cmd.Parameters.AddWithValue("@ID", id);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
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

                // Cache the employee data for 5 minutes
                if (employee != null)
                {
                    CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) };
                    cache.Set($"Employee_{id}", employee, policy);
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
        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        [Route("api/employees")]
        public IHttpActionResult Post(Employee employee)
        {
            if (employee == null) return BadRequest("Invalid employee data");

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO Employees (Name, City, IsActive) VALUES (@Name, @City, @IsActive)", conn);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                cmd.ExecuteNonQuery();
            }

            return CreatedAtRoute("DefaultApi", new { id = employee.ID }, employee);
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
        [HttpPut]
        [Authorize(Roles = "Admin,Editor")]
        [Route("api/employees/{id}")]
        public IHttpActionResult Put(int id, Employee employee)
        {
            if (employee == null) return BadRequest("Invalid employee data");

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE Employees SET Name = @Name, City = @City, IsActive = @IsActive WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
                if (cmd.ExecuteNonQuery() == 0) return NotFound();
            }

            return Ok("Employee updated successfully!");
        }

        #endregion

        #region DELETE Method

        /// <summary>
        /// Deletes an employee from the database by ID.
        /// </summary>
        /// <param name="id">Employee ID to delete</param>
        /// <returns>HTTP response with a success message</returns>
        // DELETE: api/Employees/3
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("api/employees/{id}")]
        public IHttpActionResult Delete(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM Employees WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                if (cmd.ExecuteNonQuery() == 0) return NotFound();
            }

            return Ok("Employee deleted successfully!");
        }

        #endregion
    }
}
