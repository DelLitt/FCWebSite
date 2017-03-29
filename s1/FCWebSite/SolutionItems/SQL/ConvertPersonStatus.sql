--TRUNCATE TABLE [FCWeb].[dbo].[PersonStatus];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[PersonStatus] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Person] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[PersonStatus];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonStatus] ON;

INSERT INTO [FCWeb].[dbo].[PersonStatus]
([Id], [Name], [NameFull])
SELECT [psId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[PersonStatus];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonStatus] OFF;

ALTER TABLE [FCWeb].[dbo].[PersonStatus] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Person] CHECK CONSTRAINT ALL