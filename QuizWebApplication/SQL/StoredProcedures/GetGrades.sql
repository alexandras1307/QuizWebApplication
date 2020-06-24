ALTER PROCEDURE GetGrades
@UserId INT
AS
BEGIN
	SELECT
		A.Answer,
		A.UserId, 
		QQ.QuestionID,
		QQ.QuestionText,
		QQ.CorrectOption,
		QC.CategoryId,
		QC.CategoryName
	FROM QuizCategory QC 
	JOIN QuizQuestion QQ
	ON QC.CategoryId = QQ.CategoryId
	JOIN Answers A
    ON QQ.QuestionID = A.QuestionId
	WHERE A.UserId = @UserId
	Order by QC.CategoryId
END