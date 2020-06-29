ALTER PROCEDURE GetAllLectures
AS
BEGIN

	SELECT 	
		L.Lecture,
		L.CategoryId,
		QC.CategoryName	
	FROM Lectures L
	JOIN QuizCategory QC
	ON QC.CategoryId = L.CategoryId

END