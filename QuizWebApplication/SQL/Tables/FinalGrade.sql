CREATE TABLE FinalGrade
    (  
		Id int primary key identity(1,1),
		UserId INT,
		FinalGrade FLOAT,
		TimeStamp DATETIME
    )

ALTER TABLE   
	FinalGrade 
ADD CONSTRAINT
    fk_FinalGrades_UserProfile_UserId
FOREIGN KEY (UserId) REFERENCES UserProfile