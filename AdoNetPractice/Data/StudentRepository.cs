using AdoNetPractice.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
                return students;
            }
            catch (SqlException ex)
            {
                throw;
            }
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
                return student;
            }
            catch (SqlException ex)
            {
                throw;
            }
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
                return rowsAffected != 0;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool UpdateStudent(Student student)
        {
            int rowsAffected = 0;
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                const string query = @"UPDATE Students
                                       SET FirstName = @FirstName,
                                           LastName = @LastName,
                                           Age =  @Age,
                                           Email = @Email,
                                           PhoneNumber = @PhoneNumber,
                                           Percentage = @Percentage,
                                           DepartmentId = @DepartmentId 
                                       WHERE StudentId = @StudentId";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Age", student.Age);
                command.Parameters.AddWithValue("@Email", student.Email);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@Percentage", student.Percentage);
                command.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);
                command.Parameters.AddWithValue("@StudentId", student.Id);
                rowsAffected = command.ExecuteNonQuery();
                return rowsAffected != 0;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool DeleteStudent(int id)
        {
            int rowsAffected = 0;
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                const string query = @"DELETE FROM Students
                                       WHERE StudentId = @StudentId";
                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", id);
                rowsAffected = command.ExecuteNonQuery();
                return rowsAffected != 0;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool EnrollStudent(Student student)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                using var transaction = connection.BeginTransaction();
                try
                {
                    const string query1 = @"INSERT INTO Students(
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
                                @DepartmentId);
                            SELECT SCOPE_IDENTITY();";
                    using var command1 = new SqlCommand(query1, connection, transaction);
                    command1.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command1.Parameters.AddWithValue("@LastName", student.LastName);
                    command1.Parameters.AddWithValue("@Age", student.Age);
                    command1.Parameters.AddWithValue("@Email", student.Email);
                    command1.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    command1.Parameters.AddWithValue("@Percentage", student.Percentage);
                    command1.Parameters.AddWithValue("@DepartmentId", student.DepartmentId);
                    int studentId = Convert.ToInt32(command1.ExecuteScalar());

                    const string query2 = @"INSERT INTO StudentAudit(
	                                       	StudentId,
	                                    	ActionType,
	                                    	NewPercentage
	                                    )
	                                    VALUES
                                        ( @StudentId,
                                          @Action,
                                          @Percentage);";
                    using var command2 = new SqlCommand(query2, connection, transaction);
                    command2.Parameters.AddWithValue("@StudentId", studentId);
                    command2.Parameters.AddWithValue("@Action", "INSERT");
                    command2.Parameters.AddWithValue("@Percentage", student.Percentage);
                    command2.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex) 
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        
        public List<Student> GetStudentsByDepartment(int departmentId)
        {
            List<Student> students = [];
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                var storedProcedure = @"GetStudentsByDepartment";
                using var command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = departmentId;
                using var reader = command.ExecuteReader();
                while(reader.Read())
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
                return students;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public (int,int,double) GetDepartmentStatistics(int departmentId)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                connection.Open();
                using var command = new SqlCommand("GetDepartmentStatistics", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = departmentId;
                var studentCountOutput = new SqlParameter("@StudentCount", SqlDbType.Int);
                studentCountOutput.Direction = ParameterDirection.Output;
                command.Parameters.Add(studentCountOutput);
                var studentAverageOutput = new SqlParameter("@AveragePercentage", SqlDbType.Decimal);
                studentAverageOutput.Direction = ParameterDirection.Output;
                command.Parameters.Add(studentAverageOutput);
                var departmentExists = new SqlParameter();
                departmentExists.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(departmentExists);
                command.ExecuteNonQuery();
                return (Convert.ToInt32(departmentExists.Value), studentCountOutput.Value == DBNull.Value ? 0 : Convert.ToInt32(studentCountOutput.Value), studentAverageOutput.Value==DBNull.Value ? 0 :  Convert.ToDouble(studentAverageOutput.Value));
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public DataSet DataSets()
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                DataSet dataSet = new DataSet();
                var query = @"SELECT * FROM Departments;
                             SELECT * FROM Courses;";
                using var adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dataSet);
                dataSet.Tables[0].TableName = "Departments";
                dataSet.Tables[1].TableName = "Courses";
                return dataSet;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
