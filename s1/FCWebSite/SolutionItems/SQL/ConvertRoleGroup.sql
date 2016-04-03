SET IDENTITY_INSERT [FCWeb].[dbo].[PersonRoleGroup] ON;

INSERT INTO [FCWeb].[dbo].[PersonRoleGroup]
([Id], [Name], [NameFull])
SELECT [rolesId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[Roles];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonRoleGroup] OFF;
