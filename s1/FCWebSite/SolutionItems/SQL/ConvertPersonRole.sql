--TRUNCATE TABLE [FCWeb].[dbo].[PersonRole];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[PersonRole] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Person] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[PersonRole];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonRole] ON;

INSERT INTO [FCWeb].[dbo].[PersonRole]
([Id], [personRoleGroupId], [Name], [NameFull])
SELECT [roleExtId]
      ,[roleId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[RolesExt];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonRole] OFF;

ALTER TABLE [FCWeb].[dbo].[PersonRole] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[Person] CHECK CONSTRAINT ALL