CREATE TABLE [dbo].[OrderContainer]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [OrderId] NCHAR(10) NULL, 
    [ContainerId] NCHAR(10) NULL, 
    [CargoWeight] DECIMAL(15, 3) NULL
)
