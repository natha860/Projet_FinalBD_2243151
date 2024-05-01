use football
go
create procedure Equipes.usp_stadeAppartennance(@stadeID int)
as 
begin
select E.Nom as 'Equipe propriétaire',S.Nom as stade, S.Capacite
from Championnats.Stade S
right join Equipes.Equipe E
on S.EquipeID = E.EquipeID
where @stadeID =  S.StadeID
end
go
