using System.ComponentModel.DataAnnotations;

namespace ApplicationPhoto.Web.UI.Models.ViewModel
{
    public class FilesPhotoViewModel
    {
        [Required(ErrorMessage = "Please select files")]
         public List<IFormFile> Files { get; set; }
     
        public Photo Photos { get; set; }

    }
}
