ALTER PROCEDURE GetAllLectureQuiz
AS
BEGIN

	SELECT
		L.Lecture,
		L.CategoryId,
		QC.CategoryName,
		QQ.QuestionText
	FROM Lectures L
	FULL JOIN QuizCategory QC
	ON QC.CategoryId = L.CategoryId
	FULL JOIN QuizQuestion QQ
	ON QQ.CategoryId = QC.CategoryId
	ORDER BY QC.CategoryId ASC

END