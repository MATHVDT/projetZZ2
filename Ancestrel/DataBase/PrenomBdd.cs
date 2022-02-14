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
        private readonly string _chaineConnexion;

        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";
        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mavilledie4\Source\Repos\genealogie\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True";
        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";
        //static string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\emper\OneDrive\Documents\ISIMA\ZZ2\Projet\Ancestrel\DataBase\Database.mdf;Integrated Security=True";

        private readonly string _PrenomTable = "dbo.Prenom";
        private readonly string _PrenomPersonneTable = "dbo.Prenom_Personne";

        private readonly string _IdPrenom = "Id_prenom";
        private readonly string _IdPersonne = "Id_personne";
        private readonly string _OrdrePrenom = "Ordre";
        private readonly string _Id = "Id";
        private readonly string _Prenom = "Prenom";


        public PrenomBdd(string chaineConnexion)
        {
            _chaineConnexion = chaineConnexion;
        }



        public string? GetPrenomById(int idPersonne)
        {
            // Requete SQL pour récuperer les prénoms de la personne
            string queryString = $"SELECT {_OrdrePrenom}, {_Prenom} " +
                                 $"FROM {_PrenomPersonneTable} JOIN {_PrenomTable} " +
                                 $"ON {_IdPersonne} = {_Id} " +
                                 $"WHERE {_IdPersonne} = {idPersonne} " +
                                 $"ORDER BY {_OrdrePrenom} ASC;";

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
            connexion.Open();

            // Création et execution de la requete SQL
            SqlCommand commandSql = new SqlCommand(queryString, connexion);
            Console.WriteLine(queryString);
            SqlDataReader reader = commandSql.ExecuteReader();

            // Récupération des prénoms 
            StringBuilder prenomBuilder = new StringBuilder();

            while (reader.Read())
            {
                prenomBuilder.Append(reader[_Prenom].ToString() + " ");
            }
            // Concatenation des prenoms
            string? prenoms = (prenomBuilder.Length == 0) ? null : prenomBuilder.ToString();

            return prenoms;
        }

        public void InsererAssociationPrenomsPersonneId(List<string> listPrenoms, int idPersonne)
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
            // On enlève les deux dernier caractères ", "
            //listPrenomsValuesBuilder.Remove(listPrenomsValuesBuilder.Length - 2, listPrenomsValuesBuilder.Length - 1);

            // Sert à garder en mémoire les prenoms et leurs id associé qui sont enregistré
            Dictionary<string, int> prenomsDejaEnregistres = new Dictionary<string, int>();

            // Connexion à la bdd
            SqlConnection connexion = new SqlConnection(_chaineConnexion);
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
                    Console.Write((string)reader[_Prenom] + " ");
                }
                Console.WriteLine();

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
                        Console.WriteLine("Id auto incrementé du prenom inseré : " + reader[0]);
                        reader.Close(); // Fermeture du reader
                    }

                    // Insertion de l'association dans la TABLE Prenom_Personne
                    // Récupère l'Id du prenom dans le dico => existe forcement car il à été inseré
                    idPrenomEnregistre = prenomsDejaEnregistres.GetValueOrDefault(prenom, 0);

                    // Requete SQL d'INSERTION dans la TABLE association des prenoms-personne
                    queryString = $"INSERT INTO {_PrenomPersonneTable} " +
                                  $"( {_IdPersonne}, {_IdPrenom} ) " +
                                  $"VALUES ({idPersonne}, {idPrenomEnregistre});";

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
