using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string profilePicture { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime createdAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime updatedAt { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
