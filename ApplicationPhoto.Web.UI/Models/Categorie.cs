using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationPhoto.Web.UI.Models
{
    [Table("Categorie")]
    public class Categorie
    {
        [Key]
        public int CategorieId { get; set; }
        public string? NameCategorie { get; set; }


  

    }
}
