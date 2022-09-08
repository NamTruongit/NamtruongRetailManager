CREATE TABLE [dbo].[SaleDetail]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [SaleID] INT NOT NULL, 
    [ProducID] INT NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT 1,
    [PurchasePrice] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    CONSTRAINT [FK_SaleDetail_ToSale] FOREIGN KEY (SaleID) REFERENCES Sale(Id), 
    CONSTRAINT [FK_SaleDetail_ToProduct] FOREIGN KEY (ProducID) REFERENCES Product(Id), 

    
)
