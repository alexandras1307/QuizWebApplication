ALTER PROCEDURE GetAllStudents
AS
BEGIN

	SELECT 
		UP.FirstName,
		UP.LastName,
		UP.Email,
		UP.UserId
	FROM UserProfile UP
	WHERE Role = '1'
	ORDER BY UP.LastName asc

END