--TRUNCATE TABLE [FCWeb].[dbo].[Country];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Country] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[City] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Country];

SET IDENTITY_INSERT [FCWeb].[dbo].[Country] ON;

INSERT INTO [FCWeb].[dbo].[Country]
([Id], [Name], [NameFull])
SELECT [countriesId]
      ,[valueShort]
	  ,[value]
  FROM [SSDBLive].[dbo].[countries];

SET IDENTITY_INSERT [FCWeb].[dbo].[Country] OFF;

ALTER TABLE [FCWeb].[dbo].[Country] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[City] CHECK CONSTRAINT ALL