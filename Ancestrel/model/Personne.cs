/**
 * @file Personne.cs
 * Fichier contenant la classe Personne
 * @author Mathieu
 * @date 28/12/2021
 * @copyright ...
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * @namespace model
 * Espace de nom des classes de l'application
 */
namespace model
{
    /**
     * @class Personne
     * @brief classe abstraite d'une personne
     */
    public abstract class Personne
    {

        private string? _nom = null;
        private List<string>? _listePrenom;
        private DateOnly? _dateNaissance;
        private DateOnly? _dateDeces;
        private Ville? _lieuNaissance;
        private string? _nationalite;




        /**
         * @var Identifiant
         * @brief Identifiant unique de la personne dans l'abre.
         * @details
         * Identidiant unique de la personne dans l'arbre généalogique. 
         * Le père de la personne à l'identifiant : (2 * Identifiant) et
         * la mère de la personne à l'identifiant : (2 * Identidiant + 1)
         */
        public uint Identifiant { get;  set; }


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
                if (value != null)
                {
                    _nom = value;
                    Inconnu = false;
                }
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
                        set
            {
                if (value != null)
                {

                    string[] listeValues = value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    if (_listePrenom is null)
                        _listePrenom = new List<string>();

                    foreach (var p in listeValues)
                    {
                        if (!_listePrenom.Contains(p)) // Verifie que le prenom n'y est pas deja
                            _listePrenom.Add(p);
                    }
                }
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
                if (value is DateOnly)
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
                if (value is DateOnly)
                {
                    _dateDeces = value;
                    Inconnu = false;
                }
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
                if (value != null)
                {
                    _lieuNaissance = value;
                    Inconnu = false;
                }
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
                if (value != null)
                {
                    _nationalite = value;
                    Inconnu = false;
                }
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
         * @var ListeImage
         * @brief Liste des differentes Images sur la personne.
         */
        private List<Image> _listeImage;
        private int? _indexImageProfil;

        /**
         * @var IndexImageProfil
         * @brief Indice de l'image pour le profil de la personne.
         * @warning Peut être null.
         */
        public int? IndexImageProfil
        {
            get { return _indexImageProfil; }
            set
            {
                if (value != null && value >= 0 && value < _listeImage.Count)
                {
                    IndexImageProfil = value;
                    Inconnu = false;
                }
            }
        }



        /**
         * @fn public Personne 
         * @param uint iden *Identidiant de l'enfant*
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
        public Personne(uint iden, string? nom = null, string? prenoms = null,
            DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
            Ville? lieuNaissance = null, string? nationalite = null)
        {
            Identifiant = iden;
            Nom = nom;
            Prenoms = prenoms;
            DateNaissance = dateNaissance;
            DateDeces = dateDeces;
            LieuNaissance = lieuNaissance;
            Nationalite = nationalite;
            _listeImage = new List<Image>();


            if (Nom != null || Prenoms != null ||
                DateNaissance != null || DateDeces != null)
            { Inconnu = false; }
            else
            { Inconnu = true; }

        }



        /**
        * @fn public void SetPrenoms(string[] inListeValue)
        * @param string[] inListeValue *Liste de prenoms*
        * @brief Ajouter un/des prenom(s) à la personne.
        * @details
        * Ajoute les prenoms passés en paramètre à la personne.
        * Regarde si le nom n'est pas déjà ajouté dans la liste des prenoms.
        */
        public void SetPrenoms(string[] inListeValue)
        {
            if (inListeValue != null)
            {
                if (_listePrenom is null)
                    _listePrenom = new List<string>();

                foreach (var p in inListeValue)
                {
                    // Regarde si le prenom n'est pas déjà present
                    if (!_listePrenom.Contains(p))
                        _listePrenom.Add(p);
                }
                Inconnu = false;
            }
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
         * @fn SupprimerPrenoms(string[] inListeValue)
         * @brief Supprime les prenoms
         * @param string[] inListeValue *Liste de prenoms*
         */
        public void SupprimerPrenoms(string[] inListeValue)
        {
            if (!(_listePrenom is null) && _listePrenom.Count() > 0)
            {
                foreach (var p in inListeValue)
                {
                    this.SupprimerPrenoms(p);
                }
            }
        }
        /**
         * @overload SupprimerPrenoms(string value)
         * @param string inValue *Prenoms*
         */
        public void SupprimerPrenoms(string inValue)
        {
            string[] listePrenomsSupp = inValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            SupprimerPrenoms(listePrenomsSupp);
        }

        /**
        * @fn public uint GetPereId()
        * @brief Donne l'identifiant du pere
        * @return uint *identifiant du père*
        */
        public uint GetPereId()
        {
            return Identifiant * 2;
        }

        /**
        * @fn public uint GetMereId()
        * @brief Donne l'identifiant de la mere
        * @return uint *identifiant de la mere*
        */
        public uint GetMereId()
        {
            return Identifiant * 2 + 1;
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
                strBuil.Append(Prenoms is null ? "PrenomInconnu " : Prenoms);
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
         * @fn public void SetImageProfil(Image imageSelectionne)
         * @brief Choisit la photo de profil.
         * @param Image imageSelect
         */
        public void SetImageProfil(Image imageSelect)
        {
            int image_trouve = 0; // Sert a compter le nb d'image trouve, normalement que 1
            for (int i = 0; i < _listeImage.Count; i++)
            {
                if (_listeImage[i] == imageSelect)
                {
                    _indexImageProfil = i;
                    image_trouve++;
                }
            }

            if (image_trouve == 0)
            {
                Console.WriteLine("Image non trouvee.");
            }
            else if (image_trouve == 1 && !(_indexImageProfil is null))
            {
                Console.WriteLine("Image trouvee, photo de profil selection : " +
                    _listeImage[(int)_indexImageProfil].Tag + " ; "
                    + _listeImage[(int)_indexImageProfil].ToString());
            }
            else
            { Console.WriteLine("Plus d'une image de profil trouve."); }
        }

        /**
         * @fn public void AjouterImage(string filename, bool imageProfil = false)
         * @param string filename - Nom de l'image *(path)*
         * @param bool imageProfil = false - Definit l'image pour le 
         */
        public void AjouterImage(string filename, bool imageProfil = false)
        {

            try
            {
                Image img = Image.FromFile(filename); // Charge l'image
                _listeImage.Add(img);   // Ajoute l'image
                if (imageProfil)    // Definit l'image comme image de profil
                    _indexImageProfil = _listeImage.Count - 1;

            }
            catch (TypeInitializationException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("using System.Drawing : " +
                    "bibliothèque spécifique à Windows ");
            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Format d'image du fichier n'est pas valide.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Le fichier spécifié n'existe pas.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
        }

        /**
         * @fn public Image? GetImageProfil()
         * @brief Retourne l'image de profil
         * @warning Peut être null.
         * @return Image? l'image de profil de la personne.
         */
        public Image? GetImageProfil()
        {
            return _indexImageProfil is null ? null
                                     : _listeImage[(int)_indexImageProfil];
        }

        /**
         * @fn public List<Image> GetImagesPersonne
         * @brief Retourne la liste des images de la personne.
         * @return Liste des images de la personne
         */
        public List<Image> GetImagesPersonne()
        {
            return _listeImage;
        }


    }
}