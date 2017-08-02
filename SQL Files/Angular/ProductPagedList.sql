CREATE PROCEDURE [dbo].[ProductPagedList]
	@startRow int,
	@endRow int
AS
BEGIN
	SELECT  [Id]
		   ,[ProductName]
		   ,[SupplierId]
		   ,[UnitPrice]
		   ,[Package]
		   ,[IsDiscontinued]
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY Id ) AS RowNum,
						[Id]
					   ,[ProductName]
					   ,[SupplierId]
					   ,[UnitPrice]
					   ,[Package]
					   ,[IsDiscontinued]
			  FROM     [dbo].[Product]          
			) AS RowConstrainedResult
	WHERE   RowNum >= @startRow
		AND RowNum <= @endRow
	ORDER BY RowNum
END
