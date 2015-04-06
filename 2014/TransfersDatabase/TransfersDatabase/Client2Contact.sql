CREATE TABLE [dbo].[Client2Contact]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [ClientId] INT NOT NULL, 
    [ClientContactId] INT NOT NULL
)
