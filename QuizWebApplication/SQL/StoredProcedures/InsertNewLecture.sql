ALTER PROCEDURE InsertNewLecture
@Lecture NVARCHAR(200),
@CategoryId INT

AS
BEGIN

	INSERT INTO Lectures
	(
		Lecture,
		CategoryId
	)
	VALUES
	(
		@Lecture,
		@CategoryId
	)
END