SET IDENTITY_INSERT [FCWeb].[dbo].[Country] ON;

INSERT INTO [FCWeb].[dbo].[Country]
([Id], [Name], [NameFull])
SELECT [countriesId]
      ,[valueShort]
	  ,[value]
  FROM [SSDBLive].[dbo].[countries];

SET IDENTITY_INSERT [FCWeb].[dbo].[Country] OFF;