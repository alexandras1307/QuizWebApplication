ALTER PROCEDURE InsertUserProfile
@Email NVARCHAR(200),
@Password NVARCHAR(200),
@FirstName NVARCHAR(200),
@LastName NVARCHAR(200),
@Role INT,
@Active BIT

AS
BEGIN
	
	INSERT INTO UserProfile
	(
	 Email,
	 Password,
	 FirstName,
	 LastName,
	 Role,
	 Active
	)
	VALUES
	(
	 @Email,
	 @Password,
	 @FirstName,
	 @LastName,
	 @Role,
	 @Active
	)

END

