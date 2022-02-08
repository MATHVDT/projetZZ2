using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class PrenomBdd
    {
        static string chaineConnexion = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=..\\ANCESTREL\\DATABASE\\SAMPLEDATABASE.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static readonly string _PrenomTable = "dbo.Prenom";
        static readonly string _PrenomPersonneTable = "dbo.Prenom_Personne";

        static readonly string _IdPrenom = "Id_prenom";
        static readonly string _IdPersonne = "Id_personne";
        static readonly string _OrdrePrenom = "Ordre";
        static readonly string _Id = "Id";
        static readonly string _Prenom = "Prenom";


        public static string? GetPrenomById(int id)
        {
            // Requete SQL pour récuperer les infos sur une personne 
            string queryString = $"SELECT {_OrdrePrenom}, {_Prenom} " +
                                 $"FROM {_PrenomPersonneTable} JOIN {_PrenomTable} " + 
                                 $"ON {_IdPrenom} = {_Id} " +
                                 $"WHERE {_IdPersonne} = {id}" +
                                 $"ORDER BY {_OrdrePrenom} ASC;";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(chaineConnexion);
            connexion.Open();

            // Création et execution de la requete SQL
            SqlCommand commandSql = new SqlCommand(queryString, connexion);
            SqlDataReader reader = commandSql.ExecuteReader();

            // Récupération des prénoms 
            StringBuilder prenomBuilder = new StringBuilder();
            
            while(reader.Read())
            {
                prenomBuilder.Append(reader[_Prenom].ToString());
            }
            // Concatenation des prenoms
            string? prenoms = (prenomBuilder.Length == 0) ? null : prenomBuilder.ToString();

            return prenoms;
        }

    }
}
