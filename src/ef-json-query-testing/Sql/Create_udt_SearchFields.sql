CREATE TYPE [dbo].[udt_SearchFields] AS TABLE (
	fieldId INT, 
	searchValue VARCHAR(500),
	valueType VARCHAR(100)
)
