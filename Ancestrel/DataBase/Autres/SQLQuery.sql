select Personne.Id as id_personne , Personne.Nom_usage, Prenom.Prenom, Ordre from Personne 
JOIN Prenom_Personne  on  Personne.Id = Prenom_Personne.Id_personne 
JOIN prenom  ON Prenom.Id = Prenom_Personne.Id_prenom

DELETE FROM Prenom_Personne
WHERE Id_personne = 1;

select * from Prenom