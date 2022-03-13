using Model;
using System.Data.SqlClient;
using System.Text;

namespace DataBase
{
    internal class VilleBdd
    {
        private readonly string _chaineConnexion;

        private readonly string _VilleTable = "dbo.Ville";

        private readonly string _Id = "Id";
        private readonly string _Nom = "Nom";
        private readonly string _Latitude = "Latitude";
        private readonly string _Longitude = "Longitude";
        private readonly string _Pays = "Pays";


        public VilleBdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }

        /**
         * @fn public Ville GetVilleTableById
         * @brief Récupère dans la bdd une ville par son Id.
         * 
         * @param int idVille - *Id de la ville à récupérer*
         * 
         * @return Ville *Ville chargée depuis la bdd*
         */
        public Ville GetVilleTableById(int idVille)
        {
            Ville ville = null;

            // Requete SQL pour récuperer les infos sur une ville
            string queryString = $"SELECT * " +
                                 $"FROM {_VilleTable} " +
                                 $"WHERE {_Id} = {idVille};";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);

            try
            {
                connexion.Open(); // Ouverture de la connexion à la bdd

                // Création et execution de la requete SQL
                Console.WriteLine(queryString);
                SqlCommand commandSql = new SqlCommand(queryString, connexion);
                SqlDataReader reader = commandSql.ExecuteReader();

                reader.Read(); // Ouverture du reader
                int idVilleBdd = (int)reader[_Id];
                string nomBdd = (string)reader[_Nom];
                double? latitudeBdd = (double?)(reader[_Latitude] is System.DBNull ? null : Convert.ToDouble(reader[_Latitude]));
                double? longitudeBdd = (double?)(reader[_Longitude] is System.DBNull ? null : Convert.ToDouble(reader[_Longitude]));
                //Console.WriteLine(longitudeBdd);
                reader.Close(); // Fermeture du reader

                ville = new Ville(idVilleBdd, nomBdd, latitudeBdd, longitudeBdd);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                throw;
            }
            finally
            {
                connexion.Close();
            }

            return ville;
        }

        public void InsererVilleTable(Ville ville)
        {
            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            string queryString;

            try
            {
                connexion.Open(); // Ouverture connexion

                // Requete SQL pour inserer la ville
                queryString = $"INSERT INTO {_VilleTable} \n" +
                              $"( {_Nom}, {_Latitude}, {_Longitude} ) \n" + // , {_Pays}
                              $"VALUES ( '{ville.Nom}', '{ville.Latitude.ToString().Replace(",", ".")}', '{ville.Longitude.ToString().Replace(",", ".")}' );";


                // Création de la requete SQL d'INSERTION
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de l'insertion
                Console.WriteLine(queryString);
                Console.WriteLine(commandSql.ExecuteNonQuery() + " ligne inserée.");

                // Requete SQL pour récupérer l'id de la ville
                queryString = $"SELECT IDENT_CURRENT('{_VilleTable}');";

                // Création de la requete SQL pour récupérer l'Id à la ville
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de la requete de récupération de l'Id
                Console.WriteLine(queryString);
                SqlDataReader reader = commandSql.ExecuteReader();
                reader.Read();
                int idAutoIncrementeVille = Convert.ToInt32(reader[0]);
                ville.Id = idAutoIncrementeVille;
                Console.WriteLine("Id auto incrementé de la ville : " + idAutoIncrementeVille);

                reader.Close(); // Fermeture du reader

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                throw;
            }
            finally
            {
                connexion.Close();
            }
        }



        /**
        * private static string VilleValuesUpdate(Ville ville)
        * @brief Récupère et donne les instructions pour update les valeurs.
        * 
        * @param Ville ville
        * 
        * @return instruction *Valeurs à update*
        */
        private string VilleValuesUpdate(Ville ville)
        {
            // Récupération des champs personne
            const string VALUENULL = "NULL";

            StringBuilder valuesBuilder = new StringBuilder();

            valuesBuilder.Append($"\nSET {_Nom} = ");
            valuesBuilder.Append($"{(ville.Nom is null ? VALUENULL : "'" + ville.Nom + "'")}, ");

            valuesBuilder.Append($"\n{_Latitude} = ");
            valuesBuilder.Append($"{(ville.Latitude is null ? VALUENULL : ville.Latitude.ToString().Replace(",", "."))}, ");

            valuesBuilder.Append($"\n{_Longitude} = ");
            valuesBuilder.Append($"{(ville.Longitude is null ? VALUENULL : ville.Longitude.ToString().Replace(",", "."))} ");

            // Pas de prise en compte de l'id du pays dans l'objet Ville
            //valuesBuilder.Append($"\n{_Pays} = ");
            //valuesBuilder.Append($"{(ville.IdPays is null ? VALUENULL : ville.IdPays)}, ");


            return valuesBuilder.ToString();
        }


        /**
         * @fn public void UpdateVilleTable
         * @brief Update les données d'une ville dans la Table Ville
         * 
         * @param Ville ville - *Ville à modifier*
         * 
         * @warning La ville doit être present dans la table.
         */
        public void UpdateVilleTable(Ville ville)
        {
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            string queryString;

            try
            {
                connexion.Open(); // Ouverture connexion

                // Requete SQL pour update les valeurs de la personne
                queryString = $"UPDATE {_VilleTable} " +
                              $"{VilleValuesUpdate(ville)} \n" +
                              $"WHERE {_Id} = {(int)ville.Id};";


                // Création de la requete SQL d'INSERTION
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de l'insertion
                Console.WriteLine(queryString);
                Console.WriteLine(commandSql.ExecuteNonQuery() + " lignes modifiés.");

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                throw;
            }
            finally
            {
                connexion.Close();
            }
        }


        /**
         * @fn public Dictionary<int, string> GetNomVilles
         * @brief Récupère tous les noms et id des villes dans la bdd.
         */
        public Dictionary<int, string> GetNomVilles()
        {
            Dictionary<int, string> villesNom = new();

            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            string queryString;

            try
            {
                connexion.Open(); // Ouverture connexion

                // Requete SQL pour réccupérer les noms et id
                queryString = $"SELECT {_Id}, {_Nom} " +
                              $"FROM {_VilleTable};";


                // Création de la requete SQL de recupération
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de la requete de récupération des noms et id de toutes les villes
                Console.WriteLine(queryString);
                SqlDataReader reader = commandSql.ExecuteReader();

                while (reader.Read())
                {
                    villesNom.Add((int)reader[_Id], (string)reader[_Nom]);
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error SQL Generated. Details: " + e.ToString());
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
                throw;
            }
            finally
            {
                connexion.Close();
            }
            return villesNom;
        }
    }
}
