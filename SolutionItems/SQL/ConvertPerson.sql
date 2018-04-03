--TRUNCATE TABLE [FCWeb].[dbo].[Person];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Person] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonStatistics] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonCareer] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[ProtocolRecord] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Person];

SET IDENTITY_INSERT [FCWeb].[dbo].[Person] ON;

INSERT INTO [FCWeb].[dbo].[Person]
([Id],[NameFirst],[NameMiddle],[NameLast],[NameNick],[BirthDate],[Height],[Weight],
[cityId],[teamId],[roleId],[Image],[Info],[Number],[personStatusId],[Active],[CustomIntValue])
SELECT [personId]
      ,[nameFirst]
      ,[nameSecond]
      ,[nameLast]
      ,[nameNick]
      ,[birthDate]
      ,[height]
      ,[weight]
      ,[cityId]
      ,[teamId]
      ,[roleExtId]
      ,[photoName]
      ,[info]
      ,[number]
      ,[personStatusId]
      ,[active]
      ,[customNum]
  FROM [SSDBLive].[dbo].[Persons];

SET IDENTITY_INSERT [FCWeb].[dbo].[Person] OFF;

ALTER TABLE [FCWeb].[dbo].[Person] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonStatistics] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonCareer] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[ProtocolRecord] CHECK CONSTRAINT ALL