using Model;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Vue
{
    /// <summary>
    /// Logique d'interaction pour UCPersonne.xaml
    /// </summary>
    public partial class UCPersonne : UserControl
    {
        private const string _INCONNUE = "Inconnue";
        private const string _INCONNU = "Inconnu";
        private Personne personne;
        private Page page;
        bool button = false;
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
                Numero.Visibility = Visibility.Collapsed;
                Nom.Content = _INCONNU;
                Prenom.Visibility = Visibility.Collapsed;
                Naissance.Visibility = Visibility.Collapsed;
                Deces.Visibility = Visibility.Collapsed;
                Separateur.Visibility = Visibility.Collapsed;
            }
            else
            {
                Numero.Content = personne.Numero;
                Nom.Content = personne.Nom != null ? personne.Nom : _INCONNU;
                Prenom.Content = personne.Prenoms != null ? personne.Prenoms : _INCONNU;
                Naissance.Content = personne.DateNaissance != null ? ((DateTime)personne.DateNaissance).ToString("DD/'MM'/'YYYY'") : _INCONNUE;
                Deces.Content = personne.DateDeces != null ? ((DateTime)personne.DateDeces).ToString("DD/'MM'/'YYYY'") : _INCONNUE;
            }
            if (personne.GetImageProfil() != null)
            {

                Portrait.Source = ToBitmapImage(personne.GetFichierImageProfil().Image);

            }
            else
            {
                Portrait.Source = personne is Homme ? new BitmapImage(new Uri(@"/Homme.png", UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"/Femme.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            button = true;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (button)
            {
                page.NavigationService.Navigate(new DetailsPersonne(personne));
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            button = false;
        }


        private BitmapImage ToBitmapImage(System.Drawing.Image bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
