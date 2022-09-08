CREATE PROCEDURE [dbo].[spSaleLookUp]
	@CashierID nvarchar(128),
	@SaleDate datetime2
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id FROM Sale
	WHERE CashierID = @CashierID and SaleDate = @SaleDate;
END
