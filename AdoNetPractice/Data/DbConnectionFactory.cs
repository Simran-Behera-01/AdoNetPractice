using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdoNetPractice.Data
{
    internal class DbConnectionFactory
    {
        const string connectionString =
            "Server=YOUR_SQL_SERVER;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";
        public DbConnectionFactory() { }
        public void ConnectToDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    Console.WriteLine("Connection to the database established successfully.");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while connecting to the database: {ex.Message}");
            }
        }
    }
}
