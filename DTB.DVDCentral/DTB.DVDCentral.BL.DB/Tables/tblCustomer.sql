CREATE TABLE [dbo].[tblCustomer]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Address] NCHAR(10) NOT NULL, 
    [City] VARCHAR(50) NOT NULL, 
    [State] CHAR(2) NOT NULL, 
    [ZIP] INT NOT NULL, 
    [Phone] VARCHAR(15) NOT NULL, 
    [UserId] INT NOT NULL
)
