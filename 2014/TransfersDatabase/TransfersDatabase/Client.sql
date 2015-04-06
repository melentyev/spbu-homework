CREATE TABLE [dbo].[Client]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [LegalAddress] NCHAR(50) NULL, 
    [Characteristic] TEXT NULL, 
    [Name] NCHAR(50) NULL
)
