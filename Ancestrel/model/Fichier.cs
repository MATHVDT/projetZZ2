/**
 * @file Fichier.cs
 * Fichier contenant la classe Fichier
 * @author Mathieu
 * @date 31/12/2021
 * @copyright ...
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
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
        public int? Id { get; }

        /**
         * @var NomFichier
         * @brief Nom du fichier.
         * @warning Le nom du fichier par defaut est l'Id.
         */
        public string NomFichier { get; set; }

        /**
         * @var DateAjoutFichier
         * @brief Date d'ajout du fichier dans le logiciel.
         * @todo Peut etre plutot la date d'ajout dans la bdd ???
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
         * @todo 
         * La var dateAjout est modifier ici, mais peut etre la virer de la classe 
         * et gerer la date au moment de l'insertion de l'element dans la BDD.
         */
        public Fichier(DateTime dateAjout, int? inId = null, string? inNomFichier = null)
        {
            Id = inId;
            if (!(inNomFichier is null) && (inNomFichier.Length > 0))
                NomFichier = inNomFichier;
            else
                NomFichier = inId is null ? $"Fichier_{Guid.NewGuid()}" : $"Fichier_{Id}";


            DateAjoutFichier = DateTime.Now;
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
        :this(DateTime.Now, null, inNomFichier) { }

        public override string ToString()
        {
            return NomFichier;
        }
    }
}
