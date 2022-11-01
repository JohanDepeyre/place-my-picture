namespace ApplicationPhoto.Web.UI.Utils.ImageTraitement
{
    public class ImageDeleter
    {
        public  void DeleteImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
