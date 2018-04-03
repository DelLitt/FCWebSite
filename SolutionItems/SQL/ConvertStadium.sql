--TRUNCATE TABLE [FCWeb].[dbo].[Publication];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Stadium] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Team] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Game] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Stadium];

SET IDENTITY_INSERT [FCWeb].[dbo].[Stadium] ON;

INSERT INTO [FCWeb].[dbo].[Stadium]
([Id], [cityId] ,[Name], [NameFull], [Capacity], [Address], [Description], [Image])
SELECT [stadiumId]
      ,[cityId]
      ,[valueShort]
      ,[value]
      ,[capacity]
      ,[adress]
      ,[info]
      ,[photoPath]
  FROM [SSDBLive].[dbo].[Stadiums];

SET IDENTITY_INSERT [FCWeb].[dbo].[Stadium] OFF;

ALTER TABLE [FCWeb].[dbo].[Stadium] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Team] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Game] CHECK CONSTRAINT ALL
