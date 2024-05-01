use football
go

create table Equipes.Images(
ImagesID int identity (1,1),
Identifiant uniqueidentifier  Not NULL ROWGUIDCOL,
JoueurID int null,
constraint  PK_Images_ImagesID primary key (ImagesID)

);

alter table Equipes.Images add constraint FK_Images_JoueurID
foreign key (JoueurID) references Equipes.Joueur (JoueurID)
go

alter table Equipes.Images add constraint  UC_Joueur_Identifiant
unique(Identifiant);
go

alter table Equipes.Images add constraint  DF_Joueur_Identifiant
Default newid() for Identifiant;
go

ALTER TABLE Equipes.Images ADD
Photo varbinary(max) FILESTREAM NULL;
GO

update Equipes.Joueur
set Photo = BulkColumn FROM OPENROWSET(
	BULK 'C:\Users\Admin\Downloads\Projet_FinalBD\BD Final Project\joueurs images\mobutu.jpg', SINGLE_BLOB) AS myfile
