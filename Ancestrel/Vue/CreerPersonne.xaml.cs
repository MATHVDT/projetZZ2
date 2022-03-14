using Controller;
using Model;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Vue
{
    /// <summary>
    /// Logique d'interaction pour CreerPersonne.xaml
    /// </summary>
    public partial class CreerPersonne : Page
    {
        private Manager manager = Manager.GetInstance();
        private int Numero;
        private int Index = 0;
        private bool EstNouvelArbre = true;
        private Personne personne;
        private Personne enfant;

        public CreerPersonne()
        {
            InitializeComponent();
        }

        public CreerPersonne(Personne enfant)
        {
            InitializeComponent();
            EstNouvelArbre = false;
            this.enfant = enfant;
            InitialiserVue();
        }

        private void InitialiserVue()
        {
            InitialiserTitre();
            InitialiserComboBox();
        }

        private void InitialiserTitre()
        {
            if (!EstNouvelArbre)
            {
                this.Title = $"Ajouter un Ancetre à {Numero}";
            }
        }

        private void InitialiserComboBox()
        {
            Sexe_ComboBox.SelectedIndex = Index;
            if (!EstNouvelArbre)
            {
                if (Index == 1)
                {
                    ButtonAjout.Content = "Ajouter Mère";
                }
                else
                {
                    if (Index == 0)
                    {
                        ButtonAjout.Content = "Ajouter Père";
                    }

                }
            }
        }

        private void ButtonAjout_Click(object sender, RoutedEventArgs e)
        {
            personne = Sexe_ComboBox.SelectedIndex == 0 ? new Homme() : new Femme();
            if (personne is Homme)
            {
                Debug.WriteLine("homme");
            }
            else
            {
                Debug.WriteLine("Femme");
            }
            personne.Nom = !String.IsNullOrWhiteSpace(Nom_TextBox.Text) ? Nom_TextBox.Text : null;
            foreach (Object o in SP_Prenoms.Children)
            {
                if (o.GetType().Equals(typeof(TextBox)))
                {
                    if (!string.IsNullOrWhiteSpace(((TextBox)o).Text))
                    {
                        personne.AjouterPrenoms(((TextBox)o).Text);
                    }
                }
            }
            try
            {
                if (!EstNouvelArbre)
                {
                    if (personne is Homme)
                    {
                        manager.AjouterPere(enfant, (Homme)personne);
                    }
                    else if (personne is Femme)
                    {
                        manager.AjouterMere(enfant, (Femme)personne);
                    }
                    this.NavigationService.Navigate(new Arbre());
                }
                else
                {
                    manager.ConnexionBdd($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emper\OneDrive\Documents\ISIMA\ZZ2\Projet\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True");
                    manager.CreerArbre(personne);
                    Debug.WriteLine(personne.Numero);
                    this.NavigationService.Navigate(new Arbre());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void ButtonPrenoms_Click(object sender, RoutedEventArgs e)
        {
            TextBox textBox = new TextBox
            {
                Margin = new Thickness(5),
                Width = 200,
            };
            SP_Prenoms.Children.Remove(ButtonPrenoms);
            SP_Prenoms.Children.Add(textBox);
            SP_Prenoms.Children.Add(ButtonPrenoms);
        }
    }
}



