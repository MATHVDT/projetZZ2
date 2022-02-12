using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface BddSaver
    {
        public void InsererPersonne(Personne personne);
        public void InsererPrenomsPersonne(Personne personne);
        public void InsererVille(Ville ville);
        public void InsererFichierImage(FichierImage fichierImage);
    }
}
