Create TABLE Files
    (  
		Id int primary key identity(1,1),
		Name VARCHAR(50),
		ContentType VARCHAR(200),
		Data varchar(MAX)
    ) 


ALTER TABLE
    Lectures
ADD CONSTRAINT
    fk_Lectures_Category_CategoryId
FOREIGN KEY (CategoryId) REFERENCES QuizCategory