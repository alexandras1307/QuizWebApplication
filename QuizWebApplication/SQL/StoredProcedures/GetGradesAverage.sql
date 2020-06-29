ALTER PROCEDURE GetGradesAverage
@UserId INT
AS
BEGIN

	SELECT AVG(Grade)
	FROM Grades
	WHERE UserId = @UserId

END