﻿CREATE TABLE [dbo].[Ship]
(
	[Id] INT NOT NULL IDENTITY (1,1) PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Port] NVARCHAR(50) NULL
)