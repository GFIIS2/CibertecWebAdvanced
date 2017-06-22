CREATE PROCEDURE OrdeItem_SearchByUnitPrice
	@unitPrice decimal(18,2)
AS
BEGIN
	SELECT   Id
			,OrderId
			,ProductId
			,UnitPrice
			,Quantity
	FROM dbo.OrderItem
	WHERE UnitPrice=@unitPrice 
END
GO
