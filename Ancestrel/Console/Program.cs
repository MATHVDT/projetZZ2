// See https://aka.ms/new-console-template for more information
/**
* @file Program.cs
* @author Mathieu
* @date 28/12/2021
* @brief Fichier pour lancer en mode console
* @details Le fichier ne contient que les instructions de la fonction main
*/

using model;


/**
* @fn Main
* @brief Point d'entré de l'application en mode console
*/
Console.WriteLine("Hello, World!");

Personne p = new Personne();

p.Nom = "dupond";
p.Prenoms = "toto";

Console.WriteLine(p.Nom);

