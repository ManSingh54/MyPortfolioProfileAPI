using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyPortfolio.Models;
using System.Data;

namespace MyPortfolio.Services
{
    public class UsersService
    {
        private readonly string _connectionString;

        public UsersService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public List<ContactMeInfo> GetUsers()
        {
            List<ContactMeInfo> employees = new List<ContactMeInfo>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM ContactMe", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ContactMeInfo employee = new ContactMeInfo
                    {
                        //UserId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                       // ContactNumber = reader.GetString(3).ToString(),
                        Message = reader.GetString(4)
                    };
                    employees.Add(employee);
                }
            }

            return employees;
        }

        public bool saveUserDetails(ContactMeInfo contactInfo)
        {
            // Use the connection string for your database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection to the database
                connection.Open();

                // SQL command to insert the contact details into the ContactMe table
                string query = "INSERT INTO ContactMe (Name, Email, PhoneNumber, Message) VALUES (@Name, @Email, @PhoneNumber, @Message)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@Name", contactInfo.Name);
                    command.Parameters.AddWithValue("@Email", contactInfo.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", contactInfo.ContactNumber);  // Ensure `Phone` is part of `ContactMeInfo` model
                    command.Parameters.AddWithValue("@Message", contactInfo.Message);

                    try
                    {
                        // Execute the command to insert the data
                        int rowsAffected = command.ExecuteNonQuery();

                        // If rows were affected, return true indicating the operation was successful
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (you can use a logging framework like Serilog, NLog, or log directly)
                        Console.WriteLine("Error saving data: " + ex.Message);
                        return false;
                    }
                }
            }
        }

    }
}
