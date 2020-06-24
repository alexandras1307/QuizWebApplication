CREATE PROCEDURE GetUserProfile
@UserId INT
AS
BEGIN
	SELECT 
		FirstName,
		LastName,
		Email	       
	FROM [UserProfile] 
	WHERE UserId = @UserId
END