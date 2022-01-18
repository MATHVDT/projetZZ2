/**
 * @file Image.cs
 * Fichier contenant la classe Image
 * @author Mathieu
 * @date 31/12/2021
 * @copyright ...
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace model
{
    /**
     * @class FichierImage
     * @brief Classe pour les fichiers de type images.
     */
    public sealed class FichierImage : Fichier
    {
        private readonly Image _image;
        public Image Image { get { return _image; } }


        /**
         * @fn public FichierImage(string filename, string nomFichier)
         * @brief Constructeur d'un FichierImage
         * @param string filename  - *Chemin de l'image à charger*
         */
        public FichierImage(string filename, string nomFichier = "") : base(nomFichier)
        {
            try
            {
                _image = Image.FromFile(filename);
            }
            catch (TypeInitializationException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("using System.Drawing : bibliothèque spécifique à Windows ");
            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Format d'image du fichier n'est pas valide.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Le fichier spécifié n'existe pas.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
            if(_image==null)
            {
                Console.WriteLine("image non chargée");
                throw new Exception("Image non chargée");
            }

        }
    }
}
