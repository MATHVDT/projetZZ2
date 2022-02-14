/**
 * @file Homme.cs
 * Fichier contenant la classe Homme
 * @author Mathieu
 * @date 30/12/2021
 * @copyright ...
 */
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
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
         * @param int num *Numero de l'enfant*
         * @param int? id = null *Id de la personne dans la BDD*
         * @param int? idPere = null *Id du pere dans la BDD*
         * @param int? idMere = null *Id de la mere dans la BDD*
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
        public Homme(int num, int? id = null, int? idPere = null, int? idMere = null,
                     string? nom = null, string? prenoms = null,
                     DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
                     Ville? lieuNaissance = null, string? nationalite = null) :
                base(2 * num, id, idPere, idMere, nom, prenoms, dateNaissance, dateDeces, lieuNaissance, nationalite)
        {
            //Numero = 2 * iden;
        }

        public Homme(int num, int? id = null,
                     string? nom = null, string? prenoms = null,
                     DateOnly? dateNaissance = null, DateOnly? dateDeces = null,
                     Ville? lieuNaissance = null, string? nationalite = null)
            : this(num, id, null, null, nom, prenoms, dateNaissance, dateDeces, lieuNaissance, nationalite)
        { }
    }
}
