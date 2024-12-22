using E_Learning.Validation;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Dtos
{
    public class UserDto
    {
        [MaxLength(25)]
        [MinLength(3)]
        [Required(ErrorMessage = "Name is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Image is required.")]
        [ImageTypeValidation(new[] { ".jpg", ".jpeg", ".png", ".gif" }, 2 * 1024 * 1024)]
        public IFormFile ProfileImage { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public string UserEmail { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string UserPhone { get; set; }
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(Admin|instructor|student)$", ErrorMessage = "Role must be 'admin', 'instructor', or 'student'.")]
        public string Role { get; set; }
    }
}
