using Microsoft.VisualStudio.TestTools.UnitTesting;
using model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * @namespace namespace ModelTests
 * Namespace contenant les classes de tests des differentes classes.
 */
namespace ModelTests
{
    /*
     * @class public class UnitTestPersonne
     * @brief [TestClass] Classe Personne
     */
    [TestClass]
    public class UnitTestPersonne
    {

        /*
         * @fn public void PersonneInconnueTest()
         * @brief [TestMethod] Création personne inconnue.
         */
        [TestMethod]
        public void PersonneInconnueTest()
        {
            // Test Homme Inconnu
            Homme h = new Homme(1);

            Assert.IsNotNull(h); // Bonne création de l'objet
            // Vérification des propiétés à null
            Assert.AreEqual(h.Numero, Convert.ToUInt32(2));
            Assert.IsNull(h.Nom);
            Assert.IsNull(h.Prenoms);
            Assert.IsNull(h.DateNaissance);
            Assert.IsNull(h.DateDeces);
            Assert.IsNull(h.LieuNaissance);
            Assert.IsNull(h.Nationalite);
            Assert.IsNull(h.GetFichierImageProfil());
            Assert.IsNull(h.GetImageProfil());

            Assert.IsTrue(h.Inconnu);

            // Test Femme Inconnu
            Femme f = new Femme(1);

            Assert.IsNotNull(f); // Bonne création de l'objet
            // Vérification des propiétés à null
            Assert.AreEqual(f.Numero, Convert.ToUInt32(2+1));
            Assert.IsNull(f.Nom);
            Assert.IsNull(f.Prenoms);
            Assert.IsNull(f.DateNaissance);
            Assert.IsNull(f.DateDeces);
            Assert.IsNull(f.LieuNaissance);
            Assert.IsNull(f.Nationalite);
            Assert.IsNull(f.GetFichierImageProfil());
            Assert.IsNull(f.GetImageProfil());

            Assert.IsTrue(f.Inconnu);

        }

        /*
         * @fn public void InstanciationHommeTest()
         * @brief [TestMethod] Instaciation Homme.
         */
        [TestMethod]
        public void InstanciationHommeTest()
        {
            uint i = 5;

            string nom = "Dupond";
            string prenoms = "Jean Toto";
            DateOnly dateNaissance = new DateOnly(2000,02,16);
            DateOnly dateDeces = new DateOnly(2048,02,28);
            Ville villeNaissance = new Ville("Dijon");
            string nationalite = "fr";


            Homme h = new Homme(i, null,nom, prenoms, dateNaissance,dateDeces,villeNaissance,nationalite); ;
            Assert.IsNotNull(h);
            Assert.AreEqual((uint)2*i, h.Numero);
            Assert.AreEqual( nom, h.Nom);    
            Assert.AreEqual(prenoms, h.Prenoms);
            Assert.AreEqual(dateNaissance, h.DateNaissance);
            Assert.AreEqual(dateDeces, h.DateDeces);    
            Assert.AreEqual(villeNaissance, h.LieuNaissance);
            Assert.AreEqual(nationalite, h.Nationalite);
        }

        /*
         * @fn public void InstanciationFemmeTest()
         * @brief [TestMethod] Instaciation Femme.
         */
        [TestMethod]
        public void InstanciationFemmeTest()
        {
            uint i = 5;

            string nom = "Wayne";
            string prenoms = "Diana";
            DateOnly dateNaissance = new DateOnly(2000, 02, 16);
            DateOnly dateDeces = new DateOnly(2048, 02, 28);
            Ville villeNaissance = new Ville("Dijon");
            string nationalite = "fr";

            string nomJeuneFille = "Price";

            Femme f = new Femme(i,null, nom, prenoms, 
                dateNaissance, dateDeces, villeNaissance, nationalite,
                nomJeuneFille); ;
            Assert.IsNotNull(f);
            Assert.AreEqual((uint)2*i+1, f.Numero);
            Assert.AreEqual(nom, f.Nom);
            Assert.AreEqual(prenoms, f.Prenoms);
            Assert.AreEqual(dateNaissance, f.DateNaissance);
            Assert.AreEqual(dateDeces, f.DateDeces);
            Assert.AreEqual(villeNaissance, f.LieuNaissance);
            Assert.AreEqual(nationalite, f.Nationalite);
            Assert.AreEqual(nomJeuneFille, f.NomJeuneFille);
        }






        /*
         * @fn public void SetPrenomsTest()
         * @brief [TestMethod] Setter de prénoms.
         * @details
         * Test les setters classiques de la propriété *Prenoms*,
         * et aussi la fonction *SetPrenom*.
         */
        [TestMethod]
        public void SetPrenomsTest()
        {
            Personne p = new Homme(1);

            // Definit les prenoms
            p.Prenoms = "    Peter    ";
            Assert.AreEqual(p.Prenoms, "Peter");
            Assert.IsFalse(p.Inconnu);

            p.Prenoms = "";
            Assert.IsNull(p.Prenoms);
            Assert.IsTrue(p.Inconnu);

            p.Prenoms = "Peter";
            Assert.AreEqual(p.Prenoms, "Peter");
            Assert.IsFalse(p.Inconnu);

            p.Prenoms = null;
            Assert.IsNull(p.Prenoms);
            Assert.IsTrue(p.Inconnu);

            p.Prenoms = "Charles Xavier";
            Assert.AreEqual(p.Prenoms, "Charles Xavier");
            Assert.IsFalse(p.Inconnu);

            string[] vide = new string[] { };
            p.SetPrenoms(vide);
            Assert.IsNull(p.Prenoms);
            Assert.IsTrue(p.Inconnu);

            p.SetPrenoms(" Tony  ");
            Assert.AreEqual(p.Prenoms, "Tony");
            Assert.IsFalse(p.Inconnu);

            string[] listePrenoms = new string[] { "Ulfnir", "Landiv" };
            p.SetPrenoms(listePrenoms);
            Assert.AreEqual(p.Prenoms, "Ulfnir Landiv");
            Assert.IsFalse(p.Inconnu);

            string[] listePrenomsEspace =
                new string[] { "Ulfnir  ", "    Landiv" };
            p.SetPrenoms(listePrenomsEspace);
            Assert.AreEqual(p.Prenoms, "Ulfnir Landiv");
            Assert.IsFalse(p.Inconnu);

        }


        /*
         * @fn public void AddPrenomTest()
         * @brief [TestMethod] Ajoute prenoms.
         * @details
         * Test l'ajout de prénom avec la methode *AddPrenoms*,
         * avec string ou string[]. Test pas d'ajout de prenoms en double.
         */
        [TestMethod]
        public void AddPrenomTest()
        {
            Personne p = new Femme(1);

            Assert.IsNull(p.Prenoms);
            Assert.IsTrue(p.Inconnu);

            // Ajoute un prenom
            p.AddPrenoms("   Charles   ");
            Assert.AreEqual(p.Prenoms, "Charles");
            Assert.IsFalse(p.Inconnu);

            p.AddPrenoms("Xavier    ");
            Assert.AreEqual(p.Prenoms, "Charles Xavier");
            Assert.IsFalse(p.Inconnu);

            p.AddPrenoms("  Xavier  ");
            Assert.AreEqual(p.Prenoms, "Charles Xavier");
            Assert.IsFalse(p.Inconnu);

            // Reset les prénoms
            string[] vide = new string[] { };
            p.Prenoms = "";
            p.AddPrenoms(vide);
            Assert.IsNull(p.Prenoms);
            Assert.IsTrue(p.Inconnu);

            // Ajoute prénoms avec string[]
            string[] liste1 = new string[] { "   Robert  ", "John " };
            p.AddPrenoms(liste1);
            Assert.AreEqual(p.Prenoms, "Robert John");
            Assert.IsFalse(p.Inconnu);

            string[] liste2 = new string[] { "  Junior ", "Robert " };
            p.AddPrenoms(liste2);
            Assert.AreEqual(p.Prenoms, "Robert John Junior");
            Assert.IsFalse(p.Inconnu);

            p.AddPrenoms(vide);
            Assert.AreEqual(p.Prenoms, "Robert John Junior");
            Assert.IsFalse(p.Inconnu);
        }

        /*
         * @fn public SupprimerPrenomsTest()
         * @brief [TestMethod] Suppression de prénoms.
         * @details
         * Test les differents mode de suppression.
         */
        [TestMethod]
        public void SupprimerPrenomsTest()
        {
            Personne p = new Homme(1);

            Assert.IsNull(p.Prenoms);
            Assert.IsTrue(p.Inconnu);

            // Définit des prénoms : "Prof Dormeur Simplet Timide Joyeux Grincheux Atchoum"
            p.SetPrenoms("Prof Dormeur Simplet Timide Joyeux Grincheux Atchoum");
            Assert.AreEqual("Prof Dormeur Simplet Timide Joyeux Grincheux Atchoum", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            // Suppression de prénoms spécifiques
            p.SupprimerPrenomsSpecifique("    Dormeur  Joyeux    ");
            Assert.AreEqual("Prof Simplet Timide Grincheux Atchoum", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            p.SupprimerPrenomsSpecifique("");
            Assert.AreEqual("Prof Simplet Timide Grincheux Atchoum", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            string[] vide = new string[] { };
            p.SupprimerPrenomsSpecifique(vide);
            Assert.AreEqual("Prof Simplet Timide Grincheux Atchoum", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            string[] liste1 = new string[] { "  Prof", "Timide" };
            p.SupprimerPrenomsSpecifique(liste1);
            Assert.AreEqual("Simplet Grincheux Atchoum", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            p.SupprimerPrenomsSpecifique("    Atchoum   Simplet ");
            Assert.AreEqual("Grincheux", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            // Suppression prénom non présent
            p.SupprimerPrenomsSpecifique("Toto");
            Assert.AreEqual("Grincheux", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            // Suppression de tous les prénoms
            p.SupprimerPrenoms();
            Assert.IsNull(p.Prenoms);
            Assert.IsTrue(p.Inconnu);

            // Définit des prénoms : "Joe William Jack Averell"
            p.SetPrenoms("Joe William Jack Averell");
            Assert.AreEqual("Joe William Jack Averell", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            // Suppression Position 2 : Jack
            p.SupprimerPrenomPosition(2);
            Assert.AreEqual("Joe William Averell", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            // Suppression en dehors des indices possible
            p.SupprimerPrenomPosition(-5);
            p.SupprimerPrenomPosition(48);
            Assert.AreEqual("Joe William Averell", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

            // Suppression dernier prénom
            p.SupprimerDernierPrenom();
            Assert.AreEqual("Joe William", p.Prenoms);
            Assert.IsFalse(p.Inconnu);

        }



    }

}

