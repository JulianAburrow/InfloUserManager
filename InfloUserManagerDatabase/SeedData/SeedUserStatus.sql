IF NOT EXISTS (SELECT 1 FROM UserStatus)
BEGIN
	INSERT INTO UserStatus
		( StatusName )
	VALUES
		( 'Active' ),
		( 'Inactive' )
END