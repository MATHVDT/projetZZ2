using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    internal class PaysBdd
    {
        private readonly string _chaineConnexion;

        private readonly string _PaysTable = "dbo.Pays";
        private readonly string _NationaliteTable = "dbo.Nationalite";

        private readonly string _Id = "Id";
        private readonly string _Code = "Code";
        private readonly string _Alpha2 = "Alpha2";
        private readonly string _Alpha3 = "Alpha3";
        private readonly string _NomEn = "Nom_en_gb";
        private readonly string _NomFr = "Nom_fr_fr";
        private readonly string _Nationalite = "Nationalite";

        private readonly string _IdPays = "Id_pays";
        private readonly string _IdPersonne = "Id_personne";


        public PaysBdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }


        /**
         * @fn public string GetNationaliteTableByIdPays
         * @brief Récupère la nationalite d'un pays.
         * 
         * @param int idPays - *Id du pays*
         * 
         * @return string? *Nationalite du pas chargée depuis la bdd*
         */
        public string? GetNationaliteTableByIdPays(int idPays)
        {
            string? nationalite = null;

            // Requete SQL pour récuperer la nationalite d'un pays
            string queryString = $"SELECT {_Nationalite} " +
                                 $"FROM {_PaysTable} " +
                                 $"WHERE {_Id} = {idPays};";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);

            try
            {
                connexion.Open(); // Ouverture de la connexion à la bdd

                // Création et execution de la requete SQL
                Console.WriteLine(queryString);
                SqlCommand commandSql = new SqlCommand(queryString, connexion);
                SqlDataReader reader = commandSql.ExecuteReader();

                // Ouverture du reader
                if (reader.Read())
                    nationalite = (string?)reader[_Nationalite];


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

            return nationalite;
        }



        /**
         * @fn public string? GetNationalitesByIdPersonne(int idPersonne)
         * @brief Retourne la chaine des nationalite de la personne
         * 
         * @param int idPersonne
         * 
         * @return string? *Nationalités de la personne*
         */
        public string? GetNationalitesByIdPersonne(int idPersonne)
        {
            string? nationalites = null;

            // Requete SQL pour récuperer les prénoms de la personne
            string queryString = $"SELECT {_Nationalite} \n" +
                                 $"FROM {_PaysTable} JOIN {_NationaliteTable} \n" +
                                 $"ON {_PaysTable}.{_Id} = {_NationaliteTable}.{_IdPays} \n" +
                                 $"WHERE {_IdPersonne} = {idPersonne};";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);

            try
            {
                connexion.Open();

                // Création et execution de la requete SQL
                SqlCommand commandSql = new SqlCommand(queryString, connexion);
                Console.WriteLine(queryString);
                SqlDataReader reader = commandSql.ExecuteReader();

                // Récupération des  nationalite 
                StringBuilder nationalitesBuilder = new StringBuilder();

                while (reader.Read())
                {
                    nationalitesBuilder.Append(reader[_Nationalite].ToString() + " ");
                }

                // Concatenation des nationalite
                nationalites = (nationalitesBuilder.Length == 0) ? null : nationalitesBuilder.ToString();
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
            return nationalites;
        }




        public void InsererNationalitePersonne(Personne personne)
        {
            int idPersonne = (int)personne.Id;
            List<string> listNationalites = personne.GetListNationalites();
            List<int> idPays = new List<int>();

            // Construction de la string Values pour la requete SQL
            StringBuilder listNationaliteValuesBuilder = new StringBuilder();

            listNationalites.ForEach(x => listNationaliteValuesBuilder.Append("'" + x.ToUpper() + "' "));
            listNationaliteValuesBuilder.Replace(" '", ", '");


            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            SqlDataReader reader;
            string queryString;



            // Requete SQL pour récupérer les id des pays correspondant aux natio
            queryString = $"SELECT {_Id} " +
                          $"FROM {_PaysTable} " +
                          $"WHERE {_Nationalite} IN ( {listNationaliteValuesBuilder.ToString()} );";
            try
            {
                connexion.Open();

                // Création de la requete SQL
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de la requete de récupération des l'Id des pays
                Console.WriteLine(queryString);
                reader = commandSql.ExecuteReader();

                // Stockage de id des pays
                while (reader.Read())
                {
                    idPays.Add((int)reader[_Id]);
                }
                reader.Close(); // Fermeture du SqlReader

                foreach (int id in idPays)
                {
                    queryString = $"INSERT INTO {_Nationalite} " +
                                  $"( {_IdPays}, {_IdPersonne} ) " +
                                  $"VALUES ({id},  {idPersonne});";

                    // Création de la requete SQL d'INSERTION 
                    commandSql = new SqlCommand(queryString, connexion);

                    // Excecution de la requete SQL d'INSERTION 
                    Console.WriteLine(queryString);
                    Console.WriteLine(commandSql.ExecuteNonQuery() + " ligne inserée.");
                }

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
    }
}
