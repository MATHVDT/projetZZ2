using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class Bdd : IBddLoader, IBddSaver
    {
        private readonly string _chaineConnexion;
        private readonly PersonneBdd _personneBdd;
        private readonly PrenomBdd _prenomBdd;
        private readonly VilleBdd _villeBdd;
        private readonly ImageBdd _imageBdd;
        private readonly PaysBdd _paysBdd;

        private Dictionary<int, Ville> _villeDejaChargee;
        private Dictionary<int, FichierImage> _imageDejaChargee;

        public Bdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;

            _personneBdd = new PersonneBdd(_chaineConnexion);
            _prenomBdd = new PrenomBdd(_chaineConnexion);
            _villeBdd = new VilleBdd(_chaineConnexion);
            _imageBdd = new ImageBdd(_chaineConnexion);
            _paysBdd = new PaysBdd(_chaineConnexion);

            _villeDejaChargee = new Dictionary<int, Ville>();
            _imageDejaChargee = new Dictionary<int, FichierImage>();


            // Ajout temp
            //Ville villeTest = _villeBdd.GetVilleTableById(1);
            //_villeDejaChargee.Add((int)villeTest.Id, villeTest);
        }

        public FichierImage GetFichierImageById(int idImage)
        {
            FichierImage fichierImage;

            // Checke si l'image a pas déjà été chargée
            if (!_imageDejaChargee.TryGetValue(idImage, out fichierImage))
            {
                // Requete dans la bdd
                fichierImage = _imageBdd.GetImageById(idImage);
            }

            return fichierImage;
        }

        public string? GetNationaliteByIdPays(int idPays)
        {
            return _paysBdd.GetNationaliteTableByIdPays(idPays);
        }

        public Personne GetPersonneById(int idPersonne)
        {
            Personne personne;


            // Recupération de la personne dans la Table Personne
            personne = _personneBdd.GetPersonneTableById(idPersonne);

            if (personne is null)
                return personne;

            // Recupération des prénoms de la personne dans la Table Prenom et la Table d'association
            string? prenomsBdd = _prenomBdd.GetPrenomByIdPersonne(idPersonne);
            if (prenomsBdd is not null)
                personne.AddPrenoms(prenomsBdd);

            // Recupération lieu de naissance dans la Table Ville et la Table d'association
            int? idVilleNaissance = _personneBdd.GetIdVilleNaissancePersonneById(idPersonne);

            Console.WriteLine("id ville : " + idVilleNaissance ?? "null");

            // A une ville de naissance
            if (idVilleNaissance is not null)
            {
                // Récupère la ville de naissance
                Ville villeNaissance = GetVilleById((int)idVilleNaissance);

                // Ajout du lieu de naissance à la personne
                personne.LieuNaissance = villeNaissance;
            }



            // Recupération des images de la personne dans la Table Image et la Table d'association
            List<int> listIdImage = _imageBdd.GetListIdImagePersonneById(idPersonne);

            FichierImage image; bool imgProfil = false;

            // Pour chaque Image, on l'ajoute à la personne
            foreach (int idImageId in listIdImage)
            {
                image = GetFichierImageById(idImageId); // Récupération de l'image

                // Check si ce n'est pas l'image de profil
                //imgProfil = idImage == personne.IdImageProfil; 

                personne.AjouterImage(image, imgProfil);
            }


            // Recupération de la Nationnalité dans la Table d'association
            // ...

            // Récupération des prénoms
            string? prenoms = _prenomBdd.GetPrenomByIdPersonne(idPersonne);
            personne.Prenoms = prenoms;


            return personne;
        }

        public string? GetPrenomById(int id)
        {
            return GetPrenomById(id);
        }

        public Ville GetVilleById(int idVille)
        {
            Ville ville;
            if (!_villeDejaChargee.TryGetValue((int)idVille, out ville))
            {
                ville = _villeBdd.GetVilleTableById(idVille);
                _villeDejaChargee.Add(idVille, ville);
            }
            return ville;
        }

        public void InsererFichierImage(FichierImage fichierImage)
        {
            // Verifier s'il y est deja insere ie si id != null => ModifierFichierImage
            _imageBdd.InsererImageTable(fichierImage);
            _imageDejaChargee.Add((int)fichierImage.Id, fichierImage);
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
            try
            {
                int idPersonne = (int)personne.Id;
                _prenomBdd.InsererAssociationPrenomsPersonneId(listPrenoms, idPersonne);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void InsererVille(Ville ville)
        {
            throw new NotImplementedException();
        }
    }
}
