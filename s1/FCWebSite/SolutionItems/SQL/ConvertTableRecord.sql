SET IDENTITY_INSERT [FCWeb].[dbo].[TableRecord] ON;

INSERT INTO [FCWeb].[dbo].[TableRecord]
           ([Id]
           ,[tourneyId]
           ,[teamId]
           ,[Games]
           ,[Wins]
           ,[Draws]
           ,[Loses]
           ,[GoalsFor]
           ,[GoalsAgainst]
           ,[Points]
           ,[PointsVirtual]
           ,[Position]
           ,[Active])
SELECT [tableRecordId]
      ,[tournamentId]
      ,[teamId]
      ,[games]
      ,[wins]
      ,[draws]
      ,[loses]
      ,[goalsFor]
      ,[goalsAgainst]
      ,[points]
      ,[virtualPoints]
      ,[position]
      ,[active]
  FROM [SSDBLive].[dbo].[TableRecords];

SET IDENTITY_INSERT [FCWeb].[dbo].[TableRecord] OFF;