using Model;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
        static readonly string _IdVilleNaissance = "Id_ville_naissance";
        static readonly string _IdImgProfil = "Id_img_principale";
        static readonly string _IdPere = "Id_pere";
        static readonly string _IdMere = "Id_mere";


        static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ma_th\Documents\Programmations\pgenealogie\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True";
        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emper\OneDrive\Documents\ISIMA\ZZ2\Projet\Ancestrel\DataBase\Database.mdf;Integrated Security=True";

        /**
         * @fn public static Personne GetPersonneById(int id)
         * @brief Récuperer une personne grâce à son id dans la BDD.
         * 
         * @param int id - *Id de la personne dans la BDD*
         * 
         * @return Personne *Personne avec les champs remplis*
         */
        public static Personne GetPersonneById(int id)
        {
            // Requete SQL pour récuperer les infos sur une personne 
            string queryString = $"SELECT * " +
                                 $"FROM {_PersonneTable} " +
                                 $"WHERE {_Id} = {id};";


            Console.WriteLine(queryString);
            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(chaineConnexion);
            connexion.Open();

            // Création et execution de la requete SQL
            SqlCommand commandSql = new SqlCommand(queryString, connexion);
            SqlDataReader reader = commandSql.ExecuteReader();

            reader.Read();

            // Recupération de différent champs de la requete
            int idBdd = (int)reader[_Id];

            string? nomUsageBdd = (string?)(reader[_NomUsage] is System.DBNull ? null : reader[_NomUsage]);
            string? nomBdd = (string?)(reader[_Nom] is System.DBNull ? null : reader[_Nom]);

            //string? nomBdd = SqlString.Null
            //Console.WriteLine("nomBdd : " + nomBdd + " valeur de reader[_Nom] : " + reader[_Nom]);

            DateOnly? dateNaissanceBdd = (DateOnly?)(reader[_DateNaissance] is System.DBNull ? null : DateOnly.FromDateTime((DateTime)reader[_DateNaissance]));
            DateOnly? dateDecesBdd = (DateOnly?)(reader[_DateDeces] is System.DBNull ? null : DateOnly.FromDateTime((DateTime)reader[_DateDeces]));

            int? idPere = (reader[_IdPere] is System.DBNull ? null : (int?)reader[_IdPere]);
            int? idMere = (reader[_IdMere] is System.DBNull ? null : (int?)reader[_IdMere]);


            int? idImgProfil = (reader[_IdImgProfil] is System.DBNull ? null : (int?)reader[_IdImgProfil]);
            int? idVilleNaissance = (reader[_IdVilleNaissance] is System.DBNull ? null : (int?)reader[_IdVilleNaissance]);

            int sexe = (int)reader[_Sexe]; // 0 -> Homme et 1 -> Femme


            // Récupération des prénoms
            string? prenomsBdd = null;
            //PrenomBdd.GetPrenomById(id);

            // Création de la personne avec les infos récupérées
            Personne p;

            if (sexe == 1) // Femme 
            {
                p = new Femme(0, idBdd, nomUsageBdd, prenomsBdd, dateNaissanceBdd, dateDecesBdd,
                    null, null, nomBdd);
            }
            else // Homme
            {
                p = new Homme(0, idBdd, nomUsageBdd, prenomsBdd, dateNaissanceBdd, dateDecesBdd,
                    null, null);
            }

            reader.Close();
            connexion.Close();

            // Ajout Image 
            // Ajout Image profil


            return p;
        }


        /**
         * @fn public static void InsererPersonne
         * @brief Insere une personne dans la bdd.
         * 
         * @param Personne personne - *Personne à ajouté dans la bdd*
         * 
         * @details
         * Ajoute une nouvelle personne dans la bdd.
         * Et tiens à jour les autres tables d'associations.
         * 
         * @warning La personne ajoutée ne doit pas être présente dans la bdd.
         */
        public static void InsererPersonne(Personne personne)
        {
            // Récupération des champs personne (prénoms exclus)
            string values = PersonneValuesInsert(personne);



            //Console.WriteLine(queryString);

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(chaineConnexion);
            SqlCommand commandSql;
            string queryString;

            try
            {
                connexion.Open(); // Ouverture connexion

                // Requete SQL pour inserer la personne 
                queryString = $"INSERT INTO {_PersonneTable} " +
                                     $"VALUES ({values});";

                // Création de la requete SQL d'INSERTION
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de l'insertion
                Console.WriteLine(queryString);
                Console.WriteLine(commandSql.ExecuteNonQuery() + " ligne inserée.");

                // Requete SQL pour récupérer l'id de la personne
                queryString = $"SELECT IDENT_CURRENT('{_PersonneTable}');";

                // Création de la requete SQL pour récupérer l'Id attribué à la personne inserée
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de la requete de récupération de l'Id
                Console.WriteLine(queryString);
                SqlDataReader reader = commandSql.ExecuteReader();
                reader.Read();
                int idAutoIncrementePersonne = Convert.ToInt32( reader[0]);
                personne.Id = idAutoIncrementePersonne;
                Console.WriteLine("Id auto incrementé de la personne : " + idAutoIncrementePersonne);

                reader.Close(); // Le mettre dans le finally ?

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



        /**
         * public static string PersonneValuesInsert(Personne personne)
         * @brief Récupère et donne les valeurs des champs à inserer dans la bdd.
         * 
         * @param Personne personne
         * 
         * @return string *Valeurs à inserer*
         */
        public static string PersonneValuesInsert(Personne personne)
        {
            // Récupération des champs personne
            const string VALUENULL = "NULL";

            StringBuilder valuesBuilder = new StringBuilder();

            valuesBuilder.Append($"{(personne is Homme ? 0 : 1)}, ");
            valuesBuilder.Append($"{"'" + personne.Nom + "'" ?? VALUENULL}, ");
            valuesBuilder.Append($"{(personne is Femme ? "'" + ((Femme)personne).NomJeuneFille + "'" ?? VALUENULL : VALUENULL)}, ");

            valuesBuilder.Append($"{(personne.DateNaissance is null ? VALUENULL : $"CONVERT(date, '{personne.DateNaissance}', 103)")}, ");
            valuesBuilder.Append($"{(personne.DateDeces is null ? VALUENULL : $"CONVERT(date, '{personne.DateDeces}', 103)")}, ");

            int? idLieuNaissance = personne.LieuNaissance is null ? null : personne.LieuNaissance.Id;
            valuesBuilder.Append($"{(idLieuNaissance is null ? VALUENULL : idLieuNaissance)}, ");

            int? idImageProfil = (personne.GetFichierImageProfil() is null ? null : personne.GetFichierImageProfil().Id);
            valuesBuilder.Append($"{(idImageProfil is null ? VALUENULL : idImageProfil)}, ");

            valuesBuilder.Append($"{(personne.IdPere is null ? VALUENULL : personne.IdPere) }, ");
            valuesBuilder.Append($"{(personne.IdMere is null ? VALUENULL : personne.IdMere) } ");

            return valuesBuilder.ToString();
        }



    }
}