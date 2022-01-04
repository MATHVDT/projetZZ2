using Microsoft.VisualStudio.TestTools.UnitTesting;
using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests
{
    [TestClass()]
    public class UnitTestPersonne
    {

        [TestMethod()]
        public void Homme()
        {
            // Test Homme Inconnu
            Homme h = new Homme(1);

            Assert.IsNotNull(h);

            Assert.AreEqual(h.Identifiant, Convert.ToUInt32(2));
            Assert.IsNull(h.Nom);
            Assert.IsNull(h.Prenoms);   
            Assert.IsNull(h.DateNaissance);
            Assert.IsNull(h.DateDeces);
            Assert.IsNull(h.LieuNaissance);
            Assert.IsNull(h.Nationalite);
            Assert.IsNull(h.IndexImageProfil);

            Assert.IsTrue(h.Inconnu);


            
        }
    }

}

            /*
            Arbre arbre = new Arbre("arbre1", "arbre de test", new Homme(0, "Joe"));
            Assert.IsNotNull(arbre);
            Assert.IsNotNull(arbre.Personnes[1]);
            arbre.AjouterMere(1, "Marie");
            Assert.IsNotNull(arbre.Personnes[1].GetMereId());
            arbre.AjouterPere(1, "Richard");
            Assert.IsNotNull(arbre.Personnes[1].GetPereId());
            Assert.AreEqual(arbre.Personnes.Count(), 3);
            arbre.SupprimerPersonne(arbre.Personnes[1].GetPereId());
            Assert.AreEqual(arbre.Personnes.Count(), 2);
            */