using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /**
     * @interface public IBddSaver
     * @brief Interface Pour la sauvegarde de données.
     * 
     * @details
     * Interface à implementer pour la sauvegarde des données
     * dans le Système de Persistance Choisie *(SPC)*.
     */
    public interface IBddSaver
    {

        public void InsererArbre(Arbre arbre);

        /**
         * @fn public void InsererPersonne
         * @brief Insere une personne dans le *SPC*
         * 
         * @param Personne personne - *La personne à sauvegarder*
         * 
         * @details
         * Sauvegarde la personne dans le *SPC*. 
         * Insère la personne complètement, ie insère toutes les données en rapport avec elle.
         * 
         * @warning La personne ne doit pas être null.
         */
        public void InsererPersonne(Personne personne);


        /**
         * @fn public void InsererPrenomsPersonne
         * @brief Insère les prenoms de la personne le *SPC*
         * 
         * @param Personne personne
         * 
         * @warning La personne ne doit pas être null.
         */
        public void InsererPrenomsPersonne(Personne personne); // a degager

        /**
        * @fn public void InsererVille
        * @brief Ajout une ville dans le *SPC*
        * 
        * @param Ville ville
        * 
        * @Details
        * Ajoute la ville dans le *SPC* et lui donne un *id*. 
        * Cet Id est ajouter à l'objet.
        * 
        * @warning La ville ne doit pas être null.
        */
        public void InsererVille(Ville ville);

        /**
         * @fn public void InsererFichierImage
         * @brief Ajout une image de le *SPC*
         * 
         * @param FichierImage fichierImage
         * 
         * @Details
         * Ajoute l'image dans le *SPC* et lui donne un *id*. 
         * Cet Id est ajouter à l'objet.
         * 
         * @warning Le fichier image ne doit pas être null.
         */
        public void InsererFichierImage(FichierImage fichierImage);

        /**
         * @fn public void AjouterLienParenteById
         * @brief Ajout l'id du pere et de la mere à l'enfant dans le *SPC*
         * 
         * @param int idEnfant
         * @param int? idPere
         * @param int? idMere
         * 
         * @warning L'idEnfant ne doit pas être null.
         */
        public void AjouterLienParenteById(int idEnfant, int? idPere, int? idMere);


        /**
         * @fn public void AjouterLienParente
         * @brief Ajout l'id de la personne comme parent à son enfant dans le *SPC*
         * 
         * @param Personne personne
         * 
         * @warning 
         */
        public void AjouterLienParente(Personne personne);
    }
}
