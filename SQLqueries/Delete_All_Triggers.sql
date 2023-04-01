DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql += N'DROP TRIGGER ' + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.triggers
EXEC sp_executesql @sql;