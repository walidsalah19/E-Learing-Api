using E_Learning.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Learning.Dtos
{
    public class UpdateUserDto
    {
        [MaxLength(25)]
        [MinLength(3)]
        [Required(ErrorMessage = "Name is required.")]
        public string UserName { get; set; }
        [JsonIgnore]
        [Required(ErrorMessage = "Image is required.")]
        [ImageTypeValidation(new[] { ".jpg", ".jpeg", ".png", ".gif" }, 2 * 1024 * 1024)]
        public IFormFile ProfileImage { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public string UserEmail { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required.")]
        public string UserPhone { get; set; }
    }
}
