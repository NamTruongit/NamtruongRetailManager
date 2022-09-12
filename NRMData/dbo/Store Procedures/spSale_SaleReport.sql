CREATE PROCEDURE [dbo].[spSale_SaleReport]

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT [s].[SaleDate], [s].[SubTotal], [s].[Tax], [s].[Total], u.FirstsName, u.LastName, u.EmailAddress
	FROM DBO.Sale s
		inner join dbo.[User] u on s.CashierID=u.Id
END


