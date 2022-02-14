using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BDD
{
    public class VilleBdd
    {
        static readonly string _Ville = "dbo.Ville"; // Nom de la table

        static readonly string _Id = "Id";              // Nom de la colonne id
        static readonly string _Nom = "Nom";            // Nom de la colonne nom
        static readonly string _Latitude = "Latitude";  // Nom de la colonne latitude
        static readonly string _Longitude = "Longitude"; // Nom de la colonne longitude



        public static Ville GetVille(int id)
        {
            string chaineConnexion = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\DataBase.mdf;Integrated Security=True";
            string queryString = $"SELECT {_Id}, {_Nom}, {_Latitude}, {_Longitude}" +
                                 $"FROM {_Ville}" +
                                 $"WHERE {_Id} = {id};";

            SqlConnection connexion = new SqlConnection(chaineConnexion);

            connexion.Open();

            SqlCommand commandSql = new SqlCommand(queryString, connexion);
            SqlDataReader reader = commandSql.ExecuteReader();


            int idBdd = reader[_Id];
            string nomBdd = reader[_Nom];
            double latitudeBdd = reader[_Latitude]; 
            double longitudeBdd = reader[_Longitude];


           // Ville ville = new Ville();
            return ville;
        }
    }
}
