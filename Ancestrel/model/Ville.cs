/**
 * @file Ville.cs
 * Fichier contenant la classe Ville
 * @author Florian
 * @date 17/01/2021
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
    * @class Ville
    * @brief classe d'une Ville
    */
    public class Ville
    {
        /**
        * @var Id
        * @brief Identifiant unique d'une ville
        * @details
        * Identifiant de la ville
        */
        public Guid Id { get; private set; }

        /**
        * @var Nom
        * @brief Nom de la ville
        */
        public string Nom { get; set; }

        /**
        * @var Longitude
        * @brief Longitude de la ville
        * @details
        * Coordonnée en longitude de la ville
         */
        public double? Longitude { get; set; }

        /**
        * @var Lattitude
        * @brief Lattitude de la ville
        * @details
        * Coordonnée en lattitude de la ville
        */ 
        public double? Latitude { get; set; }

        //public GeoCoordinate Coordonnees { get; set; }

        /**
         * @fn public Ville 
         * @param string nom
         * @param double? longitude = null
         * @param double? lattitude = null
         * 
         * @brief Constructeur de la classe Ville
         * @details
         * Definie les propiétés de la ville.
         */
        public Ville(string nom, double? longitude = null, double? latitude = null)
        {
            Id = new Guid(nom);
            this.Nom = nom;
            this.Longitude = longitude;
            this.Latitude = latitude;
        }
    }
}
