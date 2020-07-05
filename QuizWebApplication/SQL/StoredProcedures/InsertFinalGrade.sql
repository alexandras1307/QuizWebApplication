ALTER PROCEDURE InsertFinalGrade
@UserId INT,
@TimeStamp DATETIME
AS
BEGIN
	INSERT INTO FinalGrade 
	(
		UserId, 
		FinalGrade,
		TimeStamp
	)
	VALUES
	(
		@UserId,
		(SELECT AVG(Grade)
				FROM Grades
				WHERE UserId = @UserId),
		@TimeStamp
	)
END