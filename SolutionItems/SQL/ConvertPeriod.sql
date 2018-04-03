--TRUNCATE TABLE [FCWeb].[dbo].[Period];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Period] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Period];

SET IDENTITY_INSERT [FCWeb].[dbo].[Period] ON;

INSERT INTO [FCWeb].[dbo].[Period]
([Id], [gameFormatId], [Number], [Duration])
SELECT [trnTypePeriodsId]
      ,[tourneyTypeId]
      ,[trntpPeriod]
      ,[trntpDuration]
  FROM [SSDBLive].[dbo].[TourneyTypePeriods];

SET IDENTITY_INSERT [FCWeb].[dbo].[Period] OFF;

ALTER TABLE [FCWeb].[dbo].[Period] CHECK CONSTRAINT ALL