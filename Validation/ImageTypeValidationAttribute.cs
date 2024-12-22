using System.ComponentModel.DataAnnotations;

namespace E_Learning.Validation
{
    public class ImageTypeValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;
        private readonly long _maxFileSize;

        public ImageTypeValidationAttribute(string[] allowedExtensions, long maxFileSize)
        {
            _allowedExtensions = allowedExtensions;
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return ValidationResult.Success; // Not required, allow null if no file is provided
            }

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!_allowedExtensions.Contains(extension))
            {
                return new ValidationResult($"Invalid Image type. Allowed extensions are: {string.Join(", ", _allowedExtensions)}.");
            }

            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"Image size cannot exceed {_maxFileSize / (1024 * 1024)} MB.");
            }

            return ValidationResult.Success;
        }
    }
}
