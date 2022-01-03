/**
 * @file Femme.cs
 * Fichier contenant la classe Femme
 * @author Mathieu
 * @date 30/12/2021
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
                if (value != null)
                {
                    _nomJeuneFille = value;
                    Inconnu = false;

                }
            }
        }

        /**
         * @fn public Femme 
         * @param uint iden *Identidiant de l'enfant*
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
         * Definie l'identifiant et le nom de jeune fille de la personne.
         */
        public Femme(uint iden, string? nom = null, string? prenoms = null,
                     DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
                     Ville? lieuNaissance = null, string? nationalite = null,
                     string? nomJeuneFille = null) :
                base(iden, nom, prenoms, dateNaissance, dateDeces, lieuNaissance, nationalite)
        {
            Identifiant = 2 * iden + 1;
            NomJeuneFille = NomJeuneFille;
            
        }

        /**
         * @overload public override string ToString()
         * @brief Rajoute le nom de jeune fille.
         */
        public override string ToString()
        {
            return base.ToString() + "née " +
                (NomJeuneFille is null ? "NomJeuneFilleInconnu" : NomJeuneFille);
        }
    }
}
