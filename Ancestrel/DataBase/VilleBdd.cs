using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    internal class VilleBdd
    {
        private readonly string _chaineConnexion;

        private readonly string _VilleTable = "dbo.Ville";

        private readonly string _Id = "Id";
        private readonly string _Code = "Code";
        private readonly string _Nom = "Nom";
        private readonly string _Latitude = "Latitude";
        private readonly string _Longitude = "Longitude";
        private readonly string _Pays = "Pays";


        public VilleBdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }


    }
}
