ALTER PROCEDURE GetAllQuestions
AS
BEGIN
	SELECT
		QQ.QuestionText,
		QQ.QuestionOption1,
		QQ.QuestionOption2,
		QQ.QuestionOption3,
		QQ.QuestionOption4,
		QQ.CorrectOption,
		QC.CategoryName
	FROM QuizQuestion QQ
	JOIN QuizCategory QC ON QQ.CategoryId = QC.CategoryId
END