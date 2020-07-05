ALTER PROCEDURE UpdateFinalGrade
@UserId INT
AS
BEGIN
	UPDATE FinalGrade 
	SET FinalGrade.FinalGrade = (SELECT AVG(Grade)
							FROM Grades
							WHERE UserId = @UserId)
	WHERE UserId = @UserId
END