/**
 * @file Homme.cs
 * Fichier contenant la classe Homme
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
     * @class Homme
     * @brief Classe pour une homme.
     * @details
     * Classe hérité de Personne.
     */
    public class Homme : Personne
    {

        /**
         * @fn public Homme 
         * @param uint iden *Identidiant de l'enfant*
         * @param string? nom = null,
         * @param string? prenoms = null
         * @param DateOnly? dateNaissance = null
         * @param DateOnly? dateDeces = null
         * @param Ville? lieuNaissance = null
         * @param string? nationalite = null
         * 
         * @brief Constructeur de la classe Homme.
         * @details
         * Definie les propiétés de la personne.
         */
        public Homme(uint iden, string? nom = null, string? prenoms = null,
                     DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
                     Ville? lieuNaissance = null, string? nationalite = null) :
                base(2*iden, nom, prenoms, dateNaissance, dateDeces, lieuNaissance, nationalite)
        {
            //Identifiant = 2 * iden;
        }
    }
}
