CREATE PROCEDURE [dbo].[stp_Search_Json] 
@searchFields udt_SearchFields READONLY,
@queryStatement VARCHAR(MAX) OUTPUT
AS
BEGIN TRANSACTION 

SET @queryStatement = 'SELECT * FROM [dbo].[Media_Json] WHERE 1=1 '


SELECT @queryStatement = @queryStatement 
+ ' AND CONVERT(' + valueType + ', JSON_VALUE([DETAILS], ''$."' + CONVERT(VARCHAR, fieldId) + '"''))' 
+ (CASE WHEN valueType LIKE '%VARCHAR%' 
	THEN ' LIKE {' + CONVERT(VARCHAR, 0) + '} ' 
	ELSE ' = {' + CONVERT(VARCHAR, 0) + '}' END )

FROM @searchFields 

COMMIT TRANSACTION
GO
