SET IDENTITY_INSERT [FCWeb].[dbo].[PersonRole] ON;

INSERT INTO [FCWeb].[dbo].[PersonRole]
([Id], [personRoleGroupId], [Name], [NameFull])
SELECT [roleExtId]
      ,[roleId]
      ,[value]
      ,[valueShort]
  FROM [SSDBLive].[dbo].[RolesExt];

SET IDENTITY_INSERT [FCWeb].[dbo].[PersonRole] OFF;