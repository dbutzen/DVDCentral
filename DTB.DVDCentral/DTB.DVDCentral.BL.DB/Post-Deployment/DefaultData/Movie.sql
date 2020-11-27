BEGIN
	INSERT INTO [dbo].tblMovie(Id, Title, Description, Cost, RatingId, FormatId, DirectorId, InStkQty, ImagePath)
	VALUES 
	(1, 'Trolls', 'Trolls do the things', 10.00, 2, 1, 1, 4, 'trolls.jpeg'),
	(2, 'Sharknado', 'Sharks n Twisters', 12.00, 3, 1, 2, 2, 'sharknado.jpeg'),
	(3, 'Aladdin', 'I can show you the world...', 15.00, 2, 1, 3, 0, 'aladdin.jpeg')
END