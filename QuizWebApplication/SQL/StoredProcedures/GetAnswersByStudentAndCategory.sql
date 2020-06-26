ALTER PROCEDURE GetAnswersByStudentAndCategory
@CategoryId INT,
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
	FROM [Answers] A 
	JOIN QuizQuestion QQ ON A.QuestionId = QQ.QuestionID 
	JOIN QuizCategory QC ON QC.CategoryId = QQ.CategoryId
	WHERE A.UserId = @UserId
	AND QQ.CategoryId = @CategoryId
END