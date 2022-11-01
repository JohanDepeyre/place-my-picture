using ApplicationPhoto.Web.UI.Models;
using System.Drawing;

namespace ApplicationPhoto.Web.UI.Utils
{
    public
        class  GestionImage
    {
       
        /// <summary>
        /// sauve l'image dans le repertoire et retourne une image
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="env"></param>
        /// <returns>retourn de type Image</returns>

      //public static Image SaveAndReturnImage(Photo photo, IWebHostEnvironment env, ImageO imageO)
      //      {
      //          string uploadsFolder = Path.Combine(env.WebRootPath, imageO.ImagePathDb());
      //          imageO.uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.MyImage.FileName;
      //          imageO.filePath = Path.Combine(uploadsFolder, imageO.uniqueFileName);
      //          imageO.filePathDb = Path.Combine(imageO.ImagePathDb(), imageO.uniqueFileName);
         

      //      using (var fileStream = new FileStream(imageO.filePath, FileMode.Create))
      //      {
      //          photo.MyImage.CopyTo(fileStream);
      //      }
      //  imageO.image = Image.FromFile(imageO.filePath);

      //      return imageO.image;
      //  }
   
    //public static System.Drawing.Image SaveAndReturnImagedd(Photo photo, IWebHostEnvironment env, ImageO imageO)
    //    {
    //        string uploadsFolder = Path.Combine(env.WebRootPath, imageO.ImagePathDb());
    //        imageO.uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.MyImage.FileName;
    //        imageO.filePath = Path.Combine(uploadsFolder, imageO.uniqueFileName);
    //        imageO.filePathDb = Path.Combine(imageO.ImagePathDb(), imageO.uniqueFileName);
    //        using (Image image = Image.Load(imageO.filePath))
    //        {
    //            // Resize the image in place and return it for chaining.
    //            // 'x' signifies the current image processing context.
    //            image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));

    //            // The library automatically picks an encoder based on the file extension then
    //            // encodes and write the data to disk.
    //            // You can optionally set the encoder to choose.
    //            image.Save(imageO.filePathDb);
    //        }


    //        return System.Drawing.Image.FromFile(imageO.filePathDb);
    //    }

        //public static void DeleteImage(string path)
        //{
        //    if (System.IO.File.Exists(path))
        //    {
        //        System.IO.File.Delete(path);
        //    }
        //}
      
    }
}
