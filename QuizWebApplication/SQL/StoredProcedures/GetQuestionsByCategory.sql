ALTER PROCEDURE GetQuestionsByCategory
@CategoryId INT
AS
BEGIN
	SELECT
		QQ.QuestionId,
		QQ.QuestionText,
		QQ.QuestionOption1,
		QQ.QuestionOption2,
		QQ.QuestionOption3,
		QQ.QuestionOption4,
		QQ.CorrectOption,
		QC.CategoryName
	FROM QuizQuestion QQ
	JOIN QuizCategory QC ON QQ.CategoryId = QC.CategoryId
	WHERE QQ.CategoryId = @CategoryId
END