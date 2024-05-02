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


create table Equipes.JoueurRetour(
JoueurID int not null,
Nas Char(9) not null
)
go

alter table Equipes.JoueurRetour add constraint FK_JoueurRetour_JoueurID
foreign key(JoueurID)
references Equipes.Joueur (JoueurID)
go

Create procedure Equipes.USP_DEChiffrement
@NAS char(9),
@JoueurId int,
@AdminKey varchar(20)
as
begin
Open SYMMETRIC KEY MaSuperCle
DECRYPTION BY CERTIFICATE MonCertificat;
select JoueurID, CONVERT(char(9), DECRYPTBYKEY(NAS)) as [Nas]  from Equipes.Joueur
where JoueurID = @JoueurId
CLOSE SYMMETRIC KEY MaSuperCle;



end
go
