using E_Learning.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Learning.Dtos
{
    public class RatingInputDto
    {
        [Required]
        [JsonIgnore]
        public int CourseId { get; set; }
        [Required]
        public int rating { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string description { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime ratingTime { get; set; }
    }
}
