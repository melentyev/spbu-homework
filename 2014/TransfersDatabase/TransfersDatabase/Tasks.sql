--Примеры с простыми запросами:
-- 1.	Выбрать всю информацию обо всех кораблях.
select * from [Ship]
-- 2.	Выбрать контактных лиц фирмы “IBM”.
select * from [ClientContact] cc where cc.[Name] = 'IBM'
-- 3.	Выбрать номера контейнеров, у которых вес пустого контейнера меньше 300 и упорядочить их по этому весу.
select Id from [Container] cont where cont.EmptyWeight < 300.0 order by cont.EmptyWeight asc
-- 4.	Выбрать номера групп для контейнеров с общим весом груза больше 100 и номера контейнеров, 
--      у которых  номер корабля, на котором они будут отправлены, содержит цифру 5. Список упорядочить по номеру группы.
select [Order].Id, [Container].Id
	from [Container] c, [Order] o, [OrderContainer] oc
	where 
		o.Id = oc.OrderId AND c.Id = oc.ContainerId 
		AND 
		(select sum(oc.CargoWeight) from oc where OrderId = o.Id) > 100.0 
		AND 
		CHARINDEX('5', o.ShipId) > 0
	order by o.Id asc

-- 5.	Выдать время заказа перевозки груза, имена кораблей и дату их отправления, для которых время заказа с 10:10 до 20:00 1 июля 2005 года.
select o.[Date], s.[Name], o.[Scheduled] from [Order] o inner join [Ship] s on o.ShipId = s.Id 
	where
		o.[Date] >= CAST('2005-07-01 10:10:00.000' AS DATETIME) 
		AND o.[Date] <= CAST('2005-07-01 20:00:00.000' AS DATETIME)

--Примеры со сложными запросами:
-- 1.	Посчитать общий вес всех пустых контейнеров в первой группе контейнеров.
select sum(c.EmptyWeight) 
	from [Container] c, [Order] o, [OrderContainer] oc 
	where 
		o.Id = oc.OrderId AND c.Id = oc.ContainerId 
		AND o.Id = 1 AND oc.CargoWeight = 0.0
-- 2.	Получить список контактных лиц, отсортированный по количеству фирм, для которых оно является представителем.
select *
	from [ClientContact] cc
	order by (select count(*) from [Client] c, [Client2Contact] c2c where c2c.ClientContactId = cc.Id AND c2c.ClientId = c.Id)
-- 3.	Выбрать постоянных клиентов корабля Титаник (не менее 2 заказов)
select c.Id 
	from [Client] c 
	where (
		select count(*) 
				from [Order] o 
				where o.ShipId = (select id from [Ship] where Name = 'Титаник')
		) > 2

--Примеры на редактирование:
-- 1.	Удалить контакное лицо - Иванова.
delete from [ClientContact] where [Name] = 'Иванов'
-- 2.	Удалить все заказы, связанные с Титаником.
delete from [Order] where ShipId = (select id from [Ship] where Name = 'Титаник')
-- 3.	Заменить порт приписки кораблей Севастополь на Одессу
update [Ship] set Port = 'Одесса' where Port = 'Севастополь'