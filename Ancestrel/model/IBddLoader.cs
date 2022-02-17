using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IBddLoader
    {
        public Arbre ChargerArbre(int idPersonne);
        public Personne GetPersonneById(int idPersonne);
        public string? GetPrenomById(int id);
        public FichierImage GetFichierImageById(int id);
        public Ville GetVilleById(int idVille);
        public string? GetNationaliteByIdPays(int idPays);
        public string? GetNationalitesByIdPersonne(int idPersonne);

    }
}
