using ApplicationPhoto.Web.UI.CustomValidator;
using ApplicationPhoto.Web.UI.utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ApplicationPhoto.Web.UI.Models
{
    [Table("Photo")]
    public class Photo
    {

        [Key]
        public int PhotoId { get; set; }
        public string? Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set;}
        [Display(Name = "Url")]
        public string? ImageUrl { get; set; }
        [Display(Name = "Date")]
        public DateTime? DatePicture { get; set; }
        [NotMapped]

        [Required(ErrorMessage = "Selectionner un fichier.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile MyImage { set; get; }
        [Required(ErrorMessage = "Veuillez saisir une description")]
        public string? Description   { get; set; }
      
        public int CategorieId { get; set; }

        public Categorie? Categorie { get; set; }
        
        public int VoyageId { get; set; }
        public Voyage? Voyage { get; set; }
        }
}
