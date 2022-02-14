using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Manager
    {
        private static Manager? _mInstance;
        public Arbre? Arbre { get; private set; }

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
                _mInstance = new();

            return _mInstance;
        }

        public void ChargerArbre(Arbre a)
        {
            Arbre = a;
        }

        public void CreerArbre(Personne p)
        {
            Arbre = new Arbre(p);
            //AjouterPersonne dans BDD
        }

        public void SupprimerPersonne(Personne p)
        {
            if(p.Id != null)
            {
                //DELETEPERSONNE(p)
                Arbre.SupprimerPersonne((int)p.Id);
            }
        }

        public void ModifierPersonne(Personne p)
        {
            //UpdatePersonne(p)
        }

        public void AjouterPere(Personne enfant, Homme pere)
        {
            int idPere = 0;
            //id=AJOUTPere
            enfant.IdPere = idPere;
            //UpdateEnfant(idPere)!

        }

        public void AjouterMere(Personne enfant, Femme mere)
        {
            int idMere=0; //
            //id=AJOUTMERE
            enfant.IdMere = idMere;
            //UpdateEnfant(idMere)
        }

    }
}
