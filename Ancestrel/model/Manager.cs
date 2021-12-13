using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Manager
    {
        public Arbre Arbre { get; private set; }

        public Manager()
        {

        }

        public void ChargerArbre(Arbre a)
        {
            Arbre = a;
        }
    }
}
