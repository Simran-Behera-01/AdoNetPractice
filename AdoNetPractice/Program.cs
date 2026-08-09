using AdoNetPractice.Data;
using AdoNetPractice.Models;

namespace AdoNetPractice
{
    internal class Program
    {
        private static readonly StudentRepository _studentRepository = new StudentRepository();
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    int choice;
                    Console.WriteLine("Menu");
                    Console.WriteLine("1.Get All Students");
                    Console.WriteLine("2.Get student by Id");
                    Console.WriteLine("3.Insert student");
                    Console.WriteLine("4.Update student");
                    Console.WriteLine("5.Delete student");
                    Console.WriteLine("6.Enroll student");
                    Console.WriteLine("7.Get students by department");
                    Console.WriteLine("8.Get department statistics");
                    Console.WriteLine("9.Get All Departments");
                    Console.WriteLine("10.Get All Courses");
                    Console.WriteLine("11.Exit");

                    Console.WriteLine("Enter your choice");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            var students = _studentRepository.GetAllStudents();
                            if (students.Count == 0)
                            {
                                Console.WriteLine("No students found");
                                break;
                            }
                            foreach (var student in students)
                            {
                                Console.WriteLine($"Id: {student.Id}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}, Email: {student.Email}, Phone: {student.PhoneNumber}, Percentage: {student.Percentage}, DepartmentId: {student.DepartmentId}");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter student id");
                            int id = int.Parse(Console.ReadLine());
                            var studentById = _studentRepository.GetStudentById(id);
                            if (studentById != null)
                            {
                                Console.WriteLine($"Id: {studentById.Id}, Name: {studentById.FirstName} {studentById.LastName}, Age: {studentById.Age}, Email: {studentById.Email}, Phone: {studentById.PhoneNumber}, Percentage: {studentById.Percentage}, DepartmentId: {studentById.DepartmentId}");
                            }
                            else
                            {
                                Console.WriteLine("Student not found");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Enter student details to add:");
                            Console.WriteLine("First Name:");
                            string firstName = Console.ReadLine();
                            Console.WriteLine("Last Name:");
                            string lastName = Console.ReadLine();
                            Console.WriteLine("Age:");
                            int age = int.Parse(Console.ReadLine());
                            Console.WriteLine("Email:");
                            string email = Console.ReadLine();
                            Console.WriteLine("Phone Number:");
                            string phoneNumber = Console.ReadLine();
                            Console.WriteLine("Percentage:");
                            double percentage = double.Parse(Console.ReadLine());
                            Console.WriteLine("Department Id:");
                            int departmentId = int.Parse(Console.ReadLine());
                            bool isAdded = _studentRepository.AddStudent(new Student
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Age = age,
                                Email = email,
                                PhoneNumber = phoneNumber,
                                Percentage = percentage,
                                DepartmentId = departmentId
                            });
                            if (isAdded)
                            {
                                Console.WriteLine("Student added successfully");
                            }
                            else
                            {
                                Console.WriteLine("Failed to add student");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Enter student details to update:");
                            Console.WriteLine("Id:");
                            int updateId = int.Parse(Console.ReadLine());
                            Console.WriteLine("First Name:");
                            firstName = Console.ReadLine();
                            Console.WriteLine("Last Name:");
                            lastName = Console.ReadLine();
                            Console.WriteLine("Age:");
                            age = int.Parse(Console.ReadLine());
                            Console.WriteLine("Email:");
                            email = Console.ReadLine();
                            Console.WriteLine("Phone Number:");
                            phoneNumber = Console.ReadLine();
                            Console.WriteLine("Percentage:");
                            percentage = double.Parse(Console.ReadLine());
                            Console.WriteLine("Department Id:");
                            departmentId = int.Parse(Console.ReadLine());
                            bool isUpdated = _studentRepository.UpdateStudent(new Student
                            {
                                Id = updateId,
                                FirstName = firstName,
                                LastName = lastName,
                                Age = age,
                                Email = email,
                                PhoneNumber = phoneNumber,
                                Percentage = percentage,
                                DepartmentId = departmentId
                            });
                            if (isUpdated)
                            {
                                Console.WriteLine("Student updated successfully");
                            }
                            else
                            {
                                Console.WriteLine("Failed to update student");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Enter student id to delete:");
                            int deleteId = int.Parse(Console.ReadLine());
                            bool isDeleted = _studentRepository.DeleteStudent(deleteId);
                            if (isDeleted)
                            {
                                Console.WriteLine("Student deleted successfully");
                            }
                            else
                            {
                                Console.WriteLine("Failed to delete student");
                            }
                            break;
                        case 6:
                            Console.WriteLine("Enter student details to enroll:");
                            Console.WriteLine("First Name:");
                            firstName = Console.ReadLine();
                            Console.WriteLine("Last Name:");
                            lastName = Console.ReadLine();
                            Console.WriteLine("Age:");
                            age = int.Parse(Console.ReadLine());
                            Console.WriteLine("Email:");
                            email = Console.ReadLine();
                            Console.WriteLine("Phone Number:");
                            phoneNumber = Console.ReadLine();
                            Console.WriteLine("Percentage:");
                            percentage = double.Parse(Console.ReadLine());
                            Console.WriteLine("Department Id:");
                            departmentId = int.Parse(Console.ReadLine());
                            bool isEnrolled = _studentRepository.EnrollStudent(new Student
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Age = age,
                                Email = email,
                                PhoneNumber = phoneNumber,
                                Percentage = percentage,
                                DepartmentId = departmentId
                            });
                            if(isEnrolled)
                            {
                                Console.WriteLine("Student enrolled successfully");
                            }
                            else
                            {
                                Console.WriteLine("Failed to enroll student");
                            }
                            break;
                        case 7:
                            Console.WriteLine("Enter department id to get students:");
                            int deptId = int.Parse(Console.ReadLine());
                            var studentsByDept = _studentRepository.GetStudentsByDepartment(deptId);
                            if (studentsByDept.Count == 0)
                            {
                                Console.WriteLine("No students found in this department");
                                break;
                            }
                            foreach (var student in studentsByDept)
                            {
                                Console.WriteLine($"Id: {student.Id}, Name: {student.FirstName} {student.LastName}, Age: {student.Age}, Email: {student.Email}, Phone: {student.PhoneNumber}, Percentage: {student.Percentage}, DepartmentId: {student.DepartmentId}");
                            }
                            break;
                        case 8:
                            Console.WriteLine("Enter department id to get statistics:");
                            int departmentIdForStats = int.Parse(Console.ReadLine());
                            var stats = _studentRepository.GetDepartmentStatistics(departmentIdForStats);
                            if(stats.Item1 == 0)
                            {
                                Console.WriteLine("No students found in this department");
                                break;
                            }
                            Console.WriteLine($"Total Students: {stats.Item2}, Average Percentage: {stats.Item3}");
                            break;
                        case 9:
                            var departmentsTable = _studentRepository.DataSets().Tables["Departments"];
                            if (departmentsTable.Rows.Count == 0)
                            {
                                Console.WriteLine("No departments found");
                                break;
                            }
                            foreach (System.Data.DataRow row in departmentsTable.Rows)
                            {
                                Console.WriteLine($"Id: {row["DepartmentId"]}, Name: {row["DepartmentName"]}");
                            }
                            break;
                        case 10:
                            var coursesTable = _studentRepository.DataSets().Tables["Courses"];
                            if (coursesTable.Rows.Count == 0)
                            {
                                Console.WriteLine("No courses found");
                                break;
                            }
                            foreach (System.Data.DataRow row in coursesTable.Rows)
                            {
                                Console.WriteLine($"Id: {row["CourseId"]}, Name: {row["CourseName"]}");
                            }
                            break;
                        case 11:
                            return;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
    }
}
