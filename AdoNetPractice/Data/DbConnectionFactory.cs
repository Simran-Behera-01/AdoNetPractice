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
        private const string connectionString =
            "Server=YOUR_SQL_SERVER;Database=StudentManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
