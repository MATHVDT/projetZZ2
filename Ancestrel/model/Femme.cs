/**
 * @file Femme.cs
 * Fichier contenant la classe Femme
 * @author Mathieu
 * @date 30/12/2021
 * @copyright ...
 */
using System;

namespace Model
{
    /**
     * @class Femme
     * @brief Classe pour une femme.
     * @details
     * Classe hérité de Personne.
     */
    public class Femme : Personne
    {
        private string? _nomJeuneFille;

        /**
         * @var NomJeuneFille
         * @brief Nom de jeune fille de la personne.
         * @warning Peut être null.
         */
        public string? NomJeuneFille
        {
            get => _nomJeuneFille;
            set
            {

                _nomJeuneFille = value;
                Inconnu = _estInconnu();

            }
        }

        /**
         * @fn public Femme 
         * @param int num *Numero de l'enfant*
         * @param int? id = null *Id de la personne dans la BDD*    
         * @param string? nom = null,
         * @param string? prenoms = null
         * @param DateOnly? dateNaissance = null
         * @param DateOnly? dateDeces = null
         * @param Ville? lieuNaissance = null
         * @param string? nationalite = null
         * @param string? nomJeuneFille
         * 
         * @brief Constructeur de la classe Femme.
         * @details
         * Definie les propiétés de la personne.
         * Definie le Numero et le nom de jeune fille de la personne.
         */
        public Femme(int? id = null,
                     string? nom = null, string? prenoms = null,
                     DateTime? dateNaissance = null, DateTime? dateDeces = null,
                     Ville? lieuNaissance = null, string? nationalite = null,
                     string? nomJeuneFille = null, string? description = null) :
                base(id: id, nom: nom, prenoms: prenoms, dateNaissance: dateNaissance, dateDeces: dateDeces,
                    lieuNaissance: lieuNaissance, nationalite: nationalite, description: description)
        {
            NomJeuneFille = nomJeuneFille;
        }


        /**
         * @overload public override string ToString()
         * @brief Rajoute le nom de jeune fille.
         */
        public override string ToString()
        {
            return base.ToString() + " née " +
                (NomJeuneFille is null ? "NomJeuneFilleInconnu " : NomJeuneFille);
        }



        /**
         * @fn public void SupprimerNomJeuneFille()
         * @brief Supprime le nom de jeune fille de la personne.
         * @details
         * Supprime le nom de jeune fille de la personne,
         * et maintient à jour la propriete *Inconnu*. 
         */
        public void SupprimerNomJeuneFille()
        {
            NomJeuneFille = null;
            Inconnu = _estInconnu();
        }



        public override void LierEnfant(int numEnfant)
        {
            this.Numero = 2 * numEnfant + 1;
        }
    }
}
