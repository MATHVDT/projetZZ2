CREATE PROCEDURE [dbo].[ProcedureAjoutPrenoms](@prenom AS varchar(100))
	


AS
BEGIN
	SELECT dbo.Prenom.Id
	FROM dbo.Prenom
	WHERE dbo.Prenom.Prenom = @prenom;
	
END;
