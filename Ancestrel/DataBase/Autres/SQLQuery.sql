select * from Prenom

select * from Prenom_Personne

select * from Personne

SELECT Ordre, Prenom, Id_personne
FROM Prenom_Personne JOIN Prenom ON Prenom_Personne.Id_prenom = Prenom.Id
 