using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using DeleteIconPro.Models;

namespace DeleteIconPro.Data
{
    public class DataService
    {
        private readonly string _connectionString;

        // Constructor to get connection string from appsettings.json or other configuration files
        public DataService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("s"); // Example connection string key
        }

        public List<Person> GetAllPersons()
        {
            List<Person> persons = new List<Person>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id,Name,Age,City FROM Person";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                        // Open connection and execute the insert command
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            persons.Add(new Person
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Age = (int)reader["Age"],
                                City = reader["City"].ToString()
                            });
                        }
                        return persons;
                    
                    //catch (Exception ex)
                    //{
                    //    // Handle any exceptions here (e.g., logging, etc.)
                    //    Console.WriteLine($"Error: {ex.Message}");
                    //}
                }
            }
        }

        public void DeletePerson(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Person WHERE Id = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePerson(int id , string name)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Person SET Name = @Name WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
