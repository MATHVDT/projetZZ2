using Model;

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
            //dico personne, image, ville
        {
            _chaineConnexion = chaineConnexion;

            _personneBdd = new PersonneBdd(_chaineConnexion);
            _prenomBdd = new PrenomBdd(_chaineConnexion);
            _villeBdd = new VilleBdd(_chaineConnexion);
            _imageBdd = new ImageBdd(_chaineConnexion);
            _paysBdd = new PaysBdd(_chaineConnexion);

            _villeDejaChargee = new Dictionary<int, Ville>();
            _imageDejaChargee = new Dictionary<int, FichierImage>();



        }

        public void AjouterRelationParente(int idEnfant, int? idPere, int? idMere)
        {
            _personneBdd.AjouterRelationParente(idEnfant, idPere, idMere);
        }

        public Arbre ChargerArbre(int idPersonne)
        {
            // Récupération du cujus
            Personne cujus = GetPersonneById(idPersonne);
            if (cujus == null)
                throw new ArgumentNullException($"Cujus {idPersonne} pas dans la bdd"); // Créer une exception pas de cujus

            // Création de l'arbre
            Arbre arbre = new Arbre("\nArbre Chargé depuis la bdd",
                                    $"Abre à partir de {cujus.Id} : {cujus.Nom} {cujus.Prenoms}",
                                    cujus);
            // Création de la file de cujus dont les parents sont à charger
            Queue<Personne> queue = new Queue<Personne>();
            // Ajout du cujus
            queue.Enqueue(cujus);

            Personne enfant;
            // Id des parents à récupérer
            int? idPere; Personne pere;
            int? idMere; Personne mere;

            while (queue.Count > 0)
            {
                // Récupere une enfant dans la file
                enfant = queue.Dequeue();

                idPere = enfant.IdPere;
                if (idPere is not null)
                {
                    pere = GetPersonneById((int)idPere);
                    arbre.AjouterPere(enfant.Numero, (Homme)pere);
                    //ajout au dico+
                    queue.Enqueue(pere);
                }

                idMere = enfant.IdMere;
                if (idMere is not null)
                {
                    mere = GetPersonneById((int)idMere);
                    arbre.AjouterMere(enfant.Numero, (Femme)mere);
                    queue.Enqueue(mere);
                }
                //Console.WriteLine($"idPere {idPere} et idMere {idMere}");
            }
            return arbre;
        }

        public FichierImage GetFichierImageById(int idFichierImage)
        {
            FichierImage fichierImage;

            // Checke si l'image a pas déjà été chargée
            if (!_imageDejaChargee.TryGetValue(idFichierImage, out fichierImage))
            {
                // Requete dans la bdd
                fichierImage = _imageBdd.GetImageById(idFichierImage);
            }
            if (fichierImage is null)
                throw new ArgumentNullException($"FichierImage {idFichierImage} idnon trouvé dans la BDD.");

            return fichierImage;
        }

        public string GetNationaliteByIdPays(int idPays)
        {
            string? nationalite = _paysBdd.GetNationaliteTableByIdPays(idPays);
            if (nationalite is null)
                throw new ArgumentNullException($"Nationalite pour pays {idPays} non trouvée dans BDD.");
            return (string)nationalite;
        }

        public string? GetNationalitesByIdPersonne(int idPersonne)
        {
            return _paysBdd.GetNationalitesByIdPersonne(idPersonne);
        }

        public Personne GetPersonneById(int idPersonne)
        {
            Personne personne;

            // Recupération de la personne dans la Table Personne
            personne = _personneBdd.GetPersonneTableById(idPersonne);

            if (personne is null)
                throw new ArgumentNullException($"Personne {idPersonne} pas dans la BDD.");

            // Recupération des prénoms de la personne dans la Table Prenom et la Table d'association
            string? prenomsBdd = GetPrenomByIdPersonne(idPersonne);
            if (prenomsBdd is not null)
                personne.AddPrenoms(prenomsBdd);

            // Recupération lieu de naissance dans la Table Ville et la Table d'association
            int? idVilleNaissance = _personneBdd.GetIdVilleNaissancePersonneById(idPersonne);

            Console.WriteLine("Id ville : " + idVilleNaissance ?? "null");

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
            int? idImageProfilBdd = _personneBdd.GetIdImageProfilPersonneById(idPersonne);

            FichierImage image; bool imgProfil = false;

            // Pour chaque Image, on l'ajoute à la personne
            foreach (int idImageId in listIdImage)
            {
                image = GetFichierImageById(idImageId); // Récupération de l'image

                // Check si ce n'est pas l'image de profil
                imgProfil = (idImageId == idImageProfilBdd);

                personne.AjouterImage(image, imgProfil);
            }

            // Recupération de la Nationnalité dans la Table d'association
            personne.Nationalite = GetNationalitesByIdPersonne(idPersonne);

            // Récupération des prénoms
            string? prenoms = _prenomBdd.GetPrenomByIdPersonne(idPersonne);
            personne.Prenoms = prenoms;


            return personne;
        }

        public string? GetPrenomByIdPersonne(int idPersonne)
        {
            return _prenomBdd.GetPrenomByIdPersonne(idPersonne);
        }

        public Ville GetVilleById(int idVille)
        {
            Ville ville;
            if (!_villeDejaChargee.TryGetValue((int)idVille, out ville))
            {
                ville = _villeBdd.GetVilleTableById(idVille);
                _villeDejaChargee.Add(idVille, ville);
            }
            if (ville is null)
                throw new ArgumentNullException($"Ville {idVille} non trouvée dans la BDD");

            return ville;
        }




        public void InsererArbre(Arbre arbre)
        {
            //Console.WriteLine("Enregistrement personne");

            // Inserer les personnes une à une => elles obtiennent un id
            foreach (Personne p in arbre.Personnes.Values)
            {
                InsererPersonne(p);
            }

            //Console.WriteLine("\n Enregistrement relation parente");
            // Enregistrement des relations parents/enfant
            int? idPere; int? idMere;
            Personne pere;
            Personne mere;
            foreach (Personne p in arbre.Personnes.Values)
            {
                if (p.Id is null)
                    throw new ArgumentNullException("La personne n'a pas d'id");
                arbre.Personnes.TryGetValue(p.GetPereNumero(), out pere);
                arbre.Personnes.TryGetValue(p.GetMereNumero(), out mere);

                idPere = pere?.Id;
                idMere = mere?.Id;

                //Console.WriteLine($"idEnfant {p.Id}, idPere {idPere}, idMere {idMere}");
                AjouterRelationParente((int)p.Id, idPere, idMere);
            }
        }




        public void InsererFichierImage(FichierImage fichierImage)
        {
            // Verifier s'il y est deja insere ie si id != null => ModifierFichierImage
            _imageBdd.InsererImageTable(fichierImage);
            _imageDejaChargee.Add((int)fichierImage.Id, fichierImage);
        }


        public void InsererPersonne(Personne personne)
        {
            //if (personne.Id is not null)
            //    modiferPersonn;
            //        return;

            // Insertion ville
            if (personne.LieuNaissance is not null)
                InsererVille(personne.LieuNaissance);

            // Insertion des images de la personne dans la Table Image et la Table d'association
            List<FichierImage> fichierImages = personne.GetFichierImages();
            foreach (var x in fichierImages)
            {
                InsererFichierImage(x);
            }

            // Insertion de la personne dans la Table Personne
            _personneBdd.InsererPersonneTable(personne);

            // Association image personne 
            // ....

            // Insertion des prénoms de la personne dans la Table Prenom et la Table d'association
            if (personne.Prenoms is not null)
                InsererPrenomsPersonne(personne);

            // Insertion de la Nationnalité dans la Table d'association
            if (personne.Nationalite is not null)
                _paysBdd.InsererNationalitePersonne(personne);


            // inserer lier  fils parent
            //dico perso tryget ...

        }

        public void InsererPrenomsPersonne(Personne personne)
        {
            List<string> listPrenoms = personne.GetListPrenoms();
            if (listPrenoms.Count == 0)
                return;

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
            // Verifier si la ville existe pas déjà => id
            //if()
            _villeBdd.InsererVilleTable(ville);
        }
    }
}
