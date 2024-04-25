

USE football;
GO

CREATE SCHEMA Championnats;
GO
CREATE SCHEMA Equipes;
GO


-- Table Championnat
CREATE TABLE Championnats.Championnat (
    ChampionnatID INT IDENTITY(1,1) NOT NULL ,
    Nom VARCHAR(100) NOT NULL UNIQUE,
    Niveau VARCHAR(50) NOT NULL,
    Format VARCHAR(50) NOT NULL,
	CONSTRAINT PK_Championnat_championnat PRIMARY KEY (ChampionnatID)
);
GO

-- Table Saison
CREATE TABLE Championnats.Saison (
    SaisonID INT IDENTITY(1,1) NOT NULL,
    Annee INT NOT NULL,
    Duree INT NOT NULL,
    ChampionnatID INT null,
   CONSTRAINT PK_Saison_SaisonID PRIMARY KEY(SaisonID)
);
GO

-- Table Stade
CREATE TABLE Championnats.Stade (
    StadeID INT IDENTITY(1,1) NOT NULL ,
    Nom VARCHAR(100) NOT NULL UNIQUE,
    Capacite INT NOT NULL,
    Ville VARCHAR(100),
    AnneeInauguration INT NOT NULL,
    EquipeID INT NULL,
	CONSTRAINT PK_Stade_SatdeID PRIMARY KEY (StadeID)
);
GO

-- Table Equipe
CREATE TABLE Equipes.Equipe (
    EquipeID INT IDENTITY(1,1) NOT NULL ,
    Nom VARCHAR(100) NOT NULL UNIQUE,
    AnneeDeFondation INT NOT NULL,
    Ville VARCHAR(100) NOT NULL,
    ChampionnatID INT NOT NULL,
	CONSTRAINT PK_Equipe_EquipeID PRIMARY KEY (EquipeID)
);
GO

-- Table Match
CREATE TABLE Equipes.Match (
    MatchID INT IDENTITY(1,1) NOT NULL ,
    Date DATE NOT NULL,
    StadeID INT NULL,
    EquipeDomicile INT NULL,
    EquipeExterieure INT NULL ,
    EquipeGagnante INT NULL ,
    SaisonID INT NULL ,
	CONSTRAINT PK_Match_MatchID PRIMARY KEY (MatchID)
);
GO

-- Table Joueur
CREATE TABLE Equipes.Joueur (
    JoueurID INT IDENTITY(1,1) NOT NULL ,
    Nom VARCHAR(100) NOT NULL UNIQUE,
    DateNaissance VARCHAR(100) NOT NULL,
    Position VARCHAR(50) NOT NULL,
    Nationalite VARCHAR(100),
    EquipeID INT NULL ,
	CONSTRAINT PK_Joueur_JoueurID PRIMARY KEY (JoueurID)
);
GO

-- Table Trophee
CREATE TABLE Equipes.Trophee (
    TropheeID INT IDENTITY(1,1) NOT NULL ,
    Nom VARCHAR(100) NOT NULL UNIQUE,
    Annee INT NOT NULL,
    Categorie VARCHAR(50) NOT NULL,
	CONSTRAINT PK_Trophee_TropheeID PRIMARY KEY (TropheeID)
);
GO
CREATE TABLE Equipes.Palmares(
    PalmaresID INT IDENTITY(1,1) NOT NULL ,
    TropheeID INT NOT NULL,
    JoueurID INT NOT NULL ,
	CONSTRAINT PK_Palmares_PalmaresID PRIMARY KEY (PalmaresID)
);
GO

CREATE TABLE Equipes.ChangementClub(
    ChangementClubID INT IDENTITY(1,1) NOT NULL ,
    Joueur varchar(50) NOT NULL,
 AncienClub VARCHAR(50) NULL,
 NouveauClub VARCHAR(50) NULL,
 DateRecutement datetime NOT NULL,
	CONSTRAINT PK_ChangementClub_ChangementClubID PRIMARY KEY (ChangementClubID)
);
GO


-- Ajout des contraintes de clé étrangère avec ALTER TABLE

-- Contrainte de clé étrangère pour Stade
ALTER TABLE Championnats.Stade
ADD CONSTRAINT FK_Stade_EquipeID FOREIGN KEY (EquipeID) REFERENCES Equipes.Equipe(EquipeID)  ON DELETE Set NULL

GO

-- Contrainte de clé étrangère pour Equipe
ALTER TABLE Equipes.Equipe
ADD CONSTRAINT FK_Equipe_ChampionnatID FOREIGN KEY (ChampionnatID) REFERENCES Championnats.Championnat(ChampionnatID) ON DELETE CASCADE

GO

-- Contraintes de clé étrangère pour Match
ALTER TABLE Equipes.Match
ADD CONSTRAINT FK_Match_StadeID FOREIGN KEY (StadeID) REFERENCES Championnats.Stade(StadeID) ON DELETE Set NULL

GO
Alter Table Equipes.Match
ADD CONSTRAINT FK_Match_EquipeDomicile FOREIGN KEY (EquipeDomicile) REFERENCES Equipes.Equipe(EquipeID) ON DELETE Set NULL

Go
Alter Table Equipes.Match
ADD CONSTRAINT FK_Match_EquipeExterieur FOREIGN KEY (EquipeExterieure) REFERENCES Equipes.Equipe(EquipeID) 

Go


Alter Table Equipes.Match
ADD CONSTRAINT FK_Match_EquipeGagnant FOREIGN KEY (EquipeGagnante) REFERENCES Equipes.Equipe(EquipeID)

Go
Alter Table Equipes.Match
ADD CONSTRAINT FK_Match_SaisonID FOREIGN KEY (SaisonID) REFERENCES Championnats.Saison(SaisonID) ON DELETE Set NULL

Go




-- Contrainte de clé étrangère pour Joueur
ALTER TABLE Equipes.Joueur
ADD CONSTRAINT FK_Joueur_EquipeID FOREIGN KEY (EquipeID) REFERENCES Equipes.Equipe(EquipeID) ON DELETE Set NULL

GO


-- Contrainte de clé étrangère pour Saison
ALTER TABLE Championnats.Saison
ADD CONSTRAINT FK_Saison_ChampionnatID FOREIGN KEY (ChampionnatID) REFERENCES Championnats.Championnat(ChampionnatID) ON DELETE set null

GO

--contrainte de clé étrangège pour palmarès
ALTER TABLE Equipes.Palmares
ADD CONSTRAINT FK_Palmares_JoueurID FOREIGN KEY (JoueurID) REFERENCES Equipes.Joueur(JoueurID)  ON DELETE cascade


GO

ALTER TABLE Equipes.Palmares
ADD CONSTRAINT FK_Palmares_TropheeID FOREIGN KEY (TropheeID) REFERENCES Equipes.Trophee(TropheeID) ON DELETE cascade

GO

-- Contrainte de age  pour joueur
ALTER TABLE 
Equipes.Joueur ADD CONSTRAINT CK_Joueur_DateNaissance CHECK (YEAR(GETDATE())-YEAR(DateNaissance) >= 15)
GO


