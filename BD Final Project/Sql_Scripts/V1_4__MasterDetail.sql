use football
go
create procedure Equipes.usp_stadeAppartennance(@nomEquipe nvarchar(50))
as 
begin
select E.Nom as Equipe, J.Nom as Joueur,c.Nom as Championnat,c.Niveau,c.ChampionnatID,j.JoueurID,E.EquipeID ,E.AnneeDeFondation, E.Ville
from Equipes.Equipe E
left join  Equipes.Joueur J
on e.EquipeID = j.EquipeID
left join Championnats.Championnat C
on e.ChampionnatID = C.ChampionnatID
where @nomEquipe =  E.Nom
end
go
