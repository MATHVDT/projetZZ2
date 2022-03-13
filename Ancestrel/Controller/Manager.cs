using DataBase;
using Model;

namespace Controller
{
    public class Manager
    {
        private static Manager? _mInstance;
        public Arbre Arbre { get; private set; }
        private IBddSaver BddSaver { get; set; }

        private IBddLoader BddLoader { get; set; }


        private Manager()
        {
            _mInstance = this;
        }

        /**
        * @brief Singleton
        */
        public static Manager GetInstance()
        {
            if (_mInstance is null)
            {
                _mInstance = new();

            }

            return _mInstance;
        }

        public void ConnexionBdd(string chaineConnection)
        {
            Bdd bdd = new Bdd(chaineConnection);
            BddSaver = bdd;
            BddLoader = bdd;

        }

        public void ChargerArbre(Arbre a)
        {
            Arbre = a;
        }

        public void CreerArbre(Personne p)
        {
            Arbre = new Arbre(p);
            //BddSaver.InsererPersonne(p);
        }

        public void SupprimerPersonne(Personne p)
        {
            if (p.Id != null)
            {
                //DELETEPERSONNE(p)
                Arbre.SupprimerPersonne((int)p.Id);
            }
        }

        public void ModifierPersonne(Personne p)
        {
            //UpdatePersonne(p)
            //BddSaver.UpdatePersonne(p);
        }

        public void AjouterPere(Personne enfant, Homme pere)
        {
            BddSaver.InsererPersonne((pere as Personne));
            Arbre.AjouterParent(enfant.Numero, pere);

        }

        public void AjouterMere(Personne enfant, Femme mere)
        {
            //BddSaver.InsererPersonne((mere as Personne));
            Arbre.AjouterParent(enfant.Numero, mere);
        }

        public Personne GetPersonne(int i)
        {
            if (Arbre.Personnes.ContainsKey(i))
            {
                return Arbre.Personnes[i];
            }
            else
            {
                return null;
            }
        }
    }
}