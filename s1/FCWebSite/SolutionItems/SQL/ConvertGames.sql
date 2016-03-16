SET IDENTITY_INSERT [FCWeb].[dbo].[Game] ON;

INSERT INTO [FCWeb].[dbo].[Game]
	([Id],[homeId],[awayId],[stadiumId],[roundId],[homeScore],[awayScore],[GameDate],[Audience],[Note],
	 [Referees],[AwayAddScore],[HomePenalties],[AwayPenalties],[HomeAddScore],[Played],[ShowTime])
SELECT [gameId]
      ,[homeId]
      ,[awayId]
      ,[stadiumId]
      ,[tourneyDataId]
      ,[homeScore]
      ,[awayScore]
      ,[gameDate]
      ,[audience]
      ,[note]
      ,[referees]
      ,[awayAddScore]
      ,[homePenalties]
      ,[awayPenalties]
      ,[homeAddScore]
      ,[played]
      ,[showTime]
  FROM [SSDBLive].[dbo].[Games];

SET IDENTITY_INSERT [FCWeb].[dbo].[Game] OFF;