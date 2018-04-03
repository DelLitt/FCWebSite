--TRUNCATE TABLE [FCWeb].[dbo].[EventGroup];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[EventGroup] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Event] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[EventGroup];

SET IDENTITY_INSERT [FCWeb].[dbo].[EventGroup] ON;

INSERT INTO [FCWeb].[dbo].[EventGroup]
([Id], [Name], [NameFull])
SELECT [eventId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[Events];

SET IDENTITY_INSERT [FCWeb].[dbo].[EventGroup] OFF;

ALTER TABLE [FCWeb].[dbo].[EventGroup] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Event] CHECK CONSTRAINT ALL
