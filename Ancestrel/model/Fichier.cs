/**
 * @file Fichier.cs
 * Fichier contenant la classe Fichier
 * @author Mathieu
 * @date 31/12/2021
 * @copyright ...
 */
using System;

namespace Model
{
    /**
     * @class Fichier
     * @brief Classe pour les fichier à associer au personne.
     */
    public class Fichier
    {
        /**
         * @var Id
         * @brief Id du fichier dans la BDD.
         */
        public int? Id { get; set; }

        /**
         * @var NomFichier
         * @brief Nom du fichier.
         * @warning Le nom du fichier par defaut est l'Id.
         */
        public string NomFichier { get; set; }

        /**
         * @var DateAjoutFichier
         * @brief Date d'ajout du fichier dans le logiciel.
         */
        public readonly DateTime DateAjoutFichier;



        /**
         * @overload public Fichier(int? inId = null, string? inNomFichier = null)
         * @brief Constructeur du fichier.
         * 
         * @param DateTime dateAjout - *Date d'ajout du fichier*
         * @param int? inId = null - *Identifiant du fichier dans la BDD*
         * @param string? inNomFichier = null - *Nom du fichier*
         * 
         * @details
         * Crée un ficher. L'id est celui dans la BDD,
         * s'il est null, cela veut dire qu'il n'a pas encore été ajouté 
         * dans la BDD.
         */
        public Fichier(DateTime? dateAjout = null, int? inId = null, string? inNomFichier = null)
        {
            Id = inId;
            if (!(inNomFichier is null) && (inNomFichier.Length > 0))
                NomFichier = inNomFichier;
            else
                NomFichier = inId is null ? $"Fichier_{Guid.NewGuid()}" : $"Fichier_{Id}";


            DateAjoutFichier = dateAjout ?? DateTime.Now;
        }

        /**
         * @overload public Fichier(string? nomFichier = null)
         * @brief Constructeur d'un nouveau fichier. *sans id*
         */
        public Fichier(string? nomFichier = null)
            : this(null, nomFichier) { }

        /**
         * @overload public Fichier(int? inId = null, string? nomFichier = null)
         * @brief Constructeur d'un nouveau fichier. *sans id*
         */
        public Fichier(int? inId = null, string? inNomFichier = null)
        : this(null, null, inNomFichier) { }

        public override string ToString()
        {
            return NomFichier;
        }
    }
}
