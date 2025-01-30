using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TPA_Server.Models;

namespace TPA_Server.Helpers
{
    public class DbHelper
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        public void SaveAuthModelToDatabase(string username, string password, string role, List<string> dependencies, int tokenExpiryInMinutes)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string dependenciesText = string.Join(",", dependencies); // Convert list to comma-separated string

                string query = @"INSERT INTO auth_table (username, password, role, Dependencies, TokenExpiryInMinutes) 
                                 VALUES (@username, @password, @role, @dependencies, @tokenExpiryInMinutes)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@dependencies", dependenciesText);
                    cmd.Parameters.AddWithValue("@tokenExpiryInMinutes", tokenExpiryInMinutes);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AuthModel> GetAllUsers()
        {
            var users = new List<AuthModel>();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM auth_table";

                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new AuthModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                            Role = reader["role"].ToString(),
                            Dependencies = reader["Dependencies"].ToString().Split(',').ToList(),
                            TokenExpiryInMinutes = Convert.ToInt32(reader["TokenExpiryInMinutes"])
                        });
                    }
                }
            }
            return users;
        }

        public AuthModel GetUserById(int id)
        {
            AuthModel user = null;

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM auth_table WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new AuthModel
                            {
                                Id = reader.GetInt32("id"),
                                Username = reader.GetString("username"),
                                Password = reader.GetString("password"), // This should be handled securely
                                Role = reader.GetString("role"),
                                Dependencies = reader.GetString("Dependencies").Split(',').ToList(),
                                TokenExpiryInMinutes = reader.GetInt32("TokenExpiryInMinutes")
                            };
                        }
                    }
                }
            }
            return user;
        }

        public bool UpdateUser(int id, string username, string password, string role, List<string> dependencies, int tokenExpiryInMinutes)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE auth_table 
                         SET username = @username, password = @password, role = @role, 
                             Dependencies = @dependencies, TokenExpiryInMinutes = @tokenExpiryInMinutes
                         WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@dependencies", string.Join(",", dependencies));
                    cmd.Parameters.AddWithValue("@tokenExpiryInMinutes", tokenExpiryInMinutes);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteUser(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM auth_table WHERE id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
