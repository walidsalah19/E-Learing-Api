namespace E_Learning.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string contentUrl { get; set; }
        public string duration { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updateAt { get; set; }

        public int courseId { get; set; }
        public Course course { get; set; }

    }
}
