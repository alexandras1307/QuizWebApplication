ALTER PROCEDURE GetGrades
@UserId INT
AS
BEGIN
	SELECT
		US.FirstName,
		US.LastName,
		US.UserId,
		G.Grade,
		QC.CategoryName
	FROM UserProfile US 
	LEFT JOIN Grades G
	ON US.UserId = G.UserId
	LEFT JOIN QuizCategory QC
	ON G.CategoryId = QC.CategoryId
	WHERE US.UserId = 1
		AND US.Role = 1
	Order by QC.CategoryId
END