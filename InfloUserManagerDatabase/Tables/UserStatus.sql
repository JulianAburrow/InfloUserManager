CREATE TABLE [dbo].[UserStatus]
(
	StatusId INT NOT NULL IDENTITY (1, 1),
	StatusName NVARCHAR(10)	NOT NULL,
	CONSTRAINT PK_UserStatus PRIMARY KEY (StatusId)
)
