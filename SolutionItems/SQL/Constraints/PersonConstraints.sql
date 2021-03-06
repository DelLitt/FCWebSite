/*
   Monday, May 9, 201601:05:33 PM
   User: 
   Server: ELENOVOLAPTOP\SQLEXPRESS
   Database: FCWeb
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.City SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.City', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.City', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.City', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Person
	DROP CONSTRAINT FK_Person_Team
GO
ALTER TABLE dbo.Team SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Team', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Team', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Team', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Person
	DROP CONSTRAINT FK_Person_PersonStatus
GO
ALTER TABLE dbo.PersonStatus SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PersonStatus', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PersonStatus', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PersonStatus', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Person
	DROP CONSTRAINT FK_Person_PersonRole
GO
ALTER TABLE dbo.PersonRole SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PersonRole', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PersonRole', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PersonRole', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Person
	(
	Id int NOT NULL IDENTITY (1, 1),
	NameFirst nvarchar(64) NOT NULL,
	NameMiddle nvarchar(64) NULL,
	NameLast nvarchar(64) NOT NULL,
	NameNick nvarchar(64) NULL,
	BirthDate datetime NULL,
	Height tinyint NULL,
	Weight tinyint NULL,
	cityId int NULL,
	teamId int NULL,
	roleId smallint NULL,
	Image nvarchar(1000) NULL,
	Info nvarchar(MAX) NULL,
	Number tinyint NULL,
	personStatusId smallint NULL,
	Active bit NOT NULL,
	CustomIntValue int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Person SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Person ON
GO
IF EXISTS(SELECT * FROM dbo.Person)
	 EXEC('INSERT INTO dbo.Tmp_Person (Id, NameFirst, NameMiddle, NameLast, NameNick, BirthDate, Height, Weight, cityId, teamId, roleId, Image, Info, Number, personStatusId, Active, CustomIntValue)
		SELECT Id, NameFirst, NameMiddle, NameLast, NameNick, BirthDate, Height, Weight, CONVERT(int, cityId), teamId, roleId, Image, Info, Number, personStatusId, Active, CustomIntValue FROM dbo.Person WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Person OFF
GO
ALTER TABLE dbo.Person
	DROP CONSTRAINT FK_Person_Person
GO
ALTER TABLE dbo.PersonCareer
	DROP CONSTRAINT FK_PersonCareer_Person
GO
ALTER TABLE dbo.PersonStatistics
	DROP CONSTRAINT FK_PersonStatistics_Person
GO
DROP TABLE dbo.Person
GO
EXECUTE sp_rename N'dbo.Tmp_Person', N'Person', 'OBJECT' 
GO
ALTER TABLE dbo.Person ADD CONSTRAINT
	PK_Person PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Person WITH NOCHECK ADD CONSTRAINT
	FK_Person_Person FOREIGN KEY
	(
	Id
	) REFERENCES dbo.Person
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Person WITH NOCHECK ADD CONSTRAINT
	FK_Person_PersonRole FOREIGN KEY
	(
	roleId
	) REFERENCES dbo.PersonRole
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Person WITH NOCHECK ADD CONSTRAINT
	FK_Person_PersonStatus FOREIGN KEY
	(
	personStatusId
	) REFERENCES dbo.PersonStatus
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Person WITH NOCHECK ADD CONSTRAINT
	FK_Person_Team FOREIGN KEY
	(
	teamId
	) REFERENCES dbo.Team
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Person WITH NOCHECK ADD CONSTRAINT
	FK_Person_City FOREIGN KEY
	(
	cityId
	) REFERENCES dbo.City
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Person', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Person', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Person', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PersonStatistics WITH NOCHECK ADD CONSTRAINT
	FK_PersonStatistics_Person FOREIGN KEY
	(
	personId
	) REFERENCES dbo.Person
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PersonStatistics SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PersonStatistics', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PersonStatistics', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PersonStatistics', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.PersonCareer WITH NOCHECK ADD CONSTRAINT
	FK_PersonCareer_Person FOREIGN KEY
	(
	personId
	) REFERENCES dbo.Person
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.PersonCareer SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.PersonCareer', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.PersonCareer', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.PersonCareer', 'Object', 'CONTROL') as Contr_Per 