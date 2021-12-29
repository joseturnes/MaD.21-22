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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[ImageTags]') AND type in ('U'))
DROP TABLE [ImageTags]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]') AND type in ('U'))
DROP TABLE [Tag]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Follow_table]') AND type in ('U'))
DROP TABLE [Follow_table]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Like_table]') AND type in ('U'))
DROP TABLE [Like_table]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') AND type in ('U'))
DROP TABLE [Comment]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[ImageUpload]') AND type in ('U'))
DROP TABLE [ImageUpload]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') AND type in ('U'))
DROP TABLE [Category]
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
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)


Create Table Category(
	categoryId bigint NOT NULL,
	categoryName varchar(30) NOT NULL,
	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId)
)

CREATE TABLE ImageUpload (
	imgId bigint IDENTITY(1,1) NOT NULL,
	uploadedImage image NOT NULL,
	usrId bigint NOT NULL,
	likes bigint NOT NULL,
	title varchar(30) NOT NULL,
	descriptions varchar(50), 
	uploadDate date NOT NULL,
	f float,
	t float,
	iso varchar(20),
	wb varchar(50),
	categoryId bigint,

	CONSTRAINT [PK_Image] PRIMARY KEY (imgId),
	CONSTRAINT [FK_Usr] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId),
	CONSTRAINT [FK_Category] FOREIGN KEY (categoryId) REFERENCES Category(categoryId)
)



CREATE TABLE Comment (
	commentId bigint IDENTITY(1,1) NOT NULL,
	content varchar(100) NOT NULL,
	usrId bigint NOT NULL,
	imgId bigint NOT NULL,
	comDate date NOT NULL,
	
	CONSTRAINT [PK_Comment] PRIMARY KEY (commentId),
	CONSTRAINT [FK_Image] FOREIGN KEY (imgId) REFERENCES ImageUpload(imgId) ON DELETE CASCADE,
	CONSTRAINT [FK_User_Comment] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId)
	
)

CREATE TABLE Like_table (
	usrId bigint NOT NULL,
	imgId bigint NOT NULL,
	
	CONSTRAINT [PK_Like_table] PRIMARY KEY (usrId,imgId),
	CONSTRAINT [FK_Pub_like_table] FOREIGN KEY (imgId) REFERENCES ImageUpload(imgId),
	CONSTRAINT [FK_User_Like] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId)
	
)

CREATE TABLE Follow_table (
	usrId bigint NOT NULL,
	usrFollows bigint NOT NULL,
	
	CONSTRAINT [PK_Follow_table] PRIMARY KEY (usrId,usrFollows),
	CONSTRAINT [FK_User_Followed] FOREIGN KEY (usrFollows) REFERENCES UserProfile(usrId),
	CONSTRAINT [FK_User] FOREIGN KEY (usrId) REFERENCES UserProfile(usrId)
	
)

CREATE TABLE Tag(
	tagId bigint IDENTITY(1,1) NOT NULL,
	tagname varchar(30) NOT NULL,
	timesUsed bigInt NOT NULL,
	CONSTRAINT [PK_TagTable] PRIMARY KEY (tagId)
)

CREATE TABLE ImageTags(
	tagId bigint NOT NULL,
	imgId bigint NOT NULL,
	CONSTRAINT [PK_ImageTags] PRIMARY KEY (tagId,imgId),
	CONSTRAINT [FK_Tag_ImageTags] FOREIGN KEY (tagId) REFERENCES Tag(tagId),
	CONSTRAINT [FK_Image_ImageTags] FOREIGN KEY (imgId) REFERENCES ImageUpload(imgId)
)

INSERT INTO Category (categoryId,categoryName) VALUES (1,'Retrato')
INSERT INTO Category (categoryId,categoryName) VALUES (2,'Paisaje Nocturno')
INSERT INTO Category (categoryId,categoryName) VALUES (3,'Paisaje')
INSERT INTO Category (categoryId,categoryName) VALUES (4,'Ciudades')



CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Table UserProfile created.'
GO

PRINT N'Table ImageUpload created.'
GO

PRINT N'Table Comment created.'
GO

PRINT N'Table Like_Table created.'
GO

PRINT N'Table Follow_Table created.'
GO

PRINT N'Table Tag created.'
GO

PRINT N'Table ImageTags created.'
GO

PRINT N'Table Category created.'
GO

GO