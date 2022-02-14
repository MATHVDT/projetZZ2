using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class Bdd : BddLoader, BddSaver
    {
        private readonly string _chaineConnexion;
        private readonly PersonneBdd _personneBdd;
        private readonly PrenomBdd _prenomBdd;
        private readonly VilleBdd _villeBdd;

        private Dictionary<int, Ville> _villeDejaChargee;

        public Bdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;

            _personneBdd = new PersonneBdd(_chaineConnexion);
            _prenomBdd = new PrenomBdd(_chaineConnexion);
            _villeBdd = new VilleBdd(_chaineConnexion);

            _villeDejaChargee = new Dictionary<int, Ville>();
        }

        public FichierImage GetFichierImageById(int id)
        {
            throw new NotImplementedException();
        }

        public Personne GetPersonneById(int idPersonne)
        {
            Personne personne;


            // Recupération de la personne dans la Table Personne
            personne = _personneBdd.GetPersonneTableById(idPersonne);

            if (personne is null)
                return personne;

            // Recupération des prénoms de la personne dans la Table Prenom et la Table d'association
            string? prenomsBdd = _prenomBdd.GetPrenomById(idPersonne);
            if (prenomsBdd is not null)
                personne.AddPrenoms(prenomsBdd);

            // Recupération lieu de naissance dans la Table Ville et la Table d'association


            // Recupération de la description de la personne dans la Table Description  et la Table d'association
            // .... Demander si on a besoin de table association ou si on peut mettre PK de la Table = id personne => FK

            // Recupération des images de la personne dans la Table Image et la Table d'association

            // Recupération de la Nationnalité dans la Table d'association
            // Récupération des prénoms


            return personne;
        }

        public string? GetPrenomById(int id)
        {
            return GetPrenomById(id);
        }

        public Ville GetVilleById(int idVille)
        {
            return _villeBdd.GetVilleTableById(idVille);
        }

        public void InsererFichierImage(FichierImage fichierImage)
        {
            throw new NotImplementedException();
        }


        public void InsererPersonne(Personne personne)
        {
            // Insertion de la personne dans la Table Personne
            _personneBdd.InsererPersonneTable(personne);

            // Insertion des prénoms de la personne dans la Table Prenom et la Table d'association
            InsererPrenomsPersonne(personne);

            // Insertion lieu de naissance dans la Table Ville et la Table d'association

            // Insertion de la description de la personne dans la Table Description  et la Table d'association
            // .... Demander si on a besoin de table association ou si on peut mettre PK de la Table = id personne => FK

            // Insertion des images de la personne dans la Table Image et la Table d'association

            // Insertion de la Nationnalité dans la Table d'association
        }

        public void InsererPrenomsPersonne(Personne personne)
        {
            List<string> listPrenoms = personne.GetListPrenoms();
            // La personne n'a pas encore été inséré dans la table
            if (personne.Id is null)
            {
                Console.WriteLine("Personne pas encore ajouté à la bdd.");
                _personneBdd.InsererPersonneTable(personne);
            }
            int idPersonne = (int)personne.Id;

            _prenomBdd.InsererAssociationPrenomsPersonneId(listPrenoms, idPersonne);
        }

        public void InsererVille(Ville ville)
        {
            throw new NotImplementedException();
        }
    }
}
