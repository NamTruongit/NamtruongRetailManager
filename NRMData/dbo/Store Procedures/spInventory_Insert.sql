CREATE PROCEDURE [dbo].[spInventory_Insert]
	--[ProductID], [Quantity], [PurchasePrice], [PuchaseDate]
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@PurchaseDate datetime2
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO DBO.Inventory(ProductID,Quantity,PurchasePrice,PuchaseDate)
	VALUES (@ProductId,@Quantity,@PurchasePrice,@PurchaseDate);

END
