/**
 * @file Arbre.cs
 * Fichier contenant la classe Arbre
 * @author Florian
 * @date 03/01/2022
 * @copyright ...
 */

using Model;
using System;
using System.Collections.Generic;
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
   * @class Arbre
   * @brief classe d'un arbre
   */
    public class Arbre
    {

        /**
         * @var Id
         * @brief Id de l'arbre dans la BDD.
         */
        public string? Id { get; set; }

        /**
        * @var Nom
        * @brief Nom de l'arbre
        * @details
        * Nom de l'arbre
        */
        public string Nom { get; set; }

        /**
         * @var Description
         * @brief Description de l'arbre
         * @details
         * Description de l'arbre
         */
        public string Description { get; set; }

        /**
        * @var Personnes
        * @brief Dictionnaire des personnes
        * @details
        * Dictionnaire contenant les personnes apparaissant dans l'arbre
        */
        public Dictionary<int, Personne> Personnes;

        /**
         * @fn public Arbre 
         * @param int? id = null
         * @param string nom
         * @param string desc
         * @param Personne cujus *Personne à la base de l'arbre*
         * @brief Constructeur de la classe Arbre.
         * @details
         * Construit la base d'un arbre.
         */
        public Arbre(int? id, string nom, string desc, Personne cujus)
        {
            Id = Id;
            Nom = nom;
            Description = desc;
            Personnes = new Dictionary<int, Personne>();
            cujus.Numero = 1;
            Personnes.Add(cujus.Numero, cujus);
        }


        /**
         * @overload public Arbre(string nom, string desc, Personne cujus)
         * @brief Constructeur d'un nouvel arbre. *sans id*
         */
        public Arbre(string nom, string desc, Personne cujus)
            : this(null, nom, desc, cujus) { }



        /**
        * @fn AjouterPere(int idEnfant, string? nom = null, string? prenoms = null,
           DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
           Ville? lieuNaissance = null, string? nationalite = null)
        * @brief Ajoute un père
        * @param int idEnfant
        * @param  string? nom = null
        * @param string? prenoms = null
        * @param DateOnly? dateNaissance = null
        * @param DateOnly? dateDeces = null
        * @param Ville? lieuNaissance = null
        * @param string? nationalite = null
        * @details
        * Ajoute une personne de type Homme avec le Numero calculé en fonction de celui de l'enfant
        */
        public void AjouterPere(int idEnfant, string? nom = null, string? prenoms = null,
            DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
            Ville? lieuNaissance = null, string? nationalite = null)
        {
            Homme pere = new Homme(idEnfant, null, nom, prenoms, dateNaissance, dateDeces, lieuNaissance, nationalite);
            Personnes.Add(pere.Numero, pere);
        }


        /**
         * @overload AjouterPere
         * @brief Ajout d'une personne comme le pere 
         */
        public void AjouterPere(int numEnfant, Homme pere)
        {
            pere.Numero = 2 * numEnfant;
            Personnes.Add(pere.Numero, pere);
        }


        /**
        * @fn AjouterMere(int idEnfant, string? nom = null, string? prenoms = null,
           DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
           Ville? lieuNaissance = null, string? nationalite = null)
        * @brief Ajoute une mere
        * @param int idEnfant
        * @param  string? nom = null
        * @param string? prenoms = null
        * @param DateOnly? dateNaissance = null
        * @param DateOnly? dateDeces = null
        * @param Ville? lieuNaissance = null
        * @param string? nationalite = null
        * @details
        * Ajoute une personne de type Femme avec le Numero calculé en fonction de celui de l'enfant
        */
        public void AjouterMere(int idEnfant, string? nom = null, string? prenoms = null,
            DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
            Ville? lieuNaissance = null, string? nationalite = null)
        {
            Femme mere = new Femme(idEnfant, null, nom, prenoms, dateNaissance, dateDeces, lieuNaissance, nationalite);
            Personnes.Add(mere.Numero, mere);
        }


        /**
         * @overload AjouterMere
         * @brief Ajout d'une personne comme la mere 
         */
        public void AjouterMere(int numEnfant, Femme mere)
        {
            mere.Numero = 2 * numEnfant+1;
            Personnes.Add(mere.Numero, mere);
        }


        /**
        * @fn SupprimerPersonne(int idPersonne)
        * @brief Supprime une personne
        * @param int idPersonne
        * @details
        * Supprime une personne de l'arbre à partir de son id si elle est bien présente dans celui-ci.
        * @warning ArgumentException si l'id ne correspond à aucune personne de l'arbre
        */
        public void SupprimerPersonne(int idPersonne)
        {
            if (Personnes.ContainsKey(idPersonne))
            {
                Personnes.Remove(idPersonne);
            }
            else
            {
                throw new ArgumentException("La personne n'est pas présente dans l'arbre (id : " + idPersonne + ")");
            }
        }

    }
}
