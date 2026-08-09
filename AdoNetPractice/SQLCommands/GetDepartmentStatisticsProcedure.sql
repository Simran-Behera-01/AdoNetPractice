CREATE PROCEDURE GetDepartmentStatistics
	@DepartmentId INT,
	@StudentCount INT OUTPUT,
	@AveragePercentage INT OUTPUT
AS
BEGIN
	DECLARE @DepartmentExists BIT;

	SELECT  @DepartmentExists = Count(*) 
	FROM Departments
	WHERE DepartmentId = @DepartmentId;
	 
	IF @DepartmentExists = 0
		RETURN 0;

	SELECT @StudentCount = COUNT(*), @AveragePercentage = AVG(Percentage)
	FROM Students
	WHERE DepartmentId = @DepartmentId;

	RETURN 1;
END 