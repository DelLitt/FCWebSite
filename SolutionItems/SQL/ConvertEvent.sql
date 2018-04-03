--TRUNCATE TABLE [FCWeb].[dbo].[EventGroup];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[Event] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[EventGroup] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[ProtocolRecord] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[Event];

SET IDENTITY_INSERT [FCWeb].[dbo].[Event] ON;

INSERT INTO [FCWeb].[dbo].[Event]
([Id], [eventGroupId], [Name], [NameFull])
SELECT [eventsExtId]
      ,[eventId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[EventsExt];

SET IDENTITY_INSERT [FCWeb].[dbo].[Event] OFF;

ALTER TABLE [FCWeb].[dbo].[Event] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[EventGroup] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[ProtocolRecord] CHECK CONSTRAINT ALL
