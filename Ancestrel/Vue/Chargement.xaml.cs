using Controller;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Vue
{
    /// <summary>
    /// Logique d'interaction pour Chargement.xaml
    /// </summary>
    public partial class Chargement : Page
    {

        Manager manager = Manager.GetInstance();

        public Chargement()
        {
            InitializeComponent();
            manager.ConnexionBdd($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emper\OneDrive\Documents\ISIMA\ZZ2\Projet\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True");
            InitialiserVue();
        }

        private void InitialiserVue()
        {
            Dictionary<int, string> d = new Dictionary<int, string>();
            d = manager.ChargerPersonnes();
            foreach(KeyValuePair<int, string> kvp in d)
            {
                Button button = new Button()
                {
                    Content = kvp.Value,
                };
                button.Click += (o, i) =>
                {
                    manager.ChargerArbre(kvp.Key);
                    this.NavigationService.Navigate(new Arbre());
                };
                SP_Chargement.Children.Add(button);

            }
        }
    }
}
