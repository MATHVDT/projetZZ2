using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /**
     * @interface public IBddLoader
     * @brief Interface pour le chargement de données.
     * 
     * @details
     * Interface à implementer pour le charmgement des données
     * dans le Système de Persistance Choisie *(SPC)*.
     */
    public interface IBddLoader
    {

        /**
         * @fn public Arbre ChargerArbre
         * @brief Charge un arbre à partir d'un id d'une personne dans le *SPC*.
         * 
         * @param int idPersonne
         * 
         * @return Arbre - *Arbre construit à partir de l'id de la personne donnée*
         * 
         * @warning Revoie : ArgumentNullException (si la personne n'existe pas dans le *SPC*).
         */
        public Arbre ChargerArbre(int idPersonne);

        /**
         * @fn public Personne GetPersonneById
         * @brief Récupère une personne depuis le *SPC* avec son Id.
         * 
         * @param int idPersonne
         * 
         * @return Personne - *Personne chargée à partir de son Id*
         * 
         * @warning Renvoie : ArgumentNullException (si la personne n'est pas dans le *SPC*).
         */
        public Personne GetPersonneById(int idPersonne);

        /**
         * @fn public string? GetPrenomById
         * @brief Récupère les prénoms d'une personne depuis le *SPC* avec l'id de la personne.
         * 
         * @param int idPersonne
         * 
         * @return string? - *Prénoms de la personne*
         * 
         * @warning L'idPersonne ne doit pas être null.
         */
        public string? GetPrenomByIdPersonne(int idPersonne);

        /**
         * @fn public FichierImage GetFichierImageById
         * @brief Récupère un FichierImage depuis le *SPC* avec son Id.
         * 
         * @param int idFichierImage
         * 
         * @return FichierImage - *FichierImage chargée à partir de son Id*
         * 
         * @warning Renvoie : ArgumentNullException (si le fichierImage n'est pas dans le *SPC*).
         */
        public FichierImage GetFichierImageById(int idFichierImage);

        /**
         * @fn public Ville GetVilleById
         * @brief Récupère un Ville depuis le *SPC* avec son Id.
         * 
         * @param int idVille
         * 
         * @return Ville - *Ville chargée à partir de son Id*
         * 
         * @warning Renvoie : ArgumentNullException (si la ville n'est pas dans le *SPC*).
         */
        public Ville GetVilleById(int idVille);

        /**
         * @fn public str GetNationaliteByIdPays
         * @brief Récupère la nationalité d'un pays depuis le *SPC* avec l'id du pays.
         * 
         * @param int idPays
         * 
         * @return string - *Nationalité d'un pays chargée à partir de l'id du pays*
         * 
         * @warning Renvoie : ArgumentNullException 
         * (si la nationalite n'est pas definie dans le *SPC*, ou l'idPays n'est pas dans le *SPC*).
         */
        public string GetNationaliteByIdPays(int idPays);

        /**
         * @fn public Ville GetNationalitesByIdPersonne
         * @brief Récupère les nationalités d'une personne depuis le *SPC* avec l'id de la personne.
         * 
         * @param int idPersonne
         * 
         * @return string? - *Nationalité(s) de la personne chargée à partir*
         */
        public string? GetNationalitesByIdPersonne(int idPersonne);

    }
}
