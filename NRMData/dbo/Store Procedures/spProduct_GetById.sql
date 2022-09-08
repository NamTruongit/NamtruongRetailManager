CREATE PROCEDURE [dbo].[spProduct_GetById]
	@Id int
AS
	
BEGIN
	SET NOCOUNT ON;
	select Id,ProductName,[Decription],RetailPrice,QuantityInStock,IsTaxable
	from dbo.Product
	WHERE Id = @Id
END
