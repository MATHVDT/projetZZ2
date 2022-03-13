/**
 * @file Image.cs
 * Fichier contenant la classe Image
 * @author Mathieu
 * @date 31/12/2021
 * @copyright ...
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Model
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
         * @param string pathFile  - *Chemin de l'image à charger*
         * @param int? id = null - *Id du fichier dans la bdd*
         * @param  string nomFichier = "" - *Nom du fichier*
         */
        public FichierImage(string pathFile, int? id = null, string nomFichier = "") : base(id, nomFichier)
        {
            try
            {
                _image = Image.FromFile(pathFile);
            }
            catch (TypeInitializationException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine("using System.Drawing : bibliothèque spécifique à Windows ");
            }
            catch (OutOfMemoryException e)
            {
                Debug.WriteLine(e.ToString());
                Debug.WriteLine("Format d'image du fichier n'est pas valide.");
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("Le fichier spécifié n'existe pas.");
            }
            catch (ArgumentException)
            {
                Debug.WriteLine("ArgumentException");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            if (_image == null)
            {
                Debug.WriteLine("image non chargée");
                throw new Exception("Image non chargée");
            }
        }

        /**
         * overload public FichierImage(string pathFile, string nomFichier = "")
         * @brief Constructeur d'une nouvelle image. *sans id*
         */
        public FichierImage(string pathFile, string nomFichier = "")
            : this(pathFile, null, nomFichier) { }

        /**
         * @overload public FichierImage()
         * 
         * @param byte[] imgByte - *Image en tableau de byte*
         */
        public FichierImage(byte[] imgByte, int? id = null, string? nomFichier = null, DateTime? dateAjout = null)
            : base(dateAjout, id, nomFichier)
        {
            _image = ByteArrayToImage(imgByte);
        }


        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        public void SaveTest()
        {
            string path = @"C:\Users\ma_th\Desktop\test\";
            Debug.WriteLine(path);
            //Image.Save();

            _image.Save(path + Guid.NewGuid() + ".png", ImageFormat.Png);

        }
    }
}
