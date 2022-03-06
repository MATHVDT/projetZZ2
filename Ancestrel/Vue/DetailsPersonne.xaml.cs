using Model;
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
    /// Logique d'interaction pour DetailsPersonne.xaml
    /// </summary>
    public partial class DetailsPersonne : Page
    {
        private Personne personne;
        private const string _INCONNUE = "Inconnue";
        private const string _INCONNU = "Inconnu";
        public DetailsPersonne(Personne p)
        {
            InitializeComponent();
            personne = p;
            InitialiserVues();
        }

        private void InitialiserVues()
        {
            ModifierVisibiliteLabel(Visibility.Visible);
            ModifierVisibiliteTextBox(Visibility.Collapsed);
            Label_Nom.Content = personne.Nom != null ? personne.Nom : _INCONNU;
            Label_Prenom.Content = personne.Prenoms != null ? personne.Prenoms : _INCONNU;
            Label_Date_Dc.Content = personne.DateDeces != null ? personne.DateDeces : _INCONNUE;
            Label_Date_Naissance.Content = personne.DateNaissance != null ? personne.DateNaissance : _INCONNUE;
            ///////Gestion image : convertion image
            ImagePrincipale.Source = personne is Homme ? new BitmapImage(new Uri(@"/Homme.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"/Femme.png", UriKind.RelativeOrAbsolute));
            if(personne is Femme)
            {
                Label_Nom_JF.Content = ((Femme)personne).NomJeuneFille != null ? ((Femme)personne).NomJeuneFille : _INCONNU;
                SP_Nom_JF.Visibility = Visibility.Visible;
            }
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Arbre());
        }

        private void BTModifier_Click(object sender, RoutedEventArgs e)
        {
            ModifierVisibiliteLabel(Visibility.Collapsed);
            ModifierVisibiliteTextBox(Visibility.Visible);
            RemplirTextBox();
        }

        private void RemplirTextBox()
        {
            Nom.Text = personne.Nom != null ? personne.Nom : "";
            Prenom.Text = personne.Prenoms != null ? personne.Prenoms : "";
            if(personne is Femme)
            {
                NomJF.Text = ((Femme)personne).NomJeuneFille != null ? ((Femme)personne).NomJeuneFille : "";
            }
            Date_Naissance.SelectedDate = personne.DateNaissance;
            Date_Dc.SelectedDate = personne.DateDeces;
        }

        private void ModifierVisibiliteTextBox(Visibility visibility)
        {
            Nom.Visibility = visibility;
            NomJF.Visibility = visibility;
            Prenom.Visibility = visibility;
            Date_Naissance.Visibility = visibility;
            Date_Dc.Visibility = visibility;
            BTValider.Visibility = visibility;
        }

        private void ModifierVisibiliteLabel(Visibility visibility)
        {
            Label_Date_Dc.Visibility = visibility;
            Label_Date_Naissance.Visibility = visibility;
            Label_Nom.Visibility = visibility;
            Label_Nom_JF.Visibility = visibility;
            Label_Prenom.Visibility = visibility;
            BTModifier.Visibility = visibility;
        }

        private void BTValider_Click(object sender, RoutedEventArgs e)
        {
            personne.Nom = Nom.Text;
            personne.Prenoms = Prenom.Text;
            if(personne is Femme)
            {
                ((Femme)personne).NomJeuneFille = NomJF.Text;
            }
            personne.DateDeces = Date_Dc.SelectedDate;
            personne.DateNaissance = Date_Naissance.SelectedDate;
            InitialiserVues();
        }

        private void BTImages_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
