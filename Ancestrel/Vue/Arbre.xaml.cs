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
    /// Logique d'interaction pour Arbre.xaml
    /// </summary>
    public partial class Arbre : Page
    {
        private Manager manager = Manager.GetInstance();

        public Arbre()
        {
            InitializeComponent();
            ConstruireArbre();
        }

        private void ConstruireArbre()
        {
            GridArbre.RowDefinitions.Clear();
            GridArbre.ColumnDefinitions.Clear();
            GridArbre.Children.Clear();
            int Nmax = manager.Arbre.GetNumeroMax();


            int HauteurArbre = (int)(Math.Log(Nmax) / Math.Log(2)) + 1;


            int NombreColonne = (int)Math.Pow(2, HauteurArbre);


            for (int i = 0; i <= Math.Max(NombreColonne, HauteurArbre); i++)
            {
                if (i < NombreColonne)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    GridArbre.ColumnDefinitions.Add(column);
                }
                if (i <= HauteurArbre)
                {
                    RowDefinition row = new RowDefinition();


                    GridArbre.RowDefinitions.Add(row);
                }
            }


            Debug.WriteLine($"Nmax:{Nmax}, Hauteur:{HauteurArbre}, Colonne:{NombreColonne}");

            for (int i = 1; i <= Math.Pow(2, HauteurArbre + 1) - 1; i++)
            {
                int ligne = CalculerNumLigne(i);
                int span = CalculerSpan(NombreColonne, ligne);
                int colonne = CalculerNumCol(i, span, ligne);


                UIElement o = null;
                Personne p = manager.GetPersonne(i);
                if (p != null)
                {
                    o = new UCPersonne(p, this)
                    {
                        Width = 320,
                        Height = 150,
                    };
                }
                else
                {
                    int numero = i;
                    int numeroFils = (int)(i / 2);
                    Personne enfant = manager.GetPersonne(numeroFils);
                    if (enfant != null)
                    {
                        o = new Button()
                        {
                            Content = "+",
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Width = 50,
                            Height = 50,

                        };
                        ((Button)o).Click += (sender, args) =>
                        {
                            this.NavigationService.Navigate(new CreerPersonne(enfant));
                        };
                        ((Button)o).SetResourceReference(Control.StyleProperty, "RoundCorner");

                    }
                }
                if (o != null)
                {
                    GridArbre.Children.Add(o);
                    Grid.SetRow(o, ligne);
                    Grid.SetColumn(o, colonne);
                    Grid.SetColumnSpan(o, span);
                }

            }


            /*foreach (Personne P in manager.Arbre.Personnes.Values)
            {
                UCPersonne UCPersonne 
                int ligne = CalculerNumLigne(P.Numero);
                int span = CalculerSpan(NombreColonne, ligne);
                int colonne = CalculerNumCol(P.Numero, span, ligne);

                GridArbre.Children.Add(UCPersonne);
                Grid.SetRow(UCPersonne, ligne);
                Grid.SetColumn(UCPersonne, colonne);
                Grid.SetColumnSpan(UCPersonne, span);
            }*/
        }

        private int CalculerSpan(int nbcolonne, int nligne)
        {
            return (int)(nbcolonne / Math.Pow(2, nligne));
        }

        private int CalculerNumLigne(int numero)
        {
            return (int)(Math.Log(numero) / Math.Log(2));
        }

        private int CalculerNumCol(int numero, int span, int nligne)
        {
            return span * (numero % (int)Math.Pow(2, nligne));
        }
    }
}
