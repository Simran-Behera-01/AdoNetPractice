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

        public bool AddStudent(Student student)
        {
            int rowsAffected = 0;
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                const string query = @"INSERT INTO Students(
                                FirstName,
                                LastName,
                                Age,
                                Email,
                                PhoneNumber,
                                Percentage,
                                DepartmentId) 
                            VALUES
                                (@FirstName,
                                @LastName,
                                @Age,
                                @Email,
                                @PhoneNumber,
                                @Percentage, 
                                @DepartmentId)";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Age", student.Age);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@Percentage", student.Percentage);
                command.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return rowsAffected != 0;
        }
    }
}
