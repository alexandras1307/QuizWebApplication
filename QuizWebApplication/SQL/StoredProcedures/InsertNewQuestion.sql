ALTER PROCEDURE InsertNewQuestion
@QuestionText NVARCHAR(200),
@QuestionOption1 NVARCHAR(200),
@QuestionOption2 NVARCHAR(200),
@QuestionOption3 NVARCHAR(200),
@QuestionOption4 NVARCHAR(200),
@CorrectOption NVARCHAR(200),
@CategoryId INT

AS
BEGIN

	INSERT INTO QuizQuestion
	(
		QuestionText,
		QuestionOption1,
		QuestionOption2,
		QuestionOption3,
		QuestionOption4,
		CorrectOption,
		CategoryId
	)
	VALUES
	(
		@QuestionText,
		@QuestionOption1,
		@QuestionOption2,
		@QuestionOption3,
		@QuestionOption4,
		@CorrectOption,
		@CategoryId
	)
END