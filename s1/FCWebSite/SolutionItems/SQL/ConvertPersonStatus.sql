SET IDENTITY_INSERT [FCWeb].[dbo].[PersonStatus] ON;

INSERT INTO [FCWeb].[dbo].[PersonStatus]
([Id], [Name], [NameFull])
SELECT [psId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[PersonStatus];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonStatus] OFF;