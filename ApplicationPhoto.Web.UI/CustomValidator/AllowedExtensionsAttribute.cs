using System.ComponentModel.DataAnnotations;

namespace ApplicationPhoto.Web.UI.CustomValidator
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }


#pragma warning disable CS8765 // La nullabilité de type du paramètre ne correspond pas au membre substitué (probablement en raison des attributs de nullabilité).
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
#pragma warning restore CS8765 // La nullabilité de type du paramètre ne correspond pas au membre substitué (probablement en raison des attributs de nullabilité).
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }


            return ValidationResult.Success ;

        }

        public string GetErrorMessage()
        {
            return $"This photo extension is not allowed!";
        }
    }
}
