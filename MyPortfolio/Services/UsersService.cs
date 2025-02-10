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
        public List<ContactMeInfo> GetEmployees()
        {
            List<ContactMeInfo> employees = new List<ContactMeInfo>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Employees", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ContactMeInfo employee = new ContactMeInfo
                    {
                        UserId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2),
                        ContactNumber = reader.GetString(3),
                        Message = reader.GetString(4)
                    };
                    employees.Add(employee);
                }
            }

            return employees;
        }
    }
}
