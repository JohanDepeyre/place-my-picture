

namespace ApplicationPhoto.Web.UI.Models.ViewModel
{
    public class PhotoPageViewModel
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int IdVoyage { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
                
    }
}
