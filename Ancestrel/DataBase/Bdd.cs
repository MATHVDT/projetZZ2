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
        private PersonneBdd _personneBdd;
        private PrenomBdd _prenomBdd;

        public Bdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;

            _personneBdd = new PersonneBdd(_chaineConnexion);
            _prenomBdd = new PrenomBdd(_chaineConnexion);
        }

        public FichierImage GetFichierImageById(int id)
        {
            throw new NotImplementedException();
        }

        public Personne GetPersonneById(int id)
        {
            return GetPersonneById(id);
        }

        public string? GetPrenomById(int id)
        {
            return GetPrenomById(id);
        }

        public Ville GetVilleById(int id)
        {
            throw new NotImplementedException();
        }

        public void InsererFichierImage(FichierImage fichierImage)
        {
            throw new NotImplementedException();
        }

        public void InsererPersonne(Personne personne)
        {
            _personneBdd.InsererPersonne(personne);
        }

        public void InsererPrenomsPersonne(Personne personne)
        {
            List<string> listPrenoms = personne.GetListPrenoms();
            if (personne.Id is null)
            {
                Console.WriteLine("Personne pas encore ajouté à la bdd.");
                _personneBdd.InsererPersonne(personne);
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
