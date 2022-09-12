CREATE PROCEDURE [dbo].[spInventory_GetAll]

AS
BEGIN
		SELECT [ProductID], [Quantity], [PurchasePrice], [PuchaseDate]
		FROM Inventory
END

