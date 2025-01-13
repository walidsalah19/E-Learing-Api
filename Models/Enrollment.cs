using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
    public class Enrollment
    {
        public int courseId { get; set; }
        public string studentId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime enrolledAt { get; set; }
    }
}
