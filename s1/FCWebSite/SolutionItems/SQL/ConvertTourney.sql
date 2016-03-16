SET IDENTITY_INSERT [FCWeb].[dbo].[Tourney] ON;

INSERT INTO [FCWeb].[dbo].[Tourney]
([Id], [Name], [NameFull], [cityId], [tourneyTypeId], [Description], [DateStart], [DateEnd])
SELECT [tournamentId]
      ,[Value]
      ,[ValueShort]
      ,[cityId]
      ,[tourneyTypeId]
      ,[Info]
      ,[DateStart]
      ,[DateEnd]
  FROM [SSDBLive].[dbo].[Tournaments];

SET IDENTITY_INSERT [FCWeb].[dbo].[Tourney] OFF;


