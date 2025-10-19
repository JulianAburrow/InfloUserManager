CREATE TABLE [dbo].[User]
(
	UserId INT NOT NULL IDENTITY (1, 1),
	Forename NVARCHAR(50) NOT NULL,
	Surname NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NULL,
    DateOfBirth DATE NOT NULL,
    StatusId INT NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (UserId),
    FOREIGN KEY (StatusId) REFERENCES UserStatus(StatusId)
)
