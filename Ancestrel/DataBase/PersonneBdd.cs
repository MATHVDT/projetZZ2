using Model;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace DataBase
{
    public class PersonneBdd
    {
        private readonly string _chaineConnexion;

        private readonly string _PersonneTable = "Personne";

        private readonly string _Id = "Id";
        private readonly string _Sexe = "Sexe";
        private readonly string _NomUsage = "Nom_usage";
        private readonly string _Nom = "Nom";
        private readonly string _DateNaissance = "Date_naissance";
        private readonly string _DateDeces = "Date_deces";
        private readonly string _Description = "Description";
        private readonly string _IdVilleNaissance = "Id_ville_naissance";
        private readonly string _IdImgProfil = "Id_img_principale";
        private readonly string _IdPere = "Id_pere";
        private readonly string _IdMere = "Id_mere";


        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";
        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mavilledie4\Source\Repos\genealogie\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True";
        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ma_th\Documents\Programmations\pgenealogie\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True";
        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emper\OneDrive\Documents\ISIMA\ZZ2\Projet\Ancestrel\DataBase\Database.mdf;Integrated Security=True";


        public PersonneBdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }





        /**
         * @fn public static Personne GetPersonneTableById(int id)
         * @brief Récuperer une personne Table grâce à son id dans la BDD.
         * 
         * @param int id - *Id de la personne dans la BDD*
         * 
         * @return Personne *Personne avec les champs de la table complétés*
         */
        public Personne GetPersonneTableById(int id)
        {
            // Création de la personne avec les infos récupérées
            Personne p = null;

            // Requete SQL pour récuperer les infos sur une personne 
            string queryString = $"SELECT * " +
                                 $"FROM {_PersonneTable} " +
                                 $"WHERE {_Id} = {id};";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            SqlDataReader reader;

            try
            {
                connexion.Open(); // Ouverture de la connexion à la bdd

                // Création et execution de la requete SQL
                Console.WriteLine(queryString);
                commandSql = new SqlCommand(queryString, connexion);
                reader = commandSql.ExecuteReader();

                reader.Read(); // Debut de la lecture du la requete

                // Recupération de différent champs de la requete
                int idBdd = (int)reader[_Id];

                string? nomUsageBdd = (string?)(reader[_NomUsage] is System.DBNull ? null : reader[_NomUsage]);
                string? nomBdd = (string?)(reader[_Nom] is System.DBNull ? null : reader[_Nom]);

                DateOnly? dateNaissanceBdd = (DateOnly?)(reader[_DateNaissance] is System.DBNull ? null : DateOnly.FromDateTime((DateTime)reader[_DateNaissance]));
                DateOnly? dateDecesBdd = (DateOnly?)(reader[_DateDeces] is System.DBNull ? null : DateOnly.FromDateTime((DateTime)reader[_DateDeces]));

                string? description = (string?)(reader[_Description] is System.DBNull ? null : reader[_Description]);

                int? idPere = (reader[_IdPere] is System.DBNull ? null : (int?)reader[_IdPere]);
                int? idMere = (reader[_IdMere] is System.DBNull ? null : (int?)reader[_IdMere]);


                int? idVilleNaissance = (reader[_IdVilleNaissance] is System.DBNull ? null : (int?)reader[_IdVilleNaissance]);

                int sexe = (int)reader[_Sexe]; // 0 -> Homme et 1 -> Femme

                reader.Close();  // Fermeture de la lecture de la requete


                if (sexe == 1) // Femme 
                {
                    p = new Femme(0, idBdd, nomUsageBdd, null, dateNaissanceBdd, dateDecesBdd,
                        null, null, nomBdd, description);
                }
                else // Homme
                {
                    p = new Homme(0, idBdd, nomUsageBdd, null, dateNaissanceBdd, dateDecesBdd,
                        null, null, description);
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connexion.Close();
            }

            return p;
        }


        /**
         * @fn public static void InsererPersonneTable
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
        public void InsererPersonneTable(Personne personne)
        {
            // Récupération des champs personne (prénoms exclus)
            string values = PersonneValuesInsert(personne);


            //Console.WriteLine(queryString);

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            string queryString;

            try
            {
                connexion.Open(); // Ouverture connexion

                // Requete SQL pour inserer la personne 
                queryString = $"INSERT INTO {_PersonneTable} \n" +
                              $"( {_Sexe}, {_NomUsage}, {_Nom}, {_Description}, {_DateNaissance}, {_DateDeces}, {_IdVilleNaissance}, {_IdImgProfil}, {_IdPere}, {_IdMere} ) \n" +
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
                int idAutoIncrementePersonne = Convert.ToInt32(reader[0]);
                personne.Id = idAutoIncrementePersonne;
                Console.WriteLine("Id auto incrementé de la personne : " + idAutoIncrementePersonne);

                reader.Close(); // Le mettre dans le finally ?

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
            }
            catch (Exception e)
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
            valuesBuilder.Append($"{(personne.Nom is null ? VALUENULL : "'" + personne.Nom + "'")}, ");
            valuesBuilder.Append($"{(personne is Femme ? "'" + ((Femme)personne).NomJeuneFille + "'" ?? VALUENULL : VALUENULL)}, ");

            valuesBuilder.Append($"{(personne.Description is null ? VALUENULL : personne.Description)}, ");

            valuesBuilder.Append($"{(personne.DateNaissance is null ? VALUENULL : $"CONVERT(date, '{personne.DateNaissance}', 103)")}, ");
            valuesBuilder.Append($"{(personne.DateDeces is null ? VALUENULL : $"CONVERT(date, '{personne.DateDeces}', 103)")}, ");

            int? idLieuNaissance = personne.LieuNaissance is null ? null : personne.LieuNaissance.Id;
            valuesBuilder.Append($"{(idLieuNaissance is null ? VALUENULL : idLieuNaissance)}, ");

            int? idImageProfil = personne.GetFichierImageProfil()?.Id;
            valuesBuilder.Append($"{(idImageProfil is null ? VALUENULL : idImageProfil)}, ");

            valuesBuilder.Append($"{(personne.IdPere is null ? VALUENULL : personne.IdPere) }, ");
            valuesBuilder.Append($"{(personne.IdMere is null ? VALUENULL : personne.IdMere) } ");

            return valuesBuilder.ToString();
        }

        /**
         * @fn public int? GetIdVilleNaissancePersonneById(int idPersonne)
         * @brief Recupere l'id de la ville de naissance d'une personne.
         * 
         * @param int idPersonne
         * 
         * @return int? idVilleNaissance 
         */
        public int? GetIdVilleNaissancePersonneById(int idPersonne)
        {
            // id de la ville de naissance à récup
            int? idVilleNaissance = null;

            // Requete SQL pour récuperer les infos sur une personne 
            string queryString = $"SELECT {_IdVilleNaissance} " +
                                 $"FROM {_PersonneTable} " +
                                 $"WHERE {_Id} = {idPersonne};";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            SqlDataReader reader;

            try
            {
                connexion.Open(); // Ouverture de la connexion à la bdd

                // Création et execution de la requete SQL
                Console.WriteLine(queryString);
                commandSql = new SqlCommand(queryString, connexion);
                reader = commandSql.ExecuteReader();

                reader.Read(); // Debut de la lecture du la requete

                idVilleNaissance = (reader[_IdVilleNaissance] is System.DBNull ? null : (int?)reader[_IdVilleNaissance]);


                reader.Close();  // Fermeture de la lecture de la requete

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connexion.Close();
            }

            return idVilleNaissance;
        }


        /**
      * @fn public int? GetIdImageProfilPersonneById(int idPersonne)
      * @brief Recupere l'id de la ville de naissance d'une personne.
      * 
      * @param int idPersonne
      * 
      * @return int? idImageProfil 
      */
        public int? GetIdImageProfilPersonneById(int idPersonne)
        {
            // id de la ville de naissance à récup
            int? idImageProfil = null;

            // Requete SQL pour récuperer les infos sur une personne 
            string queryString = $"SELECT {_IdImgProfil} " +
                                 $"FROM {_PersonneTable} " +
                                 $"WHERE {_Id} = {idPersonne};";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            SqlDataReader reader;

            try
            {
                connexion.Open(); // Ouverture de la connexion à la bdd

                // Création et execution de la requete SQL
                Console.WriteLine(queryString);
                commandSql = new SqlCommand(queryString, connexion);
                reader = commandSql.ExecuteReader();

                reader.Read(); // Debut de la lecture du la requete

                idImageProfil = (reader[_IdImgProfil] is System.DBNull ? null : (int?)reader[_IdImgProfil]);


                reader.Close();  // Fermeture de la lecture de la requete

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connexion.Close();
            }

            return idImageProfil;
        }

        /**
         * @fn public void AjouterRelationParente
         * @brief Ajout dans la BDD les id du père et de la mère à l'enfant.
         * 
         * @param int idEnfant
         * @param int? idPere
         * @param int? idMere
         */
        public void AjouterRelationParente(int idEnfant, int? idPere, int? idMere)
        {
            const string VALUENULL = "NULL";

            // Requete SQL pour récuperer les infos sur une personne 
            string queryString = $"UPDATE {_PersonneTable} " +
                                 $"SET {_IdPere} = {(idPere is null ? VALUENULL : idPere)} " +
                                 $"SET {_IdMere} = {(idMere is null ? VALUENULL : idMere)} " +
                                 $"WHERE {_Id} = {idEnfant};";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;

            try
            {
                connexion.Open(); // Ouverture de la connexion à la bdd

                // Création et execution de la requete SQL
                Console.WriteLine(queryString);
                commandSql = new SqlCommand(queryString, connexion);
                Console.WriteLine(commandSql.ExecuteNonQuery() + $" ligne modifiée. " +
                    $"Enfant : {idEnfant} => Père : {idPere} et Mère : {idMere}");

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connexion.Close();
            }
        }
    }
}