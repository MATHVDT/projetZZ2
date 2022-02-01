using Microsoft.VisualStudio.TestTools.UnitTesting;
using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.Tests
{
    [TestClass()]
    public class ArbreTests
    {
        [TestMethod()]
        public void ArbreTest()
        {
            Arbre arbre = new Arbre("arbre1", "arbre de test", new Homme(0, null, "Joe"));
            Assert.IsNotNull(arbre);
            Assert.IsNotNull(arbre.Personnes[1]);
            arbre.AjouterMere(1, "Marie");
            Assert.IsNotNull(arbre.Personnes[1].GetMereId());
            arbre.AjouterPere(1, "Richard");
            Assert.IsNotNull(arbre.Personnes[1].GetPereId());
            Assert.AreEqual(arbre.Personnes.Count(), 3);
            arbre.SupprimerPersonne(arbre.Personnes[1].GetPereId());
            Assert.AreEqual(arbre.Personnes.Count(), 2);
        }
    }
}