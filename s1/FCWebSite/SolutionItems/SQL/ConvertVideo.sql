SET IDENTITY_INSERT [FCWeb].[dbo].[Video] ON;

INSERT INTO [FCWeb].[dbo].[Video]
([Id], [Title], [Header], [URLKey], [Description] ,[Author], [Priority], [CodeHTML], [ExternalId],
[Visible], [Enable], [userCreated], [userChanged], [DateCreated], [DateChanged], [DateDisplayed])
SELECT [videoId]
      ,[theHeader]
      ,[theHeader]
      ,[outIdName]
	  ,[announce]
      ,[author]
      ,[priority]
      ,[videoCode]
      ,[videoExtId]
      ,[visible]
      ,[enable]
      ,[userCreator]
      ,[userLastChanger]
      ,[dateCreating]
      ,[dateLastChanging]
      ,[dateToShow]
  FROM [SSDBLive].[dbo].[article_Video];

SET IDENTITY_INSERT [FCWeb].[dbo].[Video] OFF;