CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Date] DATETIME NULL, 
    [Name] NVARCHAR(50) NULL, 
    [UnitWeigth] DECIMAL(15, 3) NULL, 
    [UnitVolume] DECIMAL(15, 3) NULL, 
    [ShipId] INT NULL,
	[Scheduled] DATETIME NULL
)
