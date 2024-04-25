USE master 
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='football')
BEGIN
    DROP DATABASE football
END
CREATE DATABASE football
go

USE football
GO

	

	EXEC sp_configure filestream_access_level, 2 RECONFIGURE
	ALTER DATABASE football
	ADD FILEGROUP FG_Images CONTAINS FILESTREAM;
	GO
	ALTER DATABASE football
	ADD FILE (
			NAME = FG_Images,
			FILENAME = 'C:\EspaceLabo\FG_Images'
			)
			TO FILEGROUP FG_Images
			go


			CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'P4ssw0rd!009CertForever';
	go
	

	CREATE CERTIFICATE MonCertificat WITH SUBJECT ='ChiffrementNAS';
	go
	

	CREATE SYMMETRIC KEY MaSuperCle WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE MonCertificat;
	go