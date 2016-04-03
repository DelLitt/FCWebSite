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