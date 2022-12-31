namespace ApplicationPhoto.Web.UI.Utils.ImageTraitement
{
    public static class  ImageDeleter
    {
        public static void DeleteImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
