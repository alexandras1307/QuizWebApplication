ALTER PROCEDURE GetAllLectures
AS
BEGIN

	SELECT 	
		L.Lecture,
		L.CategoryId	
	FROM Lectures L

END