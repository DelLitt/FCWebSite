--TRUNCATE TABLE [FCWeb].[dbo].[City];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[City] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Stadium] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Person] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Team] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Tourney] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[City];

SET IDENTITY_INSERT [FCWeb].[dbo].[City] ON;

INSERT INTO [FCWeb].[dbo].[City]
([Id], [countryId], [Name], [NameFull])
SELECT [cityId]
      ,[countryId]
	  ,[valueShort]
      ,[value]
  FROM [SSDBLive].[dbo].[cities];

SET IDENTITY_INSERT [FCWeb].[dbo].[City] OFF;

ALTER TABLE [FCWeb].[dbo].[City] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Stadium] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Person] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Team] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Tourney] CHECK CONSTRAINT ALL