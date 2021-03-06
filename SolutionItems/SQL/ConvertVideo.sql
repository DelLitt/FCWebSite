--TRUNCATE TABLE [FCWeb].[dbo].[Video];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Video] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Game] NOCHECK CONSTRAINT ALL
DELETE FROM [FCWeb].[dbo].[Video];
ALTER TABLE [FCWeb].[dbo].[Game] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Video] CHECK CONSTRAINT ALL

SET IDENTITY_INSERT [FCWeb].[dbo].[Video] ON;

INSERT INTO [FCWeb].[dbo].[Video]
([Id],[Title],[Header],[URLKey],[Description],[Author],[Priority],[CodeHTML],[ExternalId],[Enable]
	,[userCreated],[userChanged],[DateCreated],[DateChanged],[DateDisplayed],[Visibility])
SELECT [videoId]
      ,[theHeader]
      ,[theHeader]
      ,[outIdName]
	  ,[announce]
      ,[author]
      ,[priority]
      ,[videoCode]
      ,[videoExtId]
      ,[enable]
      ,[userCreator]
      ,[userLastChanger]
      ,[dateCreating]
      ,[dateLastChanging]
      ,[dateToShow]
	  ,3 AS [Visibility]
  FROM [SSDBLive].[dbo].[article_Video];

SET IDENTITY_INSERT [FCWeb].[dbo].[Video] OFF;