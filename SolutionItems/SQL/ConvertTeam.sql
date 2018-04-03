--TRUNCATE TABLE [FCWeb].[dbo].[Team];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Team] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonStatistics] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Person] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[TableRecord] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonCareer] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Team];

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

ALTER TABLE [FCWeb].[dbo].[Team] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonStatistics] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Person] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[TableRecord] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonCareer] CHECK CONSTRAINT ALL