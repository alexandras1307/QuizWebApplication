ALTER TABLE Lectures
    (  
		Id int primary key identity(1,1),
		CategoryId INT,
		Lecture varchar(200)
    ) 


ALTER TABLE
    Lectures
ADD CONSTRAINT
    fk_Lectures_Category_CategoryId
FOREIGN KEY (CategoryId) REFERENCES QuizCategory