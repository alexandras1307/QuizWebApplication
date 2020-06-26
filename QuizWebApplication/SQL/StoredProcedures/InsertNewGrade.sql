ALTER PROCEDURE InsertNewGrade
@UserId INT,
@CategoryId INT,
@Grade FLOAT

AS
BEGIN
	INSERT INTO Grades
	(
		UserId,
		CategoryId,
		Grade
	)
	VALUES
	(
		@UserId,
		@CategoryId,
		@Grade
	)
END
