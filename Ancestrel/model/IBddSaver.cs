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

        #region Insertion
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
         * Ne pas utiliser, ne met pas à jour les idPere/Mere dans l'objet enfant
         * Pas trop grave mais préferé AjouterLienParent(Personne personne)
         */
        public void AjouterLienParents(int idEnfant, int? idPere, int? idMere);


        /**
         * @fn public void AjouterLienParente
         * @brief Ajout l'id de la personne comme parent à son enfant dans le *SPC*
         * 
         * @param Personne personne
         * 
         * @warning Renvoie ArgumentNullException
         *  - si la personne n'a pas de numéro, ie pas placé dans l'arbre
         *  - si l'enfant de la personne n'est pas dans l'arbre
         */
        public void AjouterLienParent(Personne personne);
        #endregion

        #region Update

        /**
         * @fn public void UpdatePersonne
         * @brief Modifie une personne dans le *SPC*
         * 
         * @param Personne personne
         */
        public void UpdatePersonne(Personne personne);

        /**
         * @fn public void UpdateVille
         * @brief Modifie une ville dans le *SPC*
         * 
         * @param Ville ville 
         */
        public void UpdateVille(Ville ville);

        /**
         * @fn public void UpdateFichierImage
         * @brief Modifie une image dans le *SPC*
         * 
         * @param FichierImage fichierImage
         * 
         * @warning Pas encore implémenté, pas de modif d'image pour l'instant.
         */
        public void UpdateFichierImage(FichierImage fichierImage);

        #endregion
    }
}
