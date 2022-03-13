/**
 * @file Ville.cs
 * Fichier contenant la classe Ville
 * @author Florian
 * @date 17/01/2021
 * @copyright ...
 */

using System;
using System.Text;

/**
 * @namespace Model
 * Espace de nom des classes de l'application
 */
namespace Model
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
        public int? Id { get; set; }

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
         * @param int? id = null - *Id de la ville*
         * @param string nom = null
         * @param double? longitude = null
         * @param double? lattitude = null
         * 
         * @brief Constructeur de la classe Ville
         * @details
         * Definie les propiétés de la ville.
         */
        public Ville(int? id = null, string? nom = null, double? longitude = null, double? latitude = null)
        {
            Id = id;

            // Donne une nom à une ville
            if (!(nom is null) && nom.Length > 0)
                Nom = nom;
            else
                nom = id is null ? $"NouvelleVille_{Guid.NewGuid()}" : $"NouvelleVille_{Id}";

            Nom = nom;
            Longitude = longitude;
            Latitude = latitude;
        }


        /**
         * @overload public Ville(string nom = "", double? longitude = null, double? latitude = null)
         * @brief Constructeur d'une nouvelle ville. *sans id*
         */
        public Ville(string? nom = null, double? longitude = null, double? latitude = null)
        : this(null, nom, longitude, latitude) { }

        /**
         * @overload public ToString()
         */
        public override string ToString()
        {
            StringBuilder strBuil = new();

            strBuil.Append(Nom is null ? "NomInconnu " : Nom + " (");
            strBuil.Append(Latitude is null ? "x.xxx" : "Latitude : " + Latitude + "; ");
            strBuil.Append(Longitude is null ? "x.xxx" : "Longitude : " + Longitude + ")");

            return strBuil.ToString();
        }
    }
}
