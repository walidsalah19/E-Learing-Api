using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace E_Learning.Models
{
    [Table("Course")]
    public class Course
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string thumbnail { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string level { get; set; }
        public decimal price { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime createdAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime updatedAt { get; set; }
        public string? instractureId { get; set; }
        public Instractor instracture { get; set; }
        public List<Lesson>? lessons { get; set; }
        public List<Student>? students { get;set; }
        public List<Rating>? Ratings { get; set; }

    }
}
