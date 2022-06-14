CREATE PROCEDURE [dbo].[stp_Search_Json] 
@searchFields udt_SearchFields READONLY,
@queryStatement VARCHAR(MAX) OUTPUT
AS
BEGIN TRANSACTION 

DECLARE @count INT = 0;
DECLARE @statement VARCHAR(MAX) = ''
SELECT  @statement = @statement + ' AND CONVERT(' + valueType + ', JSON_VALUE([DETAILS], ''$."' + CONVERT(VARCHAR, fieldId) + '"''))' 
+ (CASE WHEN valueType LIKE '%VARCHAR%' 
	THEN ' LIKE {' + CONVERT(VARCHAR, @count) + '} ' 
	ELSE ' = {' + CONVERT(VARCHAR, @count) + '}' END ),
	@count = @count + 1
FROM @searchFields

SELECT @queryStatement = 'SELECT * FROM [dbo].[Media_Json] WHERE 1=1 ' + @statement

COMMIT TRANSACTION
GO
