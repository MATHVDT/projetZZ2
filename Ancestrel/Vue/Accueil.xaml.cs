using System.Windows;
using System.Windows.Controls;

namespace Vue
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        public Accueil()
        {
            InitializeComponent();
        }

        private void CreerArbre_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreerPersonne());
        }

        private void ChargerArbre_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Chargement());
        }
    }
}
