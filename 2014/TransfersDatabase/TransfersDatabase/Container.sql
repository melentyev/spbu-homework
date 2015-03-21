CREATE TABLE [dbo].[Container]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Type] NVARCHAR(50) NULL, 
    [EmptyWeight] DECIMAL(15, 3) NULL
)
