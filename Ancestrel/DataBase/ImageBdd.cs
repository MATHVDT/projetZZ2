using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    internal class ImageBdd
    {


        private readonly string _chaineConnexion;

        private readonly string _ImageTable = "dbo.Image";
        private readonly string _ImagePersonneTable = "dbo.Image_Personne";

        private readonly string _Id = "Id";
        private readonly string _Image = "Image";
        private readonly string _Nom = "Nom";
        private readonly string _DateAjout = "Ordre";

        private readonly string _IdImage = "Id_image";
        private readonly string _IdPersonne = "Id_personne";


        public ImageBdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }



        public FichierImage GetImageById(int idImage)
        {
            FichierImage fichierImage = null;


            // Requete SQL pour récuperer les infos sur une ville
            string queryString = $"SELECT * " +
                                 $"FROM {_ImageTable} " +
                                 $"WHERE {_Id} = {idImage};";

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

                int idBdd = (int)reader[_Id];
                string nomBdd = (string)reader[_Nom];
                DateTime? dateAjout = (DateTime?)(reader[_DateAjout] is System.DBNull ? null : (DateTime)reader[_DateAjout]);
                byte[] imgByteBdd = (byte[])reader[_Image];

                reader.Close(); // Fermeture du reader

                // Création du fichier image
                fichierImage = new FichierImage(imgByteBdd,
                                                idBdd, nomBdd,
                                                dateAjout);
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

            return fichierImage;
        }

    }
}
