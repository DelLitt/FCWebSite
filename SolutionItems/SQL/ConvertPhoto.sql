--TRUNCATE TABLE [FCWeb].[dbo].[ImageGallery];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[ImageGallery] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Game] NOCHECK CONSTRAINT ALL
DELETE FROM [FCWeb].[dbo].[ImageGallery];
ALTER TABLE [FCWeb].[dbo].[Game] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[ImageGallery] CHECK CONSTRAINT ALL

SET IDENTITY_INSERT [FCWeb].[dbo].[ImageGallery] ON;

INSERT INTO [FCWeb].[dbo].[ImageGallery]
([Id],[Title],[Header],[URLKey],[Description],[Author],[Priority],[Enable],[userCreated]
	,[userChanged],[DateCreated],[DateChanged],[DateDisplayed],[Visibility])
SELECT [photoId]
      ,[theHeader]
      ,[theHeader]
      ,[outIdName]
      ,[announce]
      ,[author]
      ,[priority]
      ,[enable]
      ,[userCreator]
      ,[userLastChanger]
      ,[dateCreating]
      ,[dateLastChanging]
      ,[dateToShow]
	  ,3
  FROM [SSDBLive].[dbo].[article_Photo];

SET IDENTITY_INSERT [FCWeb].[dbo].[ImageGallery] OFF;