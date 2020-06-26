CREATE TABLE UserTakenQuiz
    (  
		Id int primary key identity(1,1),
		UserId INT,
		CategoryId INT
    ) 


ALTER TABLE
    UserTakenQuiz
ADD CONSTRAINT
    fk_UserTakenQuiz_UserProfile_UserId
FOREIGN KEY (UserId) REFERENCES UserProfile

ALTER TABLE
    UserTakenQuiz
ADD CONSTRAINT
    fk_UserTakenQuiz_Category_CategoryId
FOREIGN KEY (CategoryId) REFERENCES QuizCategory