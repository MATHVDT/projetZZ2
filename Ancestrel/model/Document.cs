/**
 * @file Document.cs
 * Fichier contenant la classe Document
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
     * @class Document
     * @brief Classe pour les documents à associer au personne.
     */
    public class Document
    {
        /**
         * @var Id
         * @brief Identifiant unique du document.
         */
        public Guid Id { get; }

        /**
         * @var NomDocument
         * @brief Nom du document.
         * @warning Le nom du document par defaut est l'Id.
         */
        public string NomDocument { get; set; }

        /**
         * @var DateAjoutDocument
         * @brief Date d'ajout du fichier dans le logiciel.
         */
        public readonly DateTime DateAjoutDocument;


        /**
         * @fn Document(string inNomDocument)
         * @param string inNomDocument
         * @brief Constructeur du document.
         * @details
         * Definit l'Id du document à l'aide d'un GUID. Si aucun nom de document
         * est fournit alors le nom par defaut est l'Id.
         */
        public Document(string inNomDocument) : 
            this(Guid.NewGuid(), inNomDocument) { }

        /**
         * @overload public Document()
         */
        public Document() : this(Guid.NewGuid()) { }

        /**
        * @overload public Document(Guid inId)
        * @param Guid inId Identifiant du document
        */
        public Document(Guid inId) : this(inId, inId.ToString()) { }

        /**
        * @overload public Document(Guid inId, string inNomDocument)
        * @param Guid inId Identifiant du document
        * @param string inNomDocument
        */
        public Document(Guid inId, string inNomDocument)
        {
            Id = inId;
            if (inNomDocument.Length > 0)
                NomDocument = inNomDocument;
            else
                NomDocument = Id.ToString();

            DateAjoutDocument = DateTime.Now;
        }



        public override string ToString()
        {
            return NomDocument;
        }
    }
}
