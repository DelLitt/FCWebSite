SET IDENTITY_INSERT [FCWeb].[dbo].[Team] ON;

INSERT INTO [FCWeb].[dbo].[Team]
	([Id],[Name],[NameFull],[NamePre],[Image],[Description],[Address],
	 [WebSite],[Email],[cityId],[stadiumId],[teamTypeId],[Active],[mainTourneyId])
SELECT [teamId]
      ,[name]
      ,[nameFull]
      ,[namePre]
      ,[imageName]
      ,[info]
      ,[adress]
      ,[webSite]
      ,[email]
      ,[cityId]
      ,[stadiumId]
      ,[teamType]
      ,[active]
	  ,[mainTournamentId]
  FROM [SSDBLive].[dbo].[Teams];

SET IDENTITY_INSERT [FCWeb].[dbo].[Team] OFF;
