BEGIN
	INSERT INTO [dbo].tblUserDVD(Id, FirstName, LastName, UserId, Password)
	VALUES 
	(1, 'John', 'Wayne', 'theoutlaw', 'imthegoodone'),
	(2, 'Bruce', 'Wayne', 'imnotbatman', 'imbatman'),
	(3, 'Stacy', 'Wayne', 'stwayne', 'hashtag123')
END