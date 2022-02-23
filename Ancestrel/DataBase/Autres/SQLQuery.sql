select Personne.Id, Nom_usage, Nom, Prenom from Personne
inner join Prenom_Personne on Personne.Id = Prenom_Personne.Id_personne
inner join Prenom on Prenom.Id = Prenom_Personne.Id_prenom
order by Personne.Id