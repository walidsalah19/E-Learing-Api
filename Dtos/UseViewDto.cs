using System.ComponentModel.DataAnnotations;

namespace E_Learning.Dtos
{
    public class UseViewDto : CreateUserDto
    {
        [DataType(DataType.DateTime)]
        public DateTime createdAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime updatedAt { get; set; }
        public String ProfileImage { get; set; }
    }
}
