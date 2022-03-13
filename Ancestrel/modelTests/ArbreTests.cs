using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Model.Tests
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
            //arbre.AjouterMere(1, "Marie");
            Femme f = new(prenoms: "Marie");
            arbre.AjouterMere(1, f);
            Assert.IsNotNull(arbre.Personnes[1].GetMereNumero());
            //arbre.AjouterPere(1, "Richard");
            Homme h = new(prenoms: "Richard");
            arbre.AjouterPere(1, h);
            Assert.IsNotNull(arbre.Personnes[1].GetPereNumero());
            Assert.AreEqual(arbre.Personnes.Count(), 3);
            arbre.SupprimerPersonne(arbre.Personnes[1].GetPereNumero());
            Assert.AreEqual(arbre.Personnes.Count(), 2);
        }
    }
}