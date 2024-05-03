use football
go

create nonclustered index IX_Joueur_NomEquipe on Equipes.Joueur(Nom,EquipeID)

create nonclustered index IX_Joueur_PalmaresID_TropheeID on Equipes.Palmares(JoueurID,TropheeID)