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
         * @fn Homme 
         * @brief Constructeur de la classe Homme
         * @param uint i Identifiant de l'enfant
         */
        public Homme(uint iden, string? nom = null, string? prenoms = null,
                     DateOnly? dateNaissance = null, DateOnly? dateDeces = null) :
                base(iden, nom, prenoms, dateNaissance, dateDeces)
        {
            Identifiant = 2 * iden;
        }
    }
}
