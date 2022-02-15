using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IBddSaver
    {

        /**
         * @fn public void InsererPersonne
         * @brief Insere une personne dans le système de sauvegarde
         * 
         * @param Personne personne - *La personne à sauvegarder*
         * 
         * @details
         * Sauvegarde la personne dans le système choisit de sauvegarde. 
         * Insère la personne complètement, ie insère toutes les données en rapport avec elle.
         */
        public void InsererPersonne(Personne personne);
        public void InsererPrenomsPersonne(Personne personne);
        public void InsererVille(Ville ville);
        public void InsererFichierImage(FichierImage fichierImage);
    }
}
