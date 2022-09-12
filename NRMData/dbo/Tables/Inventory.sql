CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductID] INT NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT 1, 
    [PurchasePrice] MONEY NOT NULL, 
    [PuchaseDate] DATETIME NOT NULL DEFAULT getutcdate(), 
    CONSTRAINT [FK_Inventory_ToProduct] FOREIGN KEY (ProductID) REFERENCES Product(Id), 

)
