/**
 * @file Personne.cs
 * Fichier contenant la classe Personne
 * @author Mathieu
 * @date 28/12/2021
 * @copyright ...
 */

using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * @namespace Model
 * Espace de nom des classes de l'application
 */
namespace Model
{
    /**
     * @class Personne
     * @brief classe abstraite d'une personne
     */
    public abstract class Personne
    {
        private string? _nom;
        private List<string> _listePrenom = new List<string>();
        private DateOnly? _dateNaissance;
        private DateOnly? _dateDeces;
        private Ville? _lieuNaissance;
        private string? _nationalite;

        private List<Fichier> _listeFichiers = new List<Fichier> { };
        private int? _indexImageProfil;

        /**
         * @var Id
         * @brief Id de la personne dans la bdd.
         * @details 
         * Id de la personne dans la BDD.
         * Est nulle si l'on vient de créer la personne dans l'application et qu'elle 
         * et qu'elle n'existe pas dans la BDD.
         */
        public int? Id { get; set; }


        /**
         * @var IdPere
         * @brief Id du père dans la BDD.
         */
        public int? IdPere { get; set; }

        /**
         * @var IdMere
         * @brief Id du mère dans la BDD.
         */
        public int? IdMere { get; set; }

        /**
         * @var Numero
         * @brief Numéro unique de la personne dans l'arbre.
         * @details
         * Identidiant unique de la personne dans l'arbre généalogique. 
         * Le père de la personne à le Numero : (2 * Numero) et
         * la mère de la personne à le Numero : (2 * Numero + 1)
         */
        public int Numero { get; set; }

        /**
        * @var Nom
        * @brief Nom de famille d'une personne.
        * @warning Peut être null.
        * @details Propriété de la classe qui indique le nom de famille d'une personne.
        */
        public string? Nom
        {
            get { return _nom; }
            set
            {

                _nom = value;
                Inconnu = _estInconnu();
            }
        }

        /**
        * @var Prenoms
        * @brief Prenoms d'une personne.
        * @warning Peut être null.
        * @details
        * String des prenoms de la personne. 
        * Fait réference à _listePrenom, la liste des prenoms de la personne.
        */
        public string? Prenoms
        {
            get
            {
                if (_listePrenom.Count == 0)
                    return null;
                else
                {
                    StringBuilder strBuild = new();
                    foreach (var p in _listePrenom)
                        strBuild.Append(p + " ");
                    // Supprimer le dernier espace rajouté
                    strBuild.Remove(strBuild.Length - 1, 1);
                    return strBuild.ToString();
                }
            }
            set
            {
                _listePrenom.Clear(); // Supprime tout les prénoms existants
                if (value != null)
                {

                    string[] listeValues = value.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    foreach (var p in listeValues)
                    {
                        _listePrenom.Add(p.Trim());
                    }
                }
                Inconnu = _estInconnu();
            }
        }

        /**
         * @var DateNaissance
         * @brief Date de naissance de la personne.
         * @warning Peut être null.
         */
        public DateOnly? DateNaissance
        {
            get => _dateNaissance;
            set
            {
                if (value is DateOnly || value is null)
                {
                    _dateNaissance = value;
                    Inconnu = false;
                }
            }
        }

        /**
         * @var DateDeces
         * @brief Date de deces de la personne.
         * @warning Peut être null.
         */
        public DateOnly? DateDeces
        {
            get => _dateDeces;
            set
            {
                if (value is DateOnly || value is null)
                {
                    _dateDeces = value;
                }
                Inconnu = _estInconnu();
            }
        }

        /**
         * @var LieuNaissance
         * @brief Ville de naissance.
         * @warning peut être null, ou incomplete.
         */
        public Ville? LieuNaissance
        {
            get => _lieuNaissance;
            set
            {
                if (value is Ville || value is null)
                {
                    _lieuNaissance = value;
                }
                Inconnu = _estInconnu();
            }
        }

        /**
         * @var Nationalite
         * @brief Nationalite de la personne.
         * @warning Peut être null.
         * @remark Peut être mettre une Liste pour les nationalites, 
         * et enregistrer les des valeurs predefinies.
         */
        public string? Nationalite
        {
            get => _nationalite;
            set
            {
                _nationalite = value;
                Inconnu = _estInconnu();
            }
        }

        /**
         * @var Inconnu
         * @brief Booleen qui indique si la personne est inconnue. (Lecture seule)
         * @warning La valeur est true par défaut.
         * Valeur booleen qui indique si la personne est inconnue, accessible uniquement en lecture.
         * La valeur passe à false s'il l'une des valeurs est renseignées. (ie différent de null)
         */
        public bool Inconnu { get; protected set; }

        /**
         * @fn public Personne
         * @param int num *Numero de l'enfant*
         * @param int? id = null *Id de la personne dans la BDD*
         * @param int? idPere = null *Id du pere dans la BDD*
         * @param int? idMere = null *Id de la mere dans la BDD*
         * @param string? nom = null,
         * @param string? prenoms = null
         * @param DateOnly? dateNaissance = null
         * @param DateOnly? dateDeces = null
         * @param Ville? lieuNaissance = null
         * @param string? nationalite = null
         * 
         * @brief Constructeur de la classe Personne.
         * @details
         * Definie les propiétés de la personne.
         */
        public Personne(int num, int? id = null, int? idPere = null, int? idMere = null,
            string? nom = null, string? prenoms = null,
            DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
            Ville? lieuNaissance = null, string? nationalite = null)
        {
            Id = id;
            IdPere = idPere;
            IdMere = idMere;
            Numero = num;
            Nom = nom;
            _listePrenom = new List<string>();
            Prenoms = prenoms;
            DateNaissance = dateNaissance;
            DateDeces = dateDeces;
            LieuNaissance = lieuNaissance;
            Nationalite = nationalite;
            _indexImageProfil = null;

            Inconnu = _estInconnu();

        }

        public Personne(int? id = null, int? idPere = null, int? idMere = null,
    string? nom = null, string? prenoms = null,
    DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
    Ville? lieuNaissance = null, string? nationalite = null)
        {
            Numero = 1;
        }



        /**
        * @fn public void AjouterPrenoms(string[] inListeValue)
        * @param string[] inListeValue *Liste de prenoms*
        * @brief Ajouter un/des prenom(s) à la personne.
        * @details
        * Ajoute les prenoms passés en paramètre à la personne.
        * Regarde si le nom n'est pas déjà ajouté dans la liste des prenoms.
        */
        public void AjouterPrenoms(string[] inListeValue)
        {
            if (inListeValue != null)
            {
                if (_listePrenom is null)
                    throw new NullReferenceException();

                foreach (var p in inListeValue)
                {
                    // Regarde si le prenom n'est pas déjà present
                    if (!_listePrenom.Contains(p.Trim()))
                        _listePrenom.Add(p.Trim());
                }
            }
            Inconnu = _estInconnu();
        }

        /**
        * @overload public void AjouterPrenoms(string value)
        * @param string value *Chaine de caractères de prenoms*
        */
        public void AjouterPrenoms(string value)
        {
            if (value != null)
            {

                string[] listeValues = value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                this.AjouterPrenoms(listeValues);
            }
        }

        /**
        * @fn public void SetPrenoms(string[] inListeValue)
        * @param string[] inListeValue *Liste de prenoms*
        * @brief Définit un/des prenom(s) à la personne.
        * @details
        * Définit les prenoms passés en paramètre à la personne.
        * Supprime les prénoms déjà existant de la personne.
        * Regarde si le nom n'est pas déjà ajouté dans la liste des prenoms.
        */
        public void SetPrenoms(string[] inListeValue)
        {
            if (inListeValue != null)
            {
                _listePrenom.Clear();

                foreach (var p in inListeValue)
                {
                    _listePrenom.Add(p.Trim());
                }
            }
            Inconnu = _estInconnu();
        }

        /**
        * @overload public void SetPrenoms(string value)
        * @param string value *Chaine de caractères de prenoms*
        */
        public void SetPrenoms(string value)
        {
            if (value != null)
            {

                string[] listeValues = value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                this.SetPrenoms(listeValues);
            }
        }

        /**
         * @fn public string? GetPrenoms()
         * @brief Donne les prenoms de la personnes.
         * @return string? *Chaine des prenoms*
         * @warning Retourne null s'il n'y a pas de prenoms
         */
        public string? GetPrenoms()
        {
            if (_listePrenom is null || _listePrenom.Count == 0)
                return null;
            else
            {
                StringBuilder strBuild = new();
                foreach (var p in _listePrenom)
                    strBuild.Append(p + " ");
                return strBuild.ToString();
            }
        }

        /**
         * @fn public List<string> GetListPrenoms()
         * @brief Liste des prénoms.
         * @return List<string>
         */
        public List<string> GetListPrenoms()
        {
            return _listePrenom;
        }

        /**
        * @fn public int GetPereId()
        * @brief Donne le Numero du pere
        * @return int *Numero du père*
        */
        public int GetPereId()
        {
            return Numero * 2;
        }

        /**
        * @fn public int GetMereId()
        * @brief Donne le Numero de la mere
        * @return int *Numero de la mere*
        */
        public int GetMereId()
        {
            return Numero * 2 + 1;
        }

        /**
         * @fn public override string ToString()
         * @brief Donne les informations sur la personne.
         * @return string
         */
        public override string ToString()
        {
            StringBuilder strBuil = new();
            if (Inconnu == false)
            {
                strBuil.Append(Nom is null ? "NomInconnu " : Nom + " ");
                strBuil.Append(Prenoms is null ? "PrenomInconnu " : Prenoms + " ");
                strBuil.Append(DateNaissance is null ? "[NaissanceInconnu-" :
                    "[" + DateNaissance + "-");
                strBuil.Append(DateDeces is null ? "DecesInconnu] " : DateDeces + "] ");
            }
            else
            {
                strBuil.Append("Inconnu");
            }

            return strBuil.ToString();
        }

        /**
         * @fn private bool _estInconnu() 
         * @brief Check si la personne est inconnu.
         * @return bool - *Toutes les proprietes sont nulles*
         * @details
         * Check suivant les proprietes, si la personne est inconnu. 
         * Si la personne possede une propriete non null, alors *Inconnu = true*.
         */
        protected bool _estInconnu()
        {
            if (Nom != null || Prenoms != null ||
                 DateNaissance != null || DateDeces != null ||
                 LieuNaissance != null || Nationalite != null ||
                 _listeFichiers.Count > 0)
            { return false; }
            else
            { return true; }
        }

        /**
         * @fn public void SupprimerNom()
         * @brief Supprime le nom de la personne.
         * @details
         * Supprime le nom de la personne, 
         * et maintient à jour la propriete *Inconnu*.
         */
        public void SupprimerNom()
        {
            _nom = null;
            Inconnu = _estInconnu();
        }

        /**
         * @fn public void SupprimerPrenoms()
         * @brief Supprime tous les prenoms de la personne.
         * @details
         * Supprime tous les prenoms de la personne, 
         * et maintient à jour la propriete *Inconnu*.
         */
        public void SupprimerPrenoms()
        {
            _listePrenom.Clear(); // Check si la liste est bien supp en memoire
            Inconnu = _estInconnu();
        }

        /**
         * @fn public void SupprimerPrenomSpecifique(string[] listePrenoms)
         * @brief Supprime une liste de prenoms.
         * @param string[] listePrenoms - *Liste de pénoms à supprimer*
         * @details
         * Supprime les prenoms specifiés en parametre de la liste des prenoms de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         */
        public void SupprimerPrenomsSpecifique(string[] listePrenoms)
        {
            foreach (var prenom in listePrenoms)
            {
                if (!_listePrenom.Remove(prenom.Trim()))
                {
                    Console.WriteLine("Le prenom a supp n'est pas dans la liste des prenom");
                }
            }
            Inconnu = _estInconnu();
        }

        /**
         * @overload public void SupprimerPrenomSpecifique(string prenom)
         * @brief Supprime un prenoms.
         * @param string prenoms - *Le prenom à supprimer*
         * @details
         * Supprime les prenoms specifiés en parametre de la liste des prenoms de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         */
        public void SupprimerPrenomsSpecifique(string prenoms)
        {
            string[] listePrenoms = prenoms.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            this.SupprimerPrenomsSpecifique(listePrenoms);
        }

        /**
         * @fn public void SupprimerPrenomPosition(int position)
         * @brief Supprime le ème prenom.
         * @param int position - *Position du prenom à supprimer*
         * @details
         * Supprime le prenom à la position indiquée en parametre de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         * @warning le premier prenom de la personne est à l'indice **0**. 
         */
        public void SupprimerPrenomPosition(int position)
        {
            if (position >= 0 && position < _listePrenom.Count)
            {
                _listePrenom.RemoveAt(position);
                Inconnu = _estInconnu();
            }
            else
            {
                Console.WriteLine("Indice donnee en parametre hors de la liste"
                    + " taille de la liste de prenoms : " + _listePrenom.Count);
            }
        }

        /**
         * @fn public void SupprimerDernierPrenom()
         * @brief Supprime le dernier prenom.
         * @details
         * Supprime le dernier prenom, dans la liste des prenoms, de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         */
        public void SupprimerDernierPrenom()
        {
            if (_listePrenom.Count > 0)
            {
                _listePrenom.RemoveAt(_listePrenom.Count - 1);
                Inconnu = _estInconnu();
            }
            else
            {
                Console.WriteLine("Pas de prenom à supprimer.");
            }
        }

        /**
         * @fn public void SupprimerDateNaissance()
         * @brief Supprime la date de naissance de la personne.
         * @details
         * Supprime la date de naissance de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         */
        public void SupprimerDateNaissance()
        {
            _dateNaissance = null;
            Inconnu = _estInconnu();
        }

        /**
         * @fn public void SupprimerDateDeces()
         * @brief Supprime la date de deces de la personne.
         * @details
         * Supprime la date de deces de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         */
        public void SupprimerDateDeces()
        {
            _dateDeces = null;
            Inconnu = _estInconnu();
        }

        /**
         * @fn public void SupprimerLieuNaissance()
         * @brief Supprime le lieu de naissance de la personne.
         * @details
         * Supprime le lieu de naissance de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         */
        public void SupprimerLieuNaissance()
        {
            _lieuNaissance = null;
            Inconnu = _estInconnu();
        }

        /**
         * @fn public void SupprimerNationalite()
         * @brief Supprime la nationalite de la personne.
         * @details
         * Supprime la nationalite de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         */
        public void SupprimerNationalite()
        {
            _nationalite = null;
            Inconnu = _estInconnu();
        }

        /**
         * @fn public void EnleverImageProfil()
         * @brief Enleve l'index sur l'image de profil.
         * @details
         * Ne supprime pas l'image de la personne, mais ne la reference
         * plus comme image de profil.
         * @warning Une personne ne peut pas devenir inconnu après cette methode (image non supprimer)
         */
        public void EnleverImageProfil()
        {
            _indexImageProfil = null;
        }


        /**
         * @fn public void SetImageProfil(FichierImage imageSelectionne)
         * @brief Choisit la photo de profil.
         * @param Image imageSelect
         * 
         */
        public void SetImageProfil(FichierImage imageSelect)
        {
            int image_trouve = 0; // Sert a compter le nb d'image trouve, normalement que 1
            for (int i = 0; i < _listeFichiers.Count; i++)
            { // Verifie que le fichier est bien une image
                if (_listeFichiers[i] == imageSelect)
                    if (_listeFichiers[i] is FichierImage)
                    {
                        _indexImageProfil = i;
                        image_trouve++;
                    }
                    else
                    {
                        Console.WriteLine("Fichier " +
                            $"{_listeFichiers[i].NomFichier} ({imageSelect})"
                            + " n'est pas une image.");
                        break;
                    }
            }

            if (image_trouve == 0)
            {
                Console.WriteLine("Image non trouvee.");
            }
            else if (image_trouve == 1 && !(_indexImageProfil is null))
            {
                Console.WriteLine("Image trouvee, photo de profil selection : " +
                    $"{_listeFichiers[(int)_indexImageProfil].NomFichier} " +
                    $"({_listeFichiers[(int)_indexImageProfil].Id})");
            }
            else
            { Console.WriteLine("Plus d'une image de profil trouve."); }
        }


        /**
         * @fn public void AjouterImage(string filename, bool imageProfil = false)
         * @param FichierImage image 
         * @param bool imageProfil = false - Definit l'image pour le profil 
         */
        public void AjouterImage(FichierImage image, bool imageProfil = false)
        {

            _listeFichiers.Add(image);
            if (imageProfil)
                _indexImageProfil = _listeFichiers.Count - 1;
        }

        /**
         * @fn public FichierImage? GetFichierImageProfil()
         * @brief Retourne le FichierImage du profil de la personne
         * @warning Peut être null.
         * @return FichierImage? *Le FichierImage du profil de la personne*
         */
        public FichierImage? GetFichierImageProfil()
        {
            if (!(_indexImageProfil is null))
            {
                if (_listeFichiers[(int)_indexImageProfil] is FichierImage)
                {
                    return (FichierImage)_listeFichiers[(int)_indexImageProfil];
                }
                else
                {
                    Console.WriteLine("Erreur indexImageProfil pas un FichierImage");
                }
            }
            return null;
        }

        /**
         * @fn public Image? GetrImageProfil()
         * @brief Retourne l'image du profil de la personne
         * @warning Peut être null.
         * @return Image? *L'image du profil de la personne*
         */
        public Image? GetImageProfil()
        {
            if (!(_indexImageProfil is null))
            {
                if (_listeFichiers[(int)_indexImageProfil] is FichierImage)
                {
                    return ((FichierImage)_listeFichiers[(int)_indexImageProfil]).Image;
                }
                else
                {
                    Console.WriteLine("Erreur indexImageProfil pas un FichierImage");
                }
            }
            return null;
        }



        /**
         * @fn public List<FichierImage> GetFichierImages
         * @brief Retourne la liste des images de la personne.
         * @return List<FichierImage> Liste des images de la personne
         */
        public List<FichierImage> GetFichierImages()
        {
            List<FichierImage> listeFichierImage = new List<FichierImage>();
            foreach (var fich in _listeFichiers)
            {
                if (fich is FichierImage)
                {
                    listeFichierImage.Add((FichierImage)fich);
                }
            }
            return listeFichierImage;
        }

        /**
         * @fn public List<Image> GetImages
         * @brief Retourne la liste des images de la personne.
         * @return Liste des images de la personne
         */
        public List<Image> GetImages()
        {
            List<FichierImage> fichierImages = GetFichierImages();
            List<Image> listeImage = new List<Image>();

            foreach (var item in fichierImages)
            {
                listeImage.Add(item.Image);
            };

            return listeImage;
        }


        /**
         * @fn public void AjouterFichier()
         * @param Fichier inDoc
         * @brief Ajoute un fichier à la personne.
         * @warning Le fichier n'est pas forcement une image. 
         * (...)
         */
        public void AjouterFichier(Fichier inDoc)
        {
            _listeFichiers.Add(inDoc);
        }

        /**
         * @fn public void SupprimerFichier()
         * @param Fichier inDoc
         * @brief Supprime un fichier à la personne.
         * @warning Le fichier n'est pas forcement une image. 
         * (...)
         */
        public void SupprimerFichier(Fichier inDoc)
        {
            if (_listeFichiers.Remove(inDoc))
                Console.WriteLine("Fichier supprimé");
            else
                Console.WriteLine("Fichier pas supp, absent de la liste");
        }

        /**
         * @foverload  public void SupprimerFichier()
         * @param Guid g - *Id du fichier à supprimer*
         * @brief Supprime un fichier à la personne.
         * @warning Le fichier n'est pas forcement une image. 
         * (...)
         */
        public void SupprimerFichier(int idFichierImage)
        {
            foreach (var fich in _listeFichiers)
            {
                if (fich.Id == idFichierImage)
                {
                    _listeFichiers.Remove(fich);
                    Console.WriteLine("Fichier supprimé");
                }
            }
        }
        /**
         * @foverload  public void SupprimerFichier()
         * @param Guid g - *Id du fichier à supprimer*
         * @brief Supprime un fichier à la personne.
         * @warning Le fichier n'est pas forcement une image. 
         * (...)
         */
        public void SupprimerFichier(string nomFichier)
        {
            foreach (var fich in _listeFichiers)
            {
                if (fich.NomFichier == nomFichier)
                {
                    _listeFichiers.Remove(fich);
                    Console.WriteLine("Fichier supprimé");
                }
            }
        }


    }
}

