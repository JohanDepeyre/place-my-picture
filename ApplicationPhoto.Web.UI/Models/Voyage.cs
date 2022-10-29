using System.ComponentModel.DataAnnotations;

namespace ApplicationPhoto.Web.UI.Models
{
    public class Voyage
    {
        public int VoyageId { get; set; }

        [Required(ErrorMessage ="Veuilllez saisir un titre de voyage")]
        [Display(Name = "Voyage")]
        public string NomVoyage { get; set; }

        [Required(ErrorMessage = "Veuilllez saisir une date de début de voyage")]
        [DataType(DataType.Date)]
        [Display(Name = "Date début")]
        public DateTime DateVoyageDebut { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Veuilllez saisir une date de fin de voyage")]
        [Display(Name = "Date fin")]
        public DateTime DateVoyageFin { get; set; }
        public string IdUser { get; set; }
        [Required(ErrorMessage = "Veuillez ajouter une description")]
        [Display(Name = "Description")]
        public string DescriptionVoyage { get; set; }

    }
}
