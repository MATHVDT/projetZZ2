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
    /// Logique d'interaction pour CreerPersonne.xaml
    /// </summary>
    public partial class CreerPersonne : Page
    {
        private int Numero;
        private int Index = 0;
        private bool EstNouvelArbre = true;

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

            if (string.IsNullOrWhiteSpace(Nom_TextBox.Text))
            {
                Nom_TextBox.Background = new SolidColorBrush(Colors.Red);
                test = false;
            }
            else
            {
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
                        //CreerArbre
                        throw new NotImplementedException();
                    }
                }
                catch (Exception exc)
                {
                    test = false;
                    MessageBox.Show(exc.Message);
                }
            }
            if (test)
            {
                if (EstNouvelArbre)
                {
                    //nouvellePage
                    throw new NotImplementedException();
                }
                else
                {
                    //Page Arbre
                    throw new NotImplementedException();
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



