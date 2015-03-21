delete from dbo.[Container];
delete from dbo.[Order];
delete from dbo.[Client];
delete from dbo.[ClientContact];

DBCC CHECKIDENT ([Container], RESEED, 1);
DBCC CHECKIDENT ([Order], RESEED, 1);
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