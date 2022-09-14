CREATE TABLE [dbo].UserTable
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	FirstName varchar(50) NOT NULL, 
	LastName varchar(50) NOT NULL, 
	EmailAddress varchar(100) NOT NULL, 
	Password varchar(20) NOT NULL
)
