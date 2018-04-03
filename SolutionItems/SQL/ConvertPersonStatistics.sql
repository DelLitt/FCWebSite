--TRUNCATE TABLE [FCWeb].[dbo].[PersonStatistics];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Person] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonStatistics] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[PersonStatistics];

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

ALTER TABLE [FCWeb].[dbo].[Person] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonStatistics] CHECK CONSTRAINT ALL