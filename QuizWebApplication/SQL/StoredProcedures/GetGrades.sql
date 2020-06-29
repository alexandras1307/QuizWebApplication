ALTER PROCEDURE GetGrades
@UserId INT
AS
BEGIN
	SELECT
		QC.CategoryName,
		G.Grade,
		US.FirstName,
		US.LastName,
		US.UserId
	FROM Grades G 
	JOIN QuizCategory QC
	ON QC.CategoryId = G.CategoryId
	JOIN UserProfile US
	ON US.UserId = G.UserId
	WHERE G.UserId = @UserId
		AND US.Role = 1
	Order by QC.CategoryId
END