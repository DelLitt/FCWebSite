SET IDENTITY_INSERT [FCWeb].[dbo].[EventGroup] ON;

INSERT INTO [FCWeb].[dbo].[EventGroup]
([Id], [Name], [NameFull])
SELECT [eventId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[Events];

SET IDENTITY_INSERT [FCWeb].[dbo].[EventGroup] OFF;
