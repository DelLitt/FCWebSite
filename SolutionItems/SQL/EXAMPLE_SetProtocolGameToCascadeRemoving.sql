/*
   Saturday, August 13, 201606:11:35 PM
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
ALTER TABLE dbo.ProtocolRecord
	DROP CONSTRAINT FK_ProtocolRecord_Game
GO
ALTER TABLE dbo.Game SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Game', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Game', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Game', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.ProtocolRecord WITH NOCHECK ADD CONSTRAINT
	FK_ProtocolRecord_Game FOREIGN KEY
	(
	gameId
	) REFERENCES dbo.Game
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.ProtocolRecord SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ProtocolRecord', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ProtocolRecord', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ProtocolRecord', 'Object', 'CONTROL') as Contr_Per 