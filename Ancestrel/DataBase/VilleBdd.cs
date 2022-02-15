using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                connexion.Close();
            }

            return ville;
        }



    }
}
