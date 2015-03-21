CREATE TABLE [dbo].[ClientContact]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Name] NVARCHAR(200) NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [Fax] NCHAR(10) NULL, 
    [Email] NCHAR(10) NULL, 
    [Address] NCHAR(10) NULL, 
    [ClientId] INT NOT NULL, 
    CONSTRAINT [FK_ClientContact_ToTable] FOREIGN KEY ([ClientId]) REFERENCES Client([Id])
)
