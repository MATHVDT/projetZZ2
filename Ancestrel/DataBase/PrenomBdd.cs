using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class PrenomBdd
    {
        static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\ma_th\Documents\Programmations\pgenealogie\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True";
        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emper\OneDrive\Documents\ISIMA\ZZ2\Projet\Ancestrel\DataBase\Database.mdf;Integrated Security=True";

        static readonly string _PrenomTable = "dbo.Prenom";
        static readonly string _PrenomPersonneTable = "dbo.Prenom_Personne";

        static readonly string _IdPrenom = "Id_prenom";
        static readonly string _IdPersonne = "Id_personne";
        static readonly string _OrdrePrenom = "Ordre";
        static readonly string _Id = "Id";
        static readonly string _Prenom = "Prenom";


        public static string? GetPrenomById(int id)
        {
            // Requete SQL pour récuperer les infos sur une personne 
            string queryString = $"SELECT {_OrdrePrenom}, {_Prenom} " +
                                 $"FROM {_PrenomPersonneTable} JOIN {_PrenomTable} " +
                                 $"ON {_IdPrenom} = {_Id} " +
                                 $"WHERE {_IdPersonne} = {id}" +
                                 $"ORDER BY {_OrdrePrenom} ASC;";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(chaineConnexion);
            connexion.Open();

            // Création et execution de la requete SQL
            SqlCommand commandSql = new SqlCommand(queryString, connexion);
            SqlDataReader reader = commandSql.ExecuteReader();

            // Récupération des prénoms 
            StringBuilder prenomBuilder = new StringBuilder();

            while (reader.Read())
            {
                prenomBuilder.Append(reader[_Prenom].ToString());
            }
            // Concatenation des prenoms
            string? prenoms = (prenomBuilder.Length == 0) ? null : prenomBuilder.ToString();

            return prenoms;
        }

        public static void InsererPrenomsPersonne(List<string> listPrenoms, int idPersonne)
        {
            // Construction de la string Values pour la requete SQL
            StringBuilder listPrenomsValuesBuilder = new StringBuilder();

            listPrenoms.ForEach(x => listPrenomsValuesBuilder.Append("'" + x.ToUpper() + "' "));
            listPrenomsValuesBuilder.Replace(" '", ", '");

            //foreach (string p in listPrenoms)
            //{
            //    listPrenomsValuesBuilder.Append("'");
            //    listPrenomsValuesBuilder.Append(p);
            //    listPrenomsValuesBuilder.Append("', ");
            //}
            //// On enlève les deux dernier caractères ", "
            //listPrenomsValuesBuilder.Remove(listPrenomsValuesBuilder.Length - 2, listPrenomsValuesBuilder.Length - 1);

            // Sert à garder en mémoire les prenoms et leurs id associé qui sont enregistré
            Dictionary<string, int> prenomsDejaEnregistres = new Dictionary<string, int>();

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(chaineConnexion);
            SqlCommand commandSql;
            SqlDataReader reader;
            string queryString;

            string prenom;
            int idPrenomEnregistre;

            try
            {
                connexion.Open(); // Ouverture connexion

                // Requete SQL pour récupérer les prénoms qui sont déjà dans la bdd
                queryString = $"SELECT {_Id}, {_Prenom} " +
                              $"FROM {_PrenomTable} " +
                              $"WHERE {_Prenom} IN ( {listPrenomsValuesBuilder.ToString()});";

                // Création de la requete SQL
                commandSql = new SqlCommand(queryString, connexion);

                // Excecution de la requete de récupération de l'Id
                Console.WriteLine(queryString);
                reader = commandSql.ExecuteReader();

                Console.Write("Prénoms déjà enregistrés : ");
                // Stockage des id des prénoms déjà enregistrés
                while (reader.Read())
                {
                    prenomsDejaEnregistres.Add((string)reader[_Prenom], (int)reader[_Id]);
                    Console.WriteLine((string)reader[_Prenom]);
                }

                reader.Close(); // Fermeture du SqlReader

                for (int i = 0; i < listPrenoms.Count; ++i) // A verif
                {
                    prenom = listPrenoms[i].ToUpper(); // Récupération du prénom position i

                    // Check si le prénom est déjà enregistré
                    if (!prenomsDejaEnregistres.ContainsKey(prenom))
                    {
                        // Requete SQL d'INSERTION des prénoms dans la TABLE Prenom
                        queryString = $"INSERT INTO {_PrenomTable} " +
                                      $"VALUES ('{prenom}');";

                        // Création de la requete SQL d'INSERTION des prénoms dans la TABLE Prenom
                        commandSql = new SqlCommand(queryString, connexion);

                        // Excecution de la requete SQL d'INSERTION des prénoms dans la TABLE Prenom
                        Console.WriteLine(queryString);
                        Console.WriteLine(commandSql.ExecuteNonQuery() + " ligne inserée.");

                        // Récupération de Id Généré 

                        // Requete SQL pour récupérer l'Id du prenom inseré
                        queryString = $"SELECT IDENT_CURRENT('{_PrenomTable}');";

                        // Création de la requete SQL pour récupérer l'Id du prenom inseré
                        commandSql = new SqlCommand(queryString, connexion);

                        // Excecution de la requete SQL pour récuprer l'Id du prenom inseré
                        Console.WriteLine(queryString);
                        reader = commandSql.ExecuteReader();
                        reader.Read(); // Lecture du resultat
                        prenomsDejaEnregistres.Add(prenom, Convert.ToInt32(reader[0])); 
                        reader.Close(); // Fermeture du reader
                    }

                    // Insertion de l'association dans la TABLE Prenom_Personne
                    // Récupère l'Id du prenom dans le dico => existe forcement car il à été inseré
                    idPrenomEnregistre = prenomsDejaEnregistres.GetValueOrDefault(prenom, 0);

                    // Requete SQL d'INSERTION dans la TABLE association des prenoms-personne
                    queryString = $"INSERT INTO {_PrenomPersonneTable} " +
                                  $"VALUES ({idPersonne}, {idPrenomEnregistre}, {i});";

                    // Création de la requete SQL d'INSERTION dans la TABLE association des prenoms-personne
                    commandSql = new SqlCommand(queryString, connexion);

                    // Excecution de la requete SQL d'INSERTION dans la TABLE association des prenoms-personne
                    Console.WriteLine(queryString);
                    Console.WriteLine(commandSql.ExecuteNonQuery() + " ligne inserée.");

                }


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
        }

    }
}
