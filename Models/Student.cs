namespace E_Learning.Models
{
    public class Student:ApplicationUser
    {
        public List<Rating>? Ratings { get; set; }

    }
}
