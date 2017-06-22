CREATE PROCEDURE Supp_SearchByNames
	@contactName varchar(40)	
AS
BEGIN
	SELECT	 Id
			,CompanyName
			,ContactName
			,ContactTitle
			,City
			,Country
			,Phone
			,Fax
	FROM dbo.Supplier
	WHERE ContactName = @contactName 
END
GO
