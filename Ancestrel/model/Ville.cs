using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Ville
    {
        public string Nom { get; set; }
        public double Longitude { get; set; }

        public double Latitude { get; set; }
    
        //public GeoCoordinate Coordonnees { get; set; }

        public Ville(string nom, double longitude, double latitude)
        {
            this.Nom = nom;
            this.Longitude = longitude;
            this.Latitude = latitude;
        }
    }
}
