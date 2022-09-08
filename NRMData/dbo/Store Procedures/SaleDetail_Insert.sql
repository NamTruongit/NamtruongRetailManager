CREATE PROCEDURE [dbo].[SaleDetail_Insert]
	@SaleID int,
	@ProducID int,
	@Quantity int,
	@PurchasePrice money,
	@Tax money
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO dbo.SaleDetail(SaleID,ProducID,Quantity,PurchasePrice,Tax)
	VALUES (@SaleID,@ProducID,@Quantity,@PurchasePrice,@Tax)
END