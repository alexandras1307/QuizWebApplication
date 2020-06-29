ALTER PROCEDURE InsertNewLecture
@CategoryId INT,
@Lecture NVARCHAR(200)

AS
BEGIN

	INSERT INTO Lectures
	(		
		CategoryId,
		Lecture
	)
	VALUES
	(
		@CategoryId,
		@Lecture		
	)
END