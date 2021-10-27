/* 
 * SQL Server Script
 * 
 * This script can be directly executed to configure the test database from
 * PCs located at CECAFI Lab. The database and the corresponding users are 
 * already created in the sql server, so it will create the tables needed 
 * in the samples. 
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *      Configure within the CREATE DATABASE sql-sentence the path where 
 *      database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 
USE [photogram]


/* ********** Drop Table UserProfile if already exists *********** */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Like_table]') AND type in ('U'))
DROP TABLE [Like_table]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Publication]') AND type in ('U'))
DROP TABLE [Publication]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[ImageUpload]') AND type in ('U'))
DROP TABLE [ImageUpload]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]



GO


/*
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */

CREATE TABLE UserProfile (
	usrId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2),
	country varchar(2),

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (usrId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName),
	CONSTRAINT [FK_Usr_Follow] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId)
)


CREATE TABLE ImageUpload (
	imgId bigint IDENTITY(1,1) NOT NULL,
	title varchar(30) NOT NULL,
	description varchar(50) NOT NULL,
	uploadDate Date NOT NULL,
	f float,
	t float,
	wb varchar(50),
	category varchar(50) NOT NULL,


	CONSTRAINT [PK_Image] PRIMARY KEY (imgId)
)

CREATE TABLE Publication (
	pubId bigint IDENTITY(1,1) NOT NULL,
	imgId bigint NOT NULL,
	usrId bigint NOT NULL,
	likes bigint NOT NULL,
	
	CONSTRAINT [PK_Publication] PRIMARY KEY (pubId),
	CONSTRAINT [FK_Image] FOREIGN KEY (imgId) REFERENCES ImageUpload(imgId),
	CONSTRAINT [FK_Usr] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId)
	
)

CREATE TABLE Comment (
	commentId bigint IDENTITY(1,1) NOT NULL,
	content varchar(100) NOT NULL,
	usrId bigint NOT NULL,
	pubId bigint NOT NULL,
	
	CONSTRAINT [PK_Comment] PRIMARY KEY (commentId),
	CONSTRAINT [FK_Pub] FOREIGN KEY (pubId) REFERENCES Publication(pubId),
	CONSTRAINT [FK_User_Comment] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId)
	
)

CREATE TABLE Like_table (
	usrId bigint NOT NULL,
	pubId bigint NOT NULL,
	
	CONSTRAINT [PK_Like_table] PRIMARY KEY (usrId,pubId),
	CONSTRAINT [FK_Pub_like_table] FOREIGN KEY (pubId) REFERENCES Publication(pubId),
	CONSTRAINT [FK_User_Like] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId)
	
)

CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Table UserProfile created.'
GO

PRINT N'Table Image created.'
GO

PRINT N'Table Publication created.'
GO

PRINT N'Table Comment created.'
GO

PRINT N'Table Like_Table created.'
GO

GO
