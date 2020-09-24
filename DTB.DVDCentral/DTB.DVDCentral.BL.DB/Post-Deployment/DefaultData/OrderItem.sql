BEGIN
	INSERT INTO [dbo].tblOrderItem(Id, OrderId, MovieId, Quantity, Cost)
	VALUES 
	(1, 1, 2, 2, 24.00),
	(2, 2, 3, 1, 15.00),
	(3, 2, 1, 1, 10.00),
	(4, 3, 1, 1, 10.00)

END