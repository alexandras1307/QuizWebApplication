ALTER PROCEDURE InsertNewAnswer
@StudentId INT,
@QuestionId INT,
@Answer NVARCHAR(200)

AS
BEGIN
	INSERT INTO Answers
	(
		StudentId,
		QuestionId,
		Answer
	)
	VALUES
	(
		@StudentId,
		@QuestionId,
		@Answer
	)
END