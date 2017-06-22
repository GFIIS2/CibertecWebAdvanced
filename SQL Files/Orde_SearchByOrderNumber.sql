CREATE PROCEDURE Orde_SearchByOrderNumber
	@orderNumber int
AS
BEGIN
	SELECT   Id
			,OrderDate
			,OrderNumber
			,CustomerId
			,TotalAmount
	FROM dbo.[Order]
	WHERE [OrderNumber] = @orderNumber 
END
GO
