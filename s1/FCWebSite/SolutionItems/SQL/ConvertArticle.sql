SET IDENTITY_INSERT FCWeb.dbo.Publication OFF;

SET IDENTITY_INSERT [FCWeb].[dbo].[Article] ON;

INSERT INTO [FCWeb].[dbo].[Article]
([Id], [Title], [Header], [URLKey], [Lead], [ContentHTML], [Author], [Visible],
 [Enable], [userCreated], [userChanged], [DateCreated], [DateChanged], [DateDisplayed])
SELECT [articleId]
      ,[title]
      ,[theHeader]
      ,[outIdName]
      ,[announce]
      ,[htmlContent]
      ,[author]
      ,[visible]
      ,[enable]
      ,[userCreator]
      ,[userLastChanger]
      ,[dateCreating]
      ,[dateLastChanging]
      ,[dateToShow]
  FROM [SSDBLive].[dbo].[article_Text];

  SET IDENTITY_INSERT [FCWeb].[dbo].[Article] OFF;