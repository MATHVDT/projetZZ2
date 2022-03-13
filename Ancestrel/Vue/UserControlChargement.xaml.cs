using System.Windows.Controls;

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
