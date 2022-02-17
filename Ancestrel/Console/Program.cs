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

Personne personne = new Homme(num: 1, nom: "Dupond", prenoms: "Jean Remi jean",
                              nationalite: "française Chilienne", lieuNaissance: ville,
                              dateNaissance: DateOnly.Parse("16/02/2000"), dateDeces: DateOnly.Parse("04/12/2048"));


string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";
//string chaineConnexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mavilledie4\Source\Repos\genealogie\Ancestrel\DataBase\SampleDatabase.mdf;Integrated Security=True";

Bdd bdd = new Bdd(chaineConnexion);


bdd.InsererPersonne(personne);



Console.WriteLine(bdd.GetPersonneById((int)personne.Id)?.ToString() ?? "personne pas trouvée");
//Console.WriteLine(bdd.GetNationaliteByIdPays(5)?.ToString() ?? "Pays pas trouvé");

//Console.WriteLine(bdd.GetVilleById(1));


//string path = @"E:\ma_th\Documents\Programmations\pgenealogie\Ancestrel\Image1.png";
//Console.WriteLine(File.Exists(path));

//FichierImage f = new FichierImage(path, nomFichier: "imageToto");

//bdd.InsererFichierImage(f);
//bdd.InsererFichierImage(f);
//f = bdd.GetFichierImageById(2);
//f.SaveTest();






