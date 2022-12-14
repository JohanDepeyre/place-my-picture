using System.Drawing;
using System.IO;

namespace ApplicationPhoto.Web.UI.Utils.ImageTraitement
{
    public static class ImageGetter
    {
        /// <summary>
        /// retourne un objet System.Drawing.image
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns> objet System.Drawing.image</returns>
        public static Image RetourneImage(string filePath)
        {
            return Image.FromFile(filePath);
        }
    }
}
