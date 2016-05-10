SET IDENTITY_INSERT [FCWeb].[dbo].[Stadium] ON;

INSERT INTO [FCWeb].[dbo].[Stadium]
([Id], [cityId] ,[Name], [NameFull], [Capacity], [Address], [Description], [Image])
SELECT [stadiumId]
      ,[cityId]
      ,[valueShort]
      ,[value]
      ,[capacity]
      ,[adress]
      ,[info]
      ,[photoPath]
  FROM [SSDBLive].[dbo].[Stadiums];

SET IDENTITY_INSERT [FCWeb].[dbo].[Stadium] OFF;
