using System.Drawing;

namespace ApplicationPhoto.Web.UI.Utils
{
    public class ImageO
    {
        private string _ImagePathDb = @"PictureFolder/" ;
        public string ImagePathDb() { 
              return _ImagePathDb;
        }
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

        public double W { get; set; }
        public double H { get; set; }
    }
}
