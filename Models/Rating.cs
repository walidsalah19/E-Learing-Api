using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
    public class Rating
    {
        public int id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string? StudentId { get; set; }
        public Student? Student { get; set; }
        public int rating { get; set; }
        public string description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ratingTime { get; set; }
    }
}
