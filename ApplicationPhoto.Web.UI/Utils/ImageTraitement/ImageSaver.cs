using ApplicationPhoto.Web.UI.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace ApplicationPhoto.Web.UI.Utils.ImageTraitement
{
    public class ImageSaver
    {
        private readonly ImageO imageO;
        private readonly Photo photo;
        private readonly IWebHostEnvironment env;

        public ImageSaver(ImageO imageO, Photo photo, IWebHostEnvironment env)
        {
            this.imageO = imageO;
            this.photo = photo;
            this.env = env;
        }
        /// <summary>
        /// sauve l'image sur le serveur en reduisant la taille 
        /// Utilise la Lib ImageSharp
        /// </summary>
        /// <returns>retourne le chemin de l'image</returns>
        public string SaveImage()
        {
            string uploadsFolder = Path.Combine(env.WebRootPath, imageO.ImagePathDb());
            imageO.uniqueFileName = GetRandomName(photo.MyImage.FileName);
            imageO.filePath = Path.Combine(uploadsFolder, imageO.uniqueFileName);
            imageO.filePathDb = Path.Combine(imageO.ImagePathDb(), imageO.uniqueFileName);

            Image imageSave;
            using (imageSave = Image.Load(photo.MyImage.OpenReadStream()))
            {

                int[] intTaille = TailleImage(imageSave.Width, imageSave.Height);

                imageSave.Mutate(x => x.Resize(intTaille[0], intTaille[1]));

                imageSave.Save(imageO.filePath);
            } // Dispose 
            return imageO.filePath;


        }

        private string GetRandomName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }

        private int[] TailleImage(int width, int height)
        {


            int sourceWidth = width;
            int sourceHeight = height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)1000 / (float)sourceWidth);

            nPercentH = ((float)1000 / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);

            return new int[] { destWidth, destHeight };
        }

    }
}
