CREATE TABLE QuizQuestion
    (  
		QuestionId INT PRIMARY KEY IDENTITY(1,1),
		QuestionText VARCHAR(200) NOT NULL,
		QuestionOption1 VARCHAR(200) NOT NULL,
		QuestionOption2 VARCHAR(200) NOT NULL,
		QuestionOption3 VARCHAR(200) NOT NULL,
		QuestionOption4 VARCHAR(200) NOT NULL,
		CorrectOption VARCHAR(200) NOT NULL,
		CategoryId INT
    ) 

ALTER TABLE
    QuizQuestion
ADD CONSTRAINT
    fk_Question_Category_CategoryId
FOREIGN KEY (CategoryId) REFERENCES QuizCategory
