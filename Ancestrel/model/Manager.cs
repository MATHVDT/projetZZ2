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
    }
}
