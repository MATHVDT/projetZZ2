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
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connexion.Close();
            }

            return nationalite;
        }
    }
}
