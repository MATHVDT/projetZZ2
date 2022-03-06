using Model;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly string _DateAjout = "DateAjout";

        private readonly string _IdImage = "Id_image";
        private readonly string _IdPersonne = "Id_personne";


        public ImageBdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }


        /**
         * @fn public FichierImage GetVilleTableById
         * @brief Récupère dans la bdd une image par son Id.
         * 
         * @param int idImage - *Id de l'image à récupérer*
         * 
         * @return FichierImage *Image chargée depuis la bdd*
         */
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
                Console.WriteLine(imgByteBdd);

                reader.Close(); // Fermeture du reader

                // Création du fichier image
                fichierImage = new FichierImage(imgByteBdd,
                                                idBdd, nomBdd,
                                                dateAjout);
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

            return fichierImage;
        }


        public void InsererImageTable(FichierImage fichierImage)
        {
            string values = ImageValuesInsert(fichierImage);


            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            string queryString;

            try
            {
                connexion.Open(); // Ouverture connexion

                // Requete SQL pour inserer l'image
                queryString = $"INSERT INTO {_ImageTable} \n" +
                              $"({_Nom}, {_DateAjout}, {_Image} ) \n" +
                              $"VALUES ({values} " + " @imgByteArray );";


                // Création de la requete SQL d'INSERTION
                commandSql = new SqlCommand(queryString, connexion);

                // Ajout de l'image en byteArray pour une conversion en varbinary(max)
                commandSql.Parameters.Add("@imgByteArray", SqlDbType.VarBinary).Value = FichierImage.ImageToByteArray(fichierImage.Image);

                // Excecution de l'insertion
                Console.WriteLine(queryString);
                Console.WriteLine(commandSql.ExecuteNonQuery() + " ligne inserée.");

                // Requete SQL pour récupérer l'id de l'image
                queryString = $"SELECT IDENT_CURRENT('{_ImageTable}');";

                // Création de la requete SQL pour récupérer l'Id attribué àl'image inserée
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de la requete de récupération de l'Id
                Console.WriteLine(queryString);
                SqlDataReader reader = commandSql.ExecuteReader();
                reader.Read();
                int idAutoIncrementeImage = Convert.ToInt32(reader[0]);
                fichierImage.Id = idAutoIncrementeImage;
                Console.WriteLine("Id auto incrementé de la image : " + idAutoIncrementeImage);

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
         * @fn public List<int> GetListIdImagePersonneById
         * @brief Récupère la liste des id des images associés à une personne.
         * 
         * @param int idPersonne
         */
        public List<int> GetListIdImagePersonneById(int idPersonne)
        {

            List<int> list = new List<int>();


            // Requete SQL pour récuperer les id des Images associés à une personne
            string queryString = $"SELECT {_IdImage} " +
                                 $"FROM {_ImagePersonneTable} " +
                                 $"WHERE {_IdPersonne} = {idPersonne};";

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

                while (reader.Read())
                {
                    list.Add((int)reader[_IdImage]);
                }

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
            return list;
        }




        /**
         * @fn public void InsererAssociationImagePersonneId
         * @brief Insere l'association entre une personne et image.
         * 
         * @param int idImage
         * @param int idPersonne
         * 
         * @details
         * Fait l'associations une image et une personne dans
         * la table d'association.
         * 
         * @warning La personne ne doit pas avoir d'image pour utiliser
         * cette méthode. Il faut supprimer les images de la personne avant,
         * ie supprimer dans la table d'association les relations image-personne.
         */
        public void InsererAssociationImagePersonneId(int idImage, int idPersonne)
        {
            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            string queryString;

            try
            {
                connexion.Open(); // Ouverture connexion

                // Requete SQL pour inserer l'association
                queryString = $"INSERT INTO {_ImagePersonneTable} \n" +
                              $"({_IdImage}, {_IdPersonne} ) \n" +
                              $"VALUES ({idImage}, {_IdPersonne} );";


                // Création de la requete SQL d'INSERTION
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de l'insertion
                Console.WriteLine(queryString);
                Console.WriteLine(commandSql.ExecuteNonQuery() + " ligne inserée.");

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
         * @fn public void SuppressionImagePersonne
         * @brief Supprime les associations des images d'une personne.
         * 
         * @param int idPersonne
         */
        public void SuppressionImagePersonne(int idPersonne)
        {
            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            SqlCommand commandSql;
            string queryString;

            // Requete SQL pour récupérer supprimer les images d'une personne dans la bdd
            queryString = $"DELETE FROM {_ImagePersonneTable} " +
                          $"WHERE {_IdPersonne} = {idPersonne}; ";
            try
            {
                connexion.Open(); // Ouverture connexion

                // Création de la requete SQL
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de la requete de suppression
                Console.WriteLine(queryString);
                Console.WriteLine(commandSql.ExecuteNonQuery() + " lignes spprimées.");

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
         * public string ImageValuesInsert
         * @brief Récupère et donne les valeurs des champs à inserer dans la bdd. (image exclue)
         * 
         * @param FichierImage fichierImage
         * 
         * @return string *Valeurs à inserer*
         */
        public string ImageValuesInsert(FichierImage fichierImage)
        {
            // Récupération des champs du fichierImage
            //const string VALUENULL = "NULL";

            StringBuilder valuesBuilder = new StringBuilder();

            valuesBuilder.Append($"'{fichierImage.NomFichier}', ");

            valuesBuilder.Append($"{$"CONVERT(date, '{fichierImage.DateAjoutFichier}', 103)"}, ");

            //byte[] tmp = FichierImage.ImageToByteArray(fichierImage.Image);
            //valuesBuilder.Append("CONVERT(VARBINARY(MAX), '");

            //foreach (var x in tmp)
            //    valuesBuilder.Append($"{x.ToString()}");

            //valuesBuilder.Append("' ) ");

            return valuesBuilder.ToString();
        }

        // Inutilisé
        // Transformer byte[] en string pour insertion dans bdd
        private string ToVarbinary(byte[] data)
        {
            var sb = new StringBuilder((data.Length * 2) + 2);
            sb.Append("0x");

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
