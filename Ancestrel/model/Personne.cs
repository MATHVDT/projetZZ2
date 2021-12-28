/**
 * @file Personne.cs
 * Fichier de classe Personne
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
    public class Personne
    {
        private string? _nom;
        private List<string>? _listePrenom;
        private DateTime? _dateNaissance;
        private DateTime? _dateDeces;


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
                    return _listePrenom.ToString();
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
        public DateTime? DateNaissance {
            get => _dateNaissance;
            set
            {
                if(value is DateTime)
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
        public DateTime? DateDeces
        {
            get => _dateDeces;
            set
            {
                if (value is DateTime)
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
        public bool Inconnu { get; private set; }

        //        public List<Document> ListeDoc { get; set; }

        public Personne()
        {

        }

    }
}
