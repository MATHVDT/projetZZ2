select * from Personne join Nationalite on Personne.Id = Nationalite.Id_personne join Pays on Pays.Id = Nationalite.Id_pays
where Personne.Id = 10;
