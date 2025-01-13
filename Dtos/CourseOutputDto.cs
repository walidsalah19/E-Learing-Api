using E_Learning.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Dtos
{
    public class CourseOutputDto : CourseInputDto
    {
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        [Required]
        public string thumbnail { get; set; }

        public List<RatingOutputDto>? Ratings { get; set; }
    }
}
