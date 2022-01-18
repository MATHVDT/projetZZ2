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
         * @brief Identifiant unique du fichier.
         */
        public Guid Id { get; }

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
         * @fn Fichier(string inNomFichier)
         * @param string inNomFichier
         * @brief Constructeur du fichier.
         * @details
         * Definit l'Id du fichier à l'aide d'un GUID. Si aucun nom de fichier
         * est fournit alors le nom par defaut est l'Id.
         */
        public Fichier(string inNomFichier = "") :
            this(Guid.NewGuid(), inNomFichier)
        { }

        /**
         * @overload public Fichier()
         */
        public Fichier() : this(Guid.NewGuid()) { }

        /**
        * @overload public Fichier(Guid inId)
        * @param Guid inId Identifiant du fichier
        */
        public Fichier(Guid inId) : this(inId, inId.ToString()) { }

        /**
         * @overload public Fichier(Guid inId, string inNomFichier)
         * @param Guid inId Identifiant du fichier
         * @param string inNomFichier
         */
        public Fichier(Guid inId, string inNomFichier)
        {
            Id = inId;
            if (inNomFichier.Length > 0)
                NomFichier = inNomFichier;
            else
                NomFichier = Id.ToString();

            DateAjoutFichier = DateTime.Now;
        }



        public override string ToString()
        {
            return NomFichier;
        }
    }
}
