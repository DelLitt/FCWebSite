--TRUNCATE TABLE [FCWeb].[dbo].[Tourney];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Tourney] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Round] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[TableRecord] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Team] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonStatistics] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Tourney];

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

ALTER TABLE [FCWeb].[dbo].[Tourney] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Round] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[TableRecord] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Team] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonStatistics] CHECK CONSTRAINT ALL

