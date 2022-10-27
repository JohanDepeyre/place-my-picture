using ApplicationPhoto.Web.UI.Models;
using System.Drawing;

namespace ApplicationPhoto.Web.UI.Utils
{
    public
        class GestionImage
    {
        //constante qui gére le chemin qui sera stocké en base
        const string ImagePathDb = @"PictureFolder/";

        public Image? image { get; set; }
        public string uniqueFileName { get; set; }
        /// <summary>
        /// Chemin absolu
        /// </summary>
        public string filePath { get; set; }
      /// <summary>
      /// Chemin relatif
      /// </summary>
        public String filePathDb { get; set; }
        public GestionImage()
        {
            image = null;
            uniqueFileName = "";
            filePath = "";
            filePathDb = "";
        }
        /// <summary>
        /// sauve l'image dans le repertoire et retourne une image
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="env"></param>
        /// <returns>retourn de type Image</returns>
        public Image SaveAndReturnImage(Photo photo, IWebHostEnvironment env)
        {
            string uploadsFolder = Path.Combine(env.WebRootPath, ImagePathDb);
            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.MyImage.FileName;
            filePath = Path.Combine(uploadsFolder, uniqueFileName);
            filePathDb = Path.Combine(ImagePathDb, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.MyImage.CopyTo(fileStream);
            }
            image = Image.FromFile(filePath);
            return image;
        }
        public string SaveAndReturnImageV2(Photo photo, IWebHostEnvironment env)
        {

            string uploadsFolder = Path.Combine(env.WebRootPath, ImagePathDb);
            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.MyImage.FileName;
            filePath = Path.Combine(uploadsFolder, uniqueFileName);
            filePathDb = Path.Combine(ImagePathDb, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.MyImage.CopyTo(fileStream);
            }

            return filePath;

        }
        public Image SaveAndReturnImageV3(Photo photo)
        {

            string testchemin = @"u362639861/release/wwwroot/PictureFolder";

            // u362639861 / release / wwwroot / PictureFolder
            string uploadsFolder = Path.Combine(testchemin, ImagePathDb);
            uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.MyImage.FileName;
            filePath = Path.Combine(uploadsFolder, uniqueFileName);
            filePathDb = Path.Combine(ImagePathDb, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                photo.MyImage.CopyTo(fileStream);
            }
            image = Image.FromFile(filePath);
            return image;

        }
        public void DeleteImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        public String FileName()
        {
            return uniqueFileName;
        }
        public String FilePath()
        {
            return filePathDb;
        }

    }
}
