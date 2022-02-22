using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vue
{
    /// <summary>
    /// Logique d'interaction pour UCPersonne.xaml
    /// </summary>
    public partial class UCPersonne : UserControl
    {
        private const string _INCONNUE = "Inconnue";
        private Personne personne;
        private Page page;
        public UCPersonne(Personne p, Page page)
        {
            InitializeComponent();
            this.page = page;
            this.Cursor = Cursors.Hand;
            personne = p;
            AffectationValeur();
        }

        private void AffectationValeur()
        {
            if (personne.Inconnu)
            {
                Nom.Content = _INCONNUE;
                Prenom.Content = "";
                Naissance.Content = "";
                Deces.Content = "";
            }
            else
            {
                Nom.Content = personne.Nom != null ? personne.Nom : _INCONNUE;
                Prenom.Content = personne.Prenoms != null ? personne.Prenoms : _INCONNUE;
                Naissance.Content = personne.DateNaissance != null ? personne.DateNaissance : _INCONNUE;
                Deces.Content = personne.DateDeces != null ? personne.DateDeces : _INCONNUE;
                Portrait.Source = new BitmapImage(new Uri(@"/Homme.png", UriKind.RelativeOrAbsolute));
                //Portrait = personne.GetFichierImageProfil().Image; //Utiliser un converter
            }
            if(personne.GetImageProfil() != null)
            {
                throw new NotImplementedException();
            }
            else
            {
                Portrait.Source = personne is Homme ? new BitmapImage(new Uri(@"/Homme.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"/Femme.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
