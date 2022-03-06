// See https://aka.ms/new-console-template for more information
/**
* @file Program.cs
* @author Mathieu
* @date 28/12/2021
* @brief Fichier pour lancer en mode console
* @details Le fichier ne contient que les instructions de la fonction main
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataBase;
using Model;
using Model;

/**
* @fn Main
* @brief Point d'entré de l'application en mode console
*/
Console.WriteLine("Hello, World!");

//Manager m = Manager.GetInstance();

/*Femme p = new Femme(1);

p.Nom = "dupond";
p.DateNaissance = DateOnly.FromDateTime(DateTime.Today);

Console.WriteLine(p.Nom);
Console.WriteLine("Personne : " + p.ToString());

Homme h = new(num: 1, nom: "Dupond", prenoms: "Jean Remi",
   dateNaissance: DateOnly.Parse("16/02/2000"));



Console.WriteLine();
Console.WriteLine(h.ToString());
Console.WriteLine(h.Prenoms);
Console.WriteLine(h.GetPrenoms());*/

/*
Console.WriteLine(File.Exists("C:/Users/emper/OneDrive/Documents/ISIMA/ZZ2/Projet/Ancestrel/Console/Image1.jpg"));
string path = "C:\\Users\\emper\\OneDrive\\Documents\\ISIMA\\ZZ2\\Projet\\Ancestrel\\Console\\Image1.jpg";

FichierImage f = new FichierImage(path, nomFichier: "image");

h.AjouterImage(f);

Console.WriteLine(f.NomFichier);
*/

/*Image imgTest = Image.FromFile("P:\\zz2\\genealogie\\Ancestrel\\Image1.png");
Image img2 = (Image)imgTest.Clone();*/

//const string VALUENULL = "NULL";

Ville ville = new Ville("Blancherive", 4.444, 48.42);


string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";
//string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mavilledie4\Source\Repos\genealogie\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True";

Bdd bdd = new Bdd(chaineConnexion);


#region Inserer personne

//Personne personne = new Homme(nationalite: "française Chilienne", lieuNaissance: ville,
//dateNaissance: DateTime.Parse("16/02/2000"), dateDeces: DateTime.Parse("04/12/2048"));
//Console.WriteLine(personne.ToString());
//personne.Numero = 1;

//bdd.InsererPersonne(personne);
//Console.WriteLine("personne insérée !");
//Console.WriteLine(bdd.GetPersonneById((int)personne.Id).ToString());

#endregion

//Console.WriteLine(bdd.GetPersonneById((int)personne.Id)?.ToString() ?? "personne pas trouvée");
//Console.WriteLine(bdd.GetNationaliteByIdPays(5)?.ToString() ?? "Pays pas trouvé");

//Console.WriteLine(bdd.GetVilleById(1));


//string path = @"E:\ma_th\Documents\Programmations\pgenealogie\Ancestrel\Image1.png";
//Console.WriteLine("Fichier existe ? " + File.Exists(path));

//FichierImage f = new FichierImage(path, nomFichier: "imageToto");

//bdd.InsererFichierImage(f);
//f = bdd.GetFichierImageById((int)f.Id);
//f.SaveTest();




/**  Test arbre     **/
#region Test Arbre

//Homme cujus = new Homme(nom: "VDT", prenoms: "Toto", nationalite: "française");

//Arbre arbre = new Arbre("arbre1", "description", cujus);

//Homme pere = new Homme(nom: "Père", prenoms: "yves lucien", dateNaissance: DateOnly.Parse("21/02/1999"), nationalite: "française Chilienne");
//Femme mere = new Femme(nom: "Mère", lieuNaissance: ville);

//arbre.AjouterPere(cujus.Numero, pere);
//arbre.AjouterMere(cujus.Numero, mere);


//Console.WriteLine("\n\n Arbre à enregistrer");
//foreach (var p in arbre.Personnes.Values)
//    Console.WriteLine(p.ToString());

//bdd.InsererArbre(arbre);

//Console.WriteLine("\n\n Chargement arbre");
//Arbre arbreCharge = bdd.ChargerArbre((int)cujus.Id);

//Console.WriteLine("\n\n Arbre chargé");
//foreach (var p in arbreCharge.Personnes.Values)
//    Console.WriteLine(p.ToString());

//bdd.AjouterLienParent(cujus);

#endregion

/** Test update d'une personne **/
#region Test Update personne
//Personne p = bdd.GetPersonneById(2);

//Console.WriteLine();
//Console.WriteLine(p.ToString());
//Console.WriteLine();

//p.Nom = "Delomp";
//p.DateDeces = DateOnly.Parse("25/04/2056");

//Console.WriteLine();
//Console.WriteLine(p.ToString());
//Console.WriteLine();

//bdd.UpdatePersonne(p);

//Personne p2 = bdd.GetPersonneById(2);

//Console.WriteLine();
//Console.WriteLine(p2.ToString());
//Console.WriteLine();

#endregion



#region Test Update Prenom d'une personne

//Homme h1 = new(nom: "Delafontaine");
//h1.AddPrenoms("Jean jacques");

//Console.WriteLine(h1.ToString());
//bdd.InsererPersonne(h1);
//h1.AddPrenoms("Lustucru");
//Console.WriteLine(h1.ToString());

//bdd.UpdatePersonne(h1);

//Homme h2 = (Homme)bdd.GetPersonneById((int)h1.Id);
//Console.WriteLine(h2.ToString());

#endregion


#region Test Insertion et Update Ville

//Ville v = new Ville("Rivebois", 48.48, 102.22);

//Console.WriteLine("Ville " + v.ToString());
//bdd.InsererVille(v);

//Ville vChargee = bdd.GetVilleById((int)v.Id);

//Console.WriteLine("\nVille chargee " + vChargee.ToString());

//vChargee.Nom = "Vendeaume";
//vChargee.Longitude = 55.555;
//Console.WriteLine("\nVille modif " + vChargee.ToString());

//bdd.UpdateVille(vChargee);


//Ville vUpdate = bdd.GetVilleById((int)v.Id);
//Console.WriteLine("\nVille update charge " + vUpdate.ToString());

#endregion

#region Test Recup noms/id villes

//Dictionary<int, string> villesNom = bdd.GetNomVilles();

//foreach (var villeNom in villesNom)
//{
//    Console.WriteLine($"ville : {villeNom.Key}, {villeNom.Value}");
//}

#endregion

#region Recup nom/prenoms/id personne

//Dictionary<int, string> personnesNomPrenom = bdd.GetNomPrenomPersonnes();

//foreach (var personne in personnesNomPrenom)
//    Console.WriteLine($"personne : {personne.Key}, {personne.Value}");

#endregion