--TRUNCATE TABLE [FCWeb].[dbo].[Publication];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Publication] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[ImageGallery] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Video] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Publication];

SET IDENTITY_INSERT [FCWeb].[dbo].[Publication] OFF;

INSERT INTO [FCWeb].[dbo].[Publication]
	([Title],[Header],[URLKey],[Image],[ContentHTML],[ShowImageInContet],[Priority],[Visibility]
     ,[Enable],[imageGalleryId],[videoId],[userCreated],[userChanged],[DateCreated],[DateChanged]
	 ,[DateDisplayed],[Lead],[Author])
SELECT [title]
      ,[theHeader]
      ,[outIdName]
	  ,[ImageUrl]      
      ,[htmlContent]
	  ,1
	  ,at.[priority]
	  ,3
	  ,at.[enable]
	  ,ci.photoId
	  ,ci.videoId
	  ,at.[userCreator]
      ,at.[userLastChanger]
      ,at.[dateCreating]
      ,at.[dateLastChanging]
      ,at.[dateToShow]
	  ,[announce]
      ,[author]
  FROM [SSDBLive].[dbo].[article_Text] at
  LEFT JOIN [SSDBLive].[dbo].[content_Items] ci
	ON at.articleId = ci.articleId;

ALTER TABLE [FCWeb].[dbo].[Publication] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[ImageGallery] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Video] CHECK CONSTRAINT ALL