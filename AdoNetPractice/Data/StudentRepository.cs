using AdoNetPractice.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetPractice.Data
{
    internal class StudentRepository
    {
        private readonly DbConnectionFactory _connectionFactory = new();

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                using var command = new SqlCommand("SELECT * FROM Students", connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(
                        new Student
                        {
                            Id = (int)reader["StudentId"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Age = (int)reader["Age"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"],
                            Percentage = Convert.ToDouble(reader["Percentage"]),
                            DepartmentId = (int)reader["DepartmentId"]
                        });
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return students;
        }

        public Student? GetStudentById(int id)
        {
            Student? student = null;
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                using var command = new SqlCommand("SELECT * FROM Students WHERE StudentId = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    student = new Student
                    {
                        Id = (int)reader["StudentId"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Percentage = Convert.ToDouble(reader["Percentage"]),
                        DepartmentId = (int)reader["DepartmentId"]
                    };
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return student;
        }


    }
}
