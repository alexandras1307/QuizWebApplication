CREATE TABLE Answers
    (  
		AnswerId int primary key identity(1,1),
		UserId INT,
		QuestionId INT,
		Answer NVARCHAR(MAX)
    ) 

ALTER TABLE   
	Answers 
ADD CONSTRAINT
    fk_Answers_UserProfile_UserId
FOREIGN KEY (UserId) REFERENCES UserProfile

ALTER TABLE
    Answers
ADD CONSTRAINT
    fk_Answers_Question_QuestionId
FOREIGN KEY (QuestionId) REFERENCES QuizQuestion