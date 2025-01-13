﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_Learning.Dtos
{
    public class CourseInputDto
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [JsonIgnore]
        [Required]
        public IFormFile thumbnail { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string language { get; set; }
        [RegularExpression("^(Begginner|Midlevel|Advances)$", ErrorMessage = "Role must be 'admin', 'instructor', or 'student'.")]
        [Required]
        public string level { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public string instractureId { get; set; }
    }
}