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
            throw new NotImplementedException();
        }
    }
}
