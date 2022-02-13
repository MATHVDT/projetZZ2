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
            Nom.Content = personne.Nom;
            if(personne.Prenoms != null)
            {
                Prenom.Content = personne.Prenoms;
            }
            Naissance.Content = personne.DateNaissance;
            Deces.Content = personne.DateDeces;
            //Portrait.Source = personne.Portrait;
        }
    }
}
