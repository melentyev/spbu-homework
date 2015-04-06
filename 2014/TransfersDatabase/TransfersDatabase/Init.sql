delete from dbo.[Container];
delete from dbo.[Order];
delete from dbo.[Client];
delete from dbo.[ClientContact];
delete from dbo.[Client2Contact];
delete from dbo.[OrderContainer];
delete from dbo.[Ship];

DBCC CHECKIDENT ([Container], RESEED, 1);
DBCC CHECKIDENT ([Order], RESEED, 1);
DBCC CHECKIDENT ([Client], RESEED, 1);
DBCC CHECKIDENT ([ClientContact], RESEED, 1);
DBCC CHECKIDENT ([Client2Contact], RESEED, 1);
DBCC CHECKIDENT ([OrderContainer], RESEED, 1);
DBCC CHECKIDENT ([Ship], RESEED, 1);

insert into dbo.Ship ([Name], [Port]) values 
	('Titanik', 'Sevastopol'), ('Potemkin', 'Madagascar')
insert into dbo.[Order] ([Date], [Name], [UnitWeigth], [UnitVolume], [ShipId], [Scheduled]) values
	('2005-07-01 10:11:00.000', 'Order1', 10.0, 10.0, 1, '2005-07-01 10:11:00.000')
insert into dbo.Client ([Name], [LegalAddress], [Characteristic]) values 
	('IBM', 'Sevastopol, 2321, 021', 'adasjdsjdasdjasdasjdjasdnasjdasjdmasdjasjd')
insert into dbo.Contact ([Name], [Phone], [Fax], [Email], [Address]) values 
	('Ivanov', '1111', '1111', '213@sadas.ru', 'asdsad')

/*
insert into dbo.[Student] ([Name], [Phone], [Address]) values 
(N'Иванов', '1234567',	N'Невский,9'),
(N'Петрова',	'2345678',	N'Садовая,21'),
(N'Сидоров',	'3456789',	''),
(N'Алексеев',	'4567890',	N'Гороховая,2')

GO

insert into dbo.[Course] ([Name], [Teacher]) values
(N'Алгебра', N'Фаддеев'),
(N'Геометрия', N'Волков'),
(N'Мат.анализ', N'Хавин')

GO

insert into dbo.[Marks] ([StudentId], [Mark], [CourseId]) values
(1,	5,	1),
(1,	3,	2),
(2,	4,	1),
(3,	4,	3)
*/