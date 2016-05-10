SET IDENTITY_INSERT [FCWeb].[dbo].[Game] ON;

INSERT INTO [FCWeb].[dbo].[Game]
	([Id],[homeId],[awayId],[stadiumId],[roundId],[homeScore],[awayScore],[GameDate],[Audience],[Note],
	 [Referees],[AwayAddScore],[HomePenalties],[AwayPenalties],[HomeAddScore],[Played],[ShowTime],[videoId],[imageGalleryId])
SELECT g.[gameId]
      ,g.[homeId]
      ,g.[awayId]
      ,g.[stadiumId]
      ,g.[tourneyDataId]
      ,g.[homeScore]
      ,g.[awayScore]
      ,g.[gameDate]
      ,g.[audience]
      ,g.[note]
      ,g.[referees]
      ,g.[awayAddScore]
      ,g.[homePenalties]
      ,g.[awayPenalties]
      ,g.[homeAddScore]
      ,g.[played]
      ,g.[showTime]
	  , (SELECT TOP 1 ci.[videoId] 
		 FROM [SSDBLive].[dbo].[GamesExt] ge	  
			LEFT JOIN [SSDBLive].[dbo].[content_Items] ci
				ON ge.contentId = ci.contentId
		 WHERE g.[gameId] = ge.[gameId])
	  ,	(SELECT TOP 1 ci.[photoId] 
		 FROM [SSDBLive].[dbo].[GamesExt] ge	  
			LEFT JOIN [SSDBLive].[dbo].[content_Items] ci
				ON ge.contentId = ci.contentId
		 WHERE g.[gameId] = ge.[gameId])
	  
  FROM [SSDBLive].[dbo].[Games] g

SET IDENTITY_INSERT [FCWeb].[dbo].[Game] OFF;