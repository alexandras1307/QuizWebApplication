ALTER PROCEDURE GetUnsolvedQuiz
@UserId INT
AS
BEGIN

	SELECT 
		QC.CategoryId,
		QC.CategoryName
	FROM QuizCategory QC 
	WHERE QC.CategoryId NOT IN (
							SELECT CategoryId
							FROM UserTakenQuiz UTQ WHERE UTQ.UserId = @UserId
							)

END
