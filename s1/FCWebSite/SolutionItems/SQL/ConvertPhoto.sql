SET IDENTITY_INSERT [FCWeb].[dbo].[ImageGallery] ON;

INSERT INTO [FCWeb].[dbo].[ImageGallery]
([Id], [Title], [Header], [URLKey], [Description], [Author], [Priority], [Visible], [Enable], 
[userCreated], [userChanged], [DateCreated], [DateChanged], [DateDisplayed])
SELECT [photoId]
      ,[theHeader]
      ,[theHeader]
      ,[outIdName]
      ,[announce]
      ,[author]
      ,[priority]
      ,[visible]
      ,[enable]
      ,[userCreator]
      ,[userLastChanger]
      ,[dateCreating]
      ,[dateLastChanging]
      ,[dateToShow]
  FROM [SSDBLive].[dbo].[article_Photo];

SET IDENTITY_INSERT [FCWeb].[dbo].[ImageGallery] OFF;