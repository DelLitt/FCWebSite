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