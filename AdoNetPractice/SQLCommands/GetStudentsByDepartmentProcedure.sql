CREATE PROCEDURE GetStudentsByDepartment
	@DepartmentId INT
AS 
BEGIN
	SELECT * 
	FROM Students
	WHERE DepartmentId = @DepartmentId;
END;