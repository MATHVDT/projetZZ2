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
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using model;


/**
* @fn Main
* @brief Point d'entré de l'application en mode console
*/
Console.WriteLine("Hello, World!");

Manager m = Manager.GetInstance();

Femme p = new Femme(1);

p.Nom = "dupond";
p.DateNaissance = DateOnly.FromDateTime(DateTime.Today);

Console.WriteLine(p.Nom);
Console.WriteLine("Personne : " + p.ToString());

Homme h = new(num: 1, nom: "Dupond", prenoms: "Jean Remi",
   dateNaissance: DateOnly.Parse("16/02/2000"));



Console.WriteLine(); 
Console.WriteLine(h.ToString());
Console.WriteLine(h.Prenoms);
Console.WriteLine(h.GetPrenoms());


Console.WriteLine(File.Exists("C:/Users/emper/OneDrive/Documents/ISIMA/ZZ2/Projet/Ancestrel/Console/Image1.jpg"));
string path = "C:\\Users\\emper\\OneDrive\\Documents\\ISIMA\\ZZ2\\Projet\\Ancestrel\\Console\\Image1.jpg";

FichierImage f = new FichierImage(path, "image");

h.AjouterImage(f);

Console.WriteLine(f.NomFichier);


/*Image imgTest = Image.FromFile("P:\\zz2\\genealogie\\Ancestrel\\Image1.png");
Image img2 = (Image)imgTest.Clone();*/



