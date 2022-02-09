using Model;
using System.Data.SqlClient;
using System.Text;

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

        //static string chaineConnexion = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=..\\ANCESTREL\\DATABASE\\SAMPLEDATABASE.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static string chaineConnexion = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=master;Integrated Security = True";

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

            reader.Close();
            connexion.Close();

            // Ajout Image 
            // Ajout Image profil


            return p;
        }

        public static void InsererPersonne(Personne personne)
        {
            // Récupération des champs personne
            const string VALUENULL = "NULL";

            StringBuilder valuesBuilder = new StringBuilder();

            valuesBuilder.Append($"{(personne is Homme ? 0 : 1)}, ");
            valuesBuilder.Append($"'{personne.Nom ?? VALUENULL}', ");
            valuesBuilder.Append($"'{(personne is Femme ? ((Femme)personne).NomJeuneFille ?? VALUENULL : VALUENULL)}', ");

            valuesBuilder.Append($"{(personne.DateNaissance is null ? VALUENULL : $"CONVERT(date, '{personne.DateNaissance}', 103)")}, ");
            valuesBuilder.Append($"{(personne.DateDeces is null ? VALUENULL : $"CONVERT(date, '{personne.DateDeces}', 103)")}, ");

            int? idLieuNaissance = personne.LieuNaissance is null ? null : personne.LieuNaissance.Id;
            valuesBuilder.Append($"{(idLieuNaissance is null ? VALUENULL : idLieuNaissance)}, ");

            int? idImageProfil = (personne.GetFichierImageProfil() is null ? null : personne.GetFichierImageProfil().Id);
            valuesBuilder.Append($"{(idImageProfil is null ? VALUENULL : idImageProfil)}, ");

            valuesBuilder.Append($"{(personne.IdPere is null ? VALUENULL : personne.IdPere) }, ");
            valuesBuilder.Append($"{(personne.IdMere is null ? VALUENULL : personne.IdMere) } ");

            string values = valuesBuilder.ToString();


            // Requete SQL pour inserer la personne 
            string queryString = $"INSERT INTO {_PersonneTable} " +
                                 $"VALUES {values}" +
                                 $"SELECT IDENT_CURRENT('{_PersonneTable}');";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(chaineConnexion);


            try
            {
                connexion.Open(); // Ouverture connexion

                // Création et execution de la requete SQL
                SqlCommand commandSql = new SqlCommand(queryString, connexion);

                SqlDataReader reader = commandSql.ExecuteReader();
                Console.WriteLine("Records Inserted Successfully et Id : " + reader[0]);
                reader.Close();

                //commandSql.ExecuteNonQuery();
                //Console.WriteLine("Records Inserted Successfully et Id : " );
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connexion.Close();
            }



            // SqlTransaction
            // Enregistrer les images avant
            // Enregistrer la description
            // Enregister Ville ?

            // Enregistrer les prenoms

            // Récupérer l'id générer par la bdd
            //SELECT IDENT_CURRENT('Personne');

        }
    }
}