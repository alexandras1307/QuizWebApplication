ALTER PROCEDURE InsertUserTakenQuiz
@UserId INT,
@CategoryId INT

AS
BEGIN
	INSERT INTO UserTakenQuiz
	(
		UserId,
		CategoryId		
	)
	VALUES
	(
		@UserId,
		@CategoryId
	)
END