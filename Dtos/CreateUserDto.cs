using E_Learning.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Learning.Dtos
{
    public class CreateUserDto :UpdateUserDto
    {

        [Required]
        [JsonIgnore]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(Admin|instructor|student)$", ErrorMessage = "Role must be 'admin', 'instructor', or 'student'.")]
        public string Role { get; set; }
    }
}
