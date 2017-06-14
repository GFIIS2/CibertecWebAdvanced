CREATE PROCEDURE Prod_SearchByNames
	@productName varchar(40)	
AS
BEGIN
	SELECT [Id]
		  ,ProductName
		  ,SupplierId
		  ,UnitPrice
		  ,Package
		  ,IsDiscontinued 
	FROM dbo.Product
	WHERE ProductName=@productName 
END
GO
