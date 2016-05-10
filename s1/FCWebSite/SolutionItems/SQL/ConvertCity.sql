SET IDENTITY_INSERT [FCWeb].[dbo].[City] ON;

INSERT INTO [FCWeb].[dbo].[City]
([Id], [countryId], [Name], [NameFull])
SELECT [cityId]
      ,[countryId]
	  ,[valueShort]
      ,[value]
  FROM [SSDBLive].[dbo].[cities];

SET IDENTITY_INSERT [FCWeb].[dbo].[City] OFF;