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
    /// Logique d'interaction pour UserControlChargement.xaml
    /// </summary>
    public partial class UserControlChargement : UserControl
    {
        private int id;
        private string nom;
        public UserControlChargement(int id, string nom)
        {
            InitializeComponent();
            this.id = id;
            this.nom = nom;
            Nom.Content = nom;
        }

    }
}
