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
    /// Logique d'interaction pour CreerPersonne.xaml
    /// </summary>
    public partial class CreerPersonne : Page
    {
        private Manager manager= Manager.GetInstance();
        private int Numero;
        private int Index = 0;
        private bool EstNouvelArbre = true;
        private Personne personne;

        public CreerPersonne()
        {
            InitializeComponent();
        }

        public CreerPersonne(int numero, int index)
        {
            InitializeComponent();
            EstNouvelArbre = false;
            Numero = numero;
            Index = index;
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
                Sexe_ComboBox.IsEnabled = false;
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
            Boolean test = true;
            personne = Sexe_ComboBox.SelectedValue == "Masculin" ? new Homme(): new Femme(); 
            if (string.IsNullOrWhiteSpace(Nom_TextBox.Text))
            {
                Nom_TextBox.Background = new SolidColorBrush(Colors.Red);
                test = false;
            }
            else
            {
                personne.Nom=Nom_TextBox.Text;
                Nom_TextBox.Background = new SolidColorBrush(Colors.White);
            }
            foreach (Object o in SP_Prenoms.Children)
            {
                if (o.GetType().Equals(typeof(TextBox)))
                {
                    if (string.IsNullOrWhiteSpace(((TextBox)o).Text))
                    {
                        ((TextBox)o).Background = new SolidColorBrush(Colors.Red);
                        test = false;
                    }
                    else
                    {
                        personne.AjouterPrenoms(((TextBox)o).Text);
                        ((TextBox)o).Background = new SolidColorBrush(Colors.White);
                    }
                }
            }
            if (test)
            {
                try
                {
                    if (!EstNouvelArbre)
                    {
                        if (Sexe_ComboBox.SelectedIndex == 0)
                        {
                            //ajouterPere
                            throw new NotImplementedException();
                        }
                        else
                        {
                            if (Sexe_ComboBox.SelectedIndex == 1)
                            {
                                //AjouterMere
                                throw new NotImplementedException();
                            }
                            else
                            {
                                test = false;
                            }
                        }
                    }
                    else
                    {
                        manager.CreerArbre(personne);
                        Debug.WriteLine(personne.Numero);
                        this.NavigationService.Navigate(new Arbre());
                    }
                }
                catch (Exception exc)
                {
                    test = false;
                    MessageBox.Show(exc.Message);
                }
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



