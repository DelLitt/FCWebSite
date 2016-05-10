SET IDENTITY_INSERT [FCWeb].[dbo].[ProtocolRecord] ON;

INSERT INTO [FCWeb].[dbo].[ProtocolRecord]
([Id], [gameId], [Minute], [eventId], [teamId], [personId], [CustomIntValue])
SELECT [protocolId]
      ,[gameId]
      ,[minute]
      ,[eventExtId]
      ,[teamId]
      ,[personId]
      ,[additionalValue]
  FROM [SSDBLive].[dbo].[ProtocolRecords];

SET IDENTITY_INSERT [FCWeb].[dbo].[ProtocolRecord] OFF;