ALTER PROCEDURE GetFinalGrade
@UserId INT
AS
BEGIN
  	SELECT
		FG.UserId,
		FG.TimeStamp,
		FG.FinalGrade
	FROM FinalGrade FG 
	WHERE FG.UserId = 1
END