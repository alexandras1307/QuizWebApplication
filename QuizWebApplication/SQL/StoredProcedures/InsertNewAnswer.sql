ALTER PROCEDURE InsertNewAnswer
@UserId INT,
@QuestionId INT,
@Answer NVARCHAR(200)

AS
BEGIN
	INSERT INTO Answers
	(
		UserId,
		QuestionId,
		Answer
	)
	VALUES
	(
		@UserId,
		@QuestionId,
		@Answer
	)
END