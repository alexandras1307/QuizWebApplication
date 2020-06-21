ALTER PROCEDURE GetAllCategories
AS
BEGIN

	SELECT 
		QC.CategoryId,
		QC.CategoryName
	FROM QuizCategory QC

END