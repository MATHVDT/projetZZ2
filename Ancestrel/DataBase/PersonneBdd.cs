using Model;
using System.Data.SqlClient;

namespace DataBase
{
    public class PersonneBdd
    {
        static readonly string _PersonneTable = "Personne";

        static readonly string _Id = "Id";
        static readonly string _Sexe = "Sexe";
        static readonly string _NomUsage = "Nom_usage";
        static readonly string _Nom = "Nom";
        static readonly string _DateNaissance = "Date_naissance";
        static readonly string _DateDeces = "Date_deces";
        static readonly string _IdPere = "Id_pere";
        static readonly string _IdMere = "Id_mere";
        static readonly string _IdVilleNaissance = "Id_ville_naissance";
        static readonly string _IdImgProfil = "[Id_img_principale]";

        static string chaineConnexion = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=..\\ANCESTREL\\DATABASE\\SAMPLEDATABASE.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



        public static Personne GetPersonneById(int id)
        {
            // Requete SQL pour récuperer les infos sur une personne 
            string queryString = $"SELECT * " +
                                 $"FROM {_PersonneTable}" +
                                 $"WHERE {_Id} = {id};";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(chaineConnexion);
            connexion.Open();

            // Création et execution de la requete SQL
            SqlCommand commandSql = new SqlCommand(queryString, connexion);
            SqlDataReader reader = commandSql.ExecuteReader();


            // Recupération de différent champs de la requete
            int idBdd = (int)reader[_Id];

            string? nomUsageBdd = (string?)reader[_NomUsage];
            string? nomBdd = (string?)reader[_Nom];

            DateOnly? dateNaissanceBdd = (DateOnly?)reader[_DateNaissance];
            DateOnly? dateDecesBdd = (DateOnly?)reader[_DateDeces];

            int? idPere = (int?)reader[_IdPere];
            int? idMere = (int?)reader[_IdMere];

            int? idImgProfil = (int?)reader[_IdImgProfil];
            int? idVilleNaissance = (int?)reader[_IdVilleNaissance];

            int sexe = (int)reader[_Sexe];


            // Récupération des prénoms
            string? prenomsBdd = PrenomBdd.GetPrenomById(id);

            // Création de la personne avec les infos récupérées
            Personne p;

            if (sexe == 0) // Femme 
            {
                p = new Femme(0, idBdd, nomBdd, prenomsBdd, dateNaissanceBdd, dateDecesBdd,
                    null, null, nomBdd);
            }
            else // Homme
            {
                p = new Homme(0, idBdd, nomBdd, prenomsBdd, dateNaissanceBdd, dateDecesBdd,
                    null, null);
            }


            // Ajout Image 
            // Ajout Image profil

            return p;
        }

        public static void InsererPersonne(Personne personne)
        {
            // Requete SQL pour inserer la personne 
            string queryString = $"INSERT INTO {_PersonneTable} " +
                                 $"VALUES ;";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(chaineConnexion);
            connexion.Open();

            // Création et execution de la requete SQL
            SqlCommand commandSql = new SqlCommand(queryString, connexion);
            SqlDataReader reader = commandSql.ExecuteReader();

        }

    }
}