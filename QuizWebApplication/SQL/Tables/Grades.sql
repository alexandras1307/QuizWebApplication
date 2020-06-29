CREATE TABLE Grades
    (  
		Id int primary key identity(1,1),
		UserId INT,
		CategoryId INT,
		Grade FLOAT
    ) 

ALTER TABLE   
	Grades 
ADD CONSTRAINT
    fk_Grades_UserProfile_UserId
FOREIGN KEY (UserId) REFERENCES UserProfile

ALTER TABLE
    Grades
ADD CONSTRAINT
    fk_Grades_Category_CategoryId
FOREIGN KEY (CategoryId) REFERENCES QuizCategory