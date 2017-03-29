--TRUNCATE TABLE [FCWeb].[dbo].[PersonRoleGroup];

-- for small data amout
ALTER TABLE [FCWeb].[dbo].[PersonRoleGroup] NOCHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonRole] NOCHECK CONSTRAINT ALL

DELETE FROM [FCWeb].[dbo].[PersonRoleGroup];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonRoleGroup] ON;

INSERT INTO [FCWeb].[dbo].[PersonRoleGroup]
([Id], [Name], [NameFull])
SELECT [rolesId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[Roles];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonRoleGroup] OFF;

ALTER TABLE [FCWeb].[dbo].[PersonRoleGroup] CHECK CONSTRAINT ALL
ALTER TABLE [FCWeb].[dbo].[PersonRole] CHECK CONSTRAINT ALL