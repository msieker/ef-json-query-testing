CREATE PROCEDURE [dbo].[stp_Add_Json_Index] 
@keyName NVARCHAR(200), -- json prop name
@keyType NVARCHAR(200), -- sql type
@alias NVARCHAR(200), -- what will be used to name the new index and column
@recreate BIT -- if pre-existing index/columns should be deleted and remade
AS
BEGIN TRANSACTION



DECLARE @columnPrefix NVARCHAR(10) = '_v'
DECLARE @indexPrefix NVARCHAR(100) = 'IX_Media_Json_v_'

DECLARE @columnName NVARCHAR(200) = @columnPrefix + @alias
DECLARE @indexName NVARCHAR(200) = @indexPrefix + @alias

SELECT @columnName, @indexName

IF EXISTS(SELECT NAME FROM SYS.INDEXES WHERE NAME = @indexName AND OBJECT_ID = OBJECT_ID('dbo.Media_Json'))
	IF @recreate = 0
		THROW 60002, 'Index already exists', 1;
	ELSE
		BEGIN
			DECLARE @dropIndex NVARCHAR(MAX) = 'DROP INDEX [' + @indexName + '] ON Media_Json'
			EXEC sp_executesql @dropIndex
		END

IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Media_Json' AND COLUMN_NAME = @columnName)
	IF @recreate = 0
		THROW 60001, 'Column already exists', 1;
	ELSE
		BEGIN
			DECLARE @dropColumn NVARCHAR(MAX) = 'ALTER TABLE [Media_Json] DROP COLUMN [' + @columnName + ']'
			EXEC sp_executesql @dropColumn
		END

DECLARE @createColumn NVARCHAR(MAX) = 'ALTER TABLE [Media_Json] ADD [' + @columnName + '] AS CONVERT(JSON_VALUE(Details, ''$."' + @keyName + '"'') , ' + @keyType + ')'
DECLARE @createIndex NVARCHAR(MAX) = 'CREATE INDEX [' + @indexName + '] ON [Media_Json] ([' + @columnName + '])'

EXEC sp_executesql @createColumn
EXEC sp_executesql @createIndex



COMMIT TRANSACTION
GO
