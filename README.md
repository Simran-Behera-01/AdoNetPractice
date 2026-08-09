# AdoNetPractice

A C# console application created to practice ADO.NET with SQL Server.

## Concepts Practiced

- SqlConnection and connection factory
- SqlCommand
- ExecuteReader()
- ExecuteNonQuery()
- ExecuteScalar()
- Parameterized queries
- CRUD operations
- Transactions with Commit and Rollback
- Stored procedures
- Output parameters
- Return values
- SqlDataAdapter
- DataTable
- DataSet
- SCOPE_IDENTITY()

## Project Structure

AdoNetPractice
├── Data
│   ├── DbConnectionFactory.cs
│   └── StudentRepository.cs
├── Models
│   └── Student.cs
├── SQLCommands
│   ├── GetDepartmentStatisticsProcedure.sql
│   └── GetStudentsByDepartmentProcedure.sql
└── Program.cs
