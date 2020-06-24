ALTER PROCEDURE UserLogin
@Email NVARCHAR(200),
@Password NVARCHAR(200)

AS
BEGIN

	SELECT
		UP.UserId,
		UP.Role
	FROM UserProfile UP
	WHERE UP.Active = 1
	AND UP.Email = @Email
	AND UP.Password = @Password

END