using System.ComponentModel.DataAnnotations;

namespace LoanApp.Web.Utility
{
    public class AllowedExtensions: ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensions(string[] extensions)
        {
            _extensions = extensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if(!_extensions.Contains(fileExtension.ToLower()))
                {
                    return new ValidationResult("Please upload jpg/jpeg/png format");
                }
            }
            return ValidationResult.Success;
        }
    }
}
