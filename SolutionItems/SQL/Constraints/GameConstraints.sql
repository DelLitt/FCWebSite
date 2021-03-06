/*
   Monday, May 9, 201612:56:05 PM
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
ALTER TABLE dbo.ImageGallery SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ImageGallery', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ImageGallery', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ImageGallery', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Video SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Video', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Video', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Video', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Round SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Round', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Round', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Round', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Stadium SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Stadium', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Stadium', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Stadium', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Team SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Team', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Team', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Team', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Game WITH NOCHECK ADD CONSTRAINT
	FK_Game_Team_Home FOREIGN KEY
	(
	homeId
	) REFERENCES dbo.Team
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Game WITH NOCHECK ADD CONSTRAINT
	FK_Game_Team_Away FOREIGN KEY
	(
	awayId
	) REFERENCES dbo.Team
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Game WITH NOCHECK ADD CONSTRAINT
	FK_Game_Stadium FOREIGN KEY
	(
	stadiumId
	) REFERENCES dbo.Stadium
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Game WITH NOCHECK ADD CONSTRAINT
	FK_Game_Round FOREIGN KEY
	(
	roundId
	) REFERENCES dbo.Round
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Game WITH NOCHECK ADD CONSTRAINT
	FK_Game_Video FOREIGN KEY
	(
	videoId
	) REFERENCES dbo.Video
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Game WITH NOCHECK ADD CONSTRAINT
	FK_Game_ImageGallery FOREIGN KEY
	(
	imageGalleryId
	) REFERENCES dbo.ImageGallery
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Game SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Game', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Game', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Game', 'Object', 'CONTROL') as Contr_Per 