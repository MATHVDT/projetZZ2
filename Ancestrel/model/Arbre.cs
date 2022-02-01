/**
 * @file Arbre.cs
 * Fichier contenant la classe Arbre
 * @author Florian
 * @date 03/01/2022
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
   * @class Arbre
   * @brief classe d'un arbre
   */
    public class Arbre
    {

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
        public Dictionary<uint, Personne> Personnes;

        /**
         * @fn public Arbre 
         * @param string nom
         * @param string desc
         * @param Personne cujus *Personne à la base de l'arbre*
         * @brief Constructeur de la classe Arbre.
         * @details
         * Construit la base d'un arbre.
         */
        public Arbre(string nom, string desc, Personne cujus)
        {
            Nom = nom;
            Description = desc;
            Personnes = new Dictionary<uint, Personne>();
            cujus.Numero = 1;
            Personnes.Add(cujus.Numero, cujus);
        }


        /**
        * @fn AjouterPere(uint idEnfant, string? nom = null, string? prenoms = null,
           DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
           Ville? lieuNaissance = null, string? nationalite = null)
        * @brief Ajoute un père
        * @param uint idEnfant
        * @param  string? nom = null
        * @param string? prenoms = null
        * @param DateOnly? dateNaissance = null
        * @param DateOnly? dateDeces = null
        * @param Ville? lieuNaissance = null
        * @param string? nationalite = null
        * @details
        * Ajoute une personne de type Homme avec le Numero calculé en fonction de celui de l'enfant
        */
        public void AjouterPere(uint idEnfant, string? nom = null, string? prenoms = null,
            DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
            Ville? lieuNaissance = null, string? nationalite = null)
        {
            Homme pere = new Homme(idEnfant, null, nom, prenoms, dateNaissance, dateDeces, lieuNaissance, nationalite);
            Personnes.Add(pere.Numero, pere);
        }

        /**
        * @fn AjouterMere(uint idEnfant, string? nom = null, string? prenoms = null,
           DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
           Ville? lieuNaissance = null, string? nationalite = null)
        * @brief Ajoute une mere
        * @param uint idEnfant
        * @param  string? nom = null
        * @param string? prenoms = null
        * @param DateOnly? dateNaissance = null
        * @param DateOnly? dateDeces = null
        * @param Ville? lieuNaissance = null
        * @param string? nationalite = null
        * @details
        * Ajoute une personne de type Femme avec le Numero calculé en fonction de celui de l'enfant
        */
        public void AjouterMere(uint idEnfant, string? nom = null, string? prenoms = null,
            DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
            Ville? lieuNaissance = null, string? nationalite = null)
        {
            Femme mere = new Femme(idEnfant, null, nom, prenoms, dateNaissance, dateDeces, lieuNaissance, nationalite);
            Personnes.Add(mere.Numero, mere);
        }

        /**
        * @fn SupprimerPersonne(uint idPersonne)
        * @brief Supprime une personne
        * @param uint idPersonne
        * @details
        * Supprime une personne de l'arbre à partir de son id si elle est bien présente dans celui-ci.
        * @warning ArgumentException si l'id ne correspond à aucune personne de l'arbre
        */
        public void SupprimerPersonne(uint idPersonne)
        {
            if (Personnes.ContainsKey(idPersonne))
            {
                Personnes.Remove(idPersonne);
            }
            else
            {
                throw new ArgumentException("La personne n'est pas présente dans l'arbre (id : "+idPersonne+")");
            }
        }

    }
}
