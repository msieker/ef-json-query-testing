CREATE PROCEDURE [dbo].[stp_Search_Json] 
@searchFields udt_SearchFields READONLY
AS
BEGIN TRANSACTION 

DECLARE @statement NVARCHAR(MAX) = 'SELECT * FROM [dbo].[Media_Json] WHERE 1=1 '


SELECT @statement = @statement 
+ ' AND CONVERT(' + valueType + ', JSON_VALUE([DETAILS], ''$."' + CONVERT(VARCHAR, fieldId) + '"''))' 
+ (CASE WHEN valueType LIKE '%VARCHAR%' 
	THEN ' LIKE ''%' + searchValue + '%'' ' 
	ELSE ' = ' + searchValue END )
FROM @searchFields 

EXEC sp_executesql @statement
 

COMMIT TRANSACTION
GO
