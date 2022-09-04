CREATE PROCEDURE [dbo].[spProduct_GetAll]

AS
	
begin
	set nocount on;
	select Id,ProductName,Decription,RetailPrice,QuantityInStock
	from dbo.Product
	order by ProductName;

end
