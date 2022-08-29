CREATE PROCEDURE [dbo].[SPUserLookUp]
	@id nvarchar(128)
AS
	SELECT Id,FirstsName,LastName,EmailAddress FROM [dbo].[User]
	WHERE Id = @id
RETURN 0