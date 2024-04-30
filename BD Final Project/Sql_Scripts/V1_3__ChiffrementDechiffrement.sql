use football

alter table Equipes.Joueur
ADD  NAS varbinary(max)  null
go

Create procedure Equipes.USP_ChangeNasChiffrement
@NAS char(9),
@JoueurId int
as
begin
Open SYMMETRIC KEY MaSuperCle
DECRYPTION BY CERTIFICATE MonCertificat;
Declare @NasCHIFFRE	 varbinary(max) = ENCRYPTBYKEY(KEY_GUID('MaSuperCle'),@NAS);
CLOSE SYMMETRIC KEY MaSuperCle;

update Equipes.Joueur
set NAS = @NasCHIFFRE
where @JoueurId = JoueurID

end
go

--drop procedure Equipes.USP_ChangeNas

Create procedure Equipes.USP_DEChiffrement
@NAS char(9),
@JoueurId int,
@AdminKey varchar(20)
as
begin
Open SYMMETRIC KEY MaSuperCle
DECRYPTION BY CERTIFICATE MonCertificat;
select CONVERT(char(9), DECRYPTBYKEY(NAS))  from Equipes.Equipe
CLOSE SYMMETRIC KEY MaSuperCle;

update Equipes.Joueur
set NAS = @NasCHIFFRE
where @JoueurId = JoueurID

end
go
