using model;
using System;
using System.Collections.Generic;
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
        public UCPersonne(Personne p)
        {
            InitializeComponent();
            this.Cursor = Cursors.Hand;
            personne = p;
            AffectationValeur();
        }

        private void AffectationValeur()
        {
            if (personne.Inconnu)
            {
                Portrait.Source = personne is Homme ? new BitmapImage(new Uri(@"Images/Homme.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"Images/Femme.png", UriKind.RelativeOrAbsolute));
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
                //Portrait = personne.GetFichierImageProfil().Image; //Utiliser un converter
            }
        }
    }
}
