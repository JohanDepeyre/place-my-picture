using ApplicationPhoto.Web.UI.Data.Migrations;
using ApplicationPhoto.Web.UI.Models;
using System.Drawing;

namespace ApplicationPhoto.Web.UI.Utils.ImageTraitement
{
    public class ImageProcessor
    {
        private readonly ImageSaver imageSaver;
        private readonly ImageDeleter imageDeleter;
        private readonly ImageGetter imageGetter;

        public ImageProcessor(ImageSaver imageSaver, ImageDeleter imageDeleter, ImageGetter imageGetter)
        {
            this.imageSaver = imageSaver;
            this.imageDeleter = imageDeleter;
            this.imageGetter = imageGetter;
        }
        public Image ProcessSave()
        {
            
            return imageGetter.RetourneImage(imageSaver.SaveImage());
        }
       
      
        public void ProcessDelete(string filePath)
        {
            imageDeleter.DeleteImage(filePath);
        }
    }
}
