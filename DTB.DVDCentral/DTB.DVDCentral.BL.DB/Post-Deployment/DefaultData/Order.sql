BEGIN
	INSERT INTO [dbo].tblOrder(Id, CustomerId, OrderDate, UserId, ShipDate)
	VALUES 
	(1, 1, '2020-6-7', 1, '2020-6-9'),
	(2, 1, '2020-6-10', 1, '2020-6-12'),
	(3, 3, '2020-6-10', 3, '2020-6-11')
END