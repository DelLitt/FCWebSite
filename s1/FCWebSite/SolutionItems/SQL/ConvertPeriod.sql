SET IDENTITY_INSERT [FCWeb].[dbo].[Period] ON;

INSERT INTO [FCWeb].[dbo].[Period]
([Id], [gameFormatId], [Number], [Duration])
SELECT [trnTypePeriodsId]
      ,[tourneyTypeId]
      ,[trntpPeriod]
      ,[trntpDuration]
  FROM [SSDBLive].[dbo].[TourneyTypePeriods];

SET IDENTITY_INSERT [FCWeb].[dbo].[Period] OFF;