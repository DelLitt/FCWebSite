SET IDENTITY_INSERT [FCWeb].[dbo].[Event] ON;

INSERT INTO [FCWeb].[dbo].[Event]
([Id], [eventGroupId], [Name], [NameFull])
SELECT [eventsExtId]
      ,[eventId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[EventsExt];

SET IDENTITY_INSERT [FCWeb].[dbo].[Event] OFF;
