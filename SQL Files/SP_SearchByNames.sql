CREATE PROCEDURE SearchByNames
	@firstName varchar(40),
	@lastName varchar(40)
AS
BEGIN
	SELECT [Id]
		  ,[FirstName]
		  ,[LastName]
		  ,[City]
		  ,[Country]
		  ,[Phone] 
	FROM dbo.Customer
	WHERE LastName=@lastName and FirstName=@firstName
END
GO
