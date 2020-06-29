Create Table UserProfile  
    (  
	UserId int primary key identity,
	Email NVARCHAR(200),
	Password NVARCHAR(200),
	FirstName NVARCHAR(200),
	LastName NVARCHAR(200),
	Role INT,
	Active BIT
    ) 
