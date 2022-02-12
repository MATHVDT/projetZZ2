using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface BddLoader
    {

        public Personne GetPersonneById(int idPersonne);
        public string? GetPrenomById(int id);
        public FichierImage GetFichierImageById(int id);
        public Ville GetVilleById(int idVille);
    }
}
