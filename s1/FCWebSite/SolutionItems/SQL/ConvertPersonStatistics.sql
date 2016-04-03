SET IDENTITY_INSERT [FCWeb].[dbo].[PersonStatistics] ON;

INSERT INTO [FCWeb].[dbo].[PersonStatistics]
([Id],[personId],[teamId],[tourneyId],[Games],[Substitutes],[Goals],[Assists],[Yellows],[Reds])
SELECT [spId]
      ,[personId]
      ,[teamId]
      ,[tournamentId]
      ,[games]
      ,[substitutes]
      ,[goals]
      ,[assists]
      ,[yellows]
      ,[reds]
  FROM [SSDBLive].[dbo].[StatisticsPerson];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonStatistics] OFF;