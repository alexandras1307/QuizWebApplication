ALTER PROCEDURE InsertNewCategory
@CategoryName NVARCHAR(200)
AS
BEGIN

	INSERT INTO QuizCategory
	(
		CategoryName
	)
	VALUES
	(
		@CategoryName
	)

END