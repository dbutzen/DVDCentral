BEGIN
	INSERT INTO [dbo].tblCustomer (Id, FirstName, LastName, Address, City, State, ZIP, Phone, UserId)
	VALUES 
	(1, 'John', 'Wayne', 'Here', 'Appleton', 'WI', 54914, '920-555-5555', 1),
	(2, 'Bruce', 'Wayne', 'There', 'Appleton', 'WI', 54914, '920-555-4444', 2),
	(3, 'Stacy', 'Wayne', 'Everywhere', 'Appleton', 'WI', 54914, '920-555-3333', 3)
END