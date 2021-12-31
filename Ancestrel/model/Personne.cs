/**
 * @file Personne.cs
 * Fichier contenant la classe Personne
 * @author Mathieu
 * @date 28/12/2021
 * @copyright ...
 */

using System;
using System.Collections.Generic;
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
        private List<string>? _listePrenom = null;
        private DateOnly? _dateNaissance = null;
        private DateOnly? _dateDeces = null;

        /**
         * @var Identifiant
         * @brief Identifiant unique de la personne dans l'abre.
         * @details
         * Identidiant unique de la personne dans l'arbre généalogique. 
         * Le père de la personne à l'identifiant : (2 * Identifiant) et
         * la mère de la personne à l'identifiant : (2 * Identidiant + 1)
         */
        public uint Identifiant { get; set; }


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
            /**
             * Simple getter 
             * @brief Obtenir les prenoms de la personne.
             * @warning Peut être null.
             * @return Renvoie un string des prenoms de la personne.
             */
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
            /**
             * Simpe setter
             * @param un string
             * @brief Ajouter un/des prenoms à la personne.
             * @details
             * Prend la string et la split pour obtenir les différents prenoms 
             * et les ajoute à la liste de prenoms.
             */
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
         * @var Inconnu
         * @brief Booleen qui indique si la personne est inconnue. (Lecture seule)
         * @warning La valeur est true par défaut.
         * Valeur booleen qui indique si la personne est inconnue, accessible uniquement en lecture.
         * La valeur passe à false s'il l'une des valeurs est renseignées. (ie différent de null)
         */
        public bool Inconnu { get; protected set; }

        //        public List<Document> ListeDoc { get; set; }

        /**
         * @fn Personne()
         * @brief Constructeur d'une personne inconnue
         */
        public Personne(uint iden, string? nom = null, string? prenoms = null,
            DateOnly? dateNaissance = null, DateOnly? dateDeces = null)
        {
            Nom = nom;
            Prenoms = prenoms;
            DateNaissance = dateNaissance;
            DateDeces = dateDeces;

            if (Nom != null || Prenoms != null ||
                DateNaissance != null || DateDeces != null)
            { Inconnu = false; }
            else
            { Inconnu = true; }

        }






        /**
        * @fn public void SetPrenoms(string[] inListeValue)
        * @param string[] inListeValue Liste de prenoms
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
        * @param string value Chaine de caractères de prenoms
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
         * @return string? Chaine des prenoms
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
         * @param string[] inListeValue Liste de prenoms
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
         * @param string inValue Prenoms
         */
        public void SupprimerPrenoms(string inValue)
        {
            string[] listePrenomsSupp = inValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            SupprimerPrenoms(listePrenomsSupp);
        }

        /**
         * @fn public override string ToString()
         * @brief Donne les informations sur la personne.
         * @return un string
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

    }
}