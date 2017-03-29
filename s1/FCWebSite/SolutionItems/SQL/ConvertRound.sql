--TRUNCATE TABLE [FCWeb].[dbo].[Round];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Round] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Round];

SET IDENTITY_INSERT [FCWeb].[dbo].[Round] ON;

INSERT INTO [FCWeb].[dbo].[Round]
([Id], [tourneyId], [Name], [NameFull], [roundFormatId], [TeamList])
SELECT [tourneyDataId]
      ,[tournamentId]
      ,[value]
      ,[valueShort]
	  ,1
      ,[teamsList]
  FROM [SSDBLive].[dbo].[TourneyData];

SET IDENTITY_INSERT [FCWeb].[dbo].[Round] OFF;

ALTER TABLE [FCWeb].[dbo].[Round] CHECK CONSTRAINT ALL