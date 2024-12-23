using System.ComponentModel.DataAnnotations;

namespace E_Learning.Dtos
{
    public class UserLoginDto
    {
        [MaxLength(25)]
        [MinLength(3)]
        [Required(ErrorMessage = "Name is required.")]
        public string userName { get; set; }
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$",
        ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character.")]
        public string Password { get; set; }
    }
}
