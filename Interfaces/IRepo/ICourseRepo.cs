using E_Learning.Models;

namespace E_Learning.Interfaces.IRepo
{
    public interface ICourseRepo
    {
        public List<Course> AllCourses();
        public List<Course> InstractureCourses(string id);
        public List<Course> StudentCourses(string id);
        public List<Course> TopRatingCourses();
        public List<Course> AllCourseInCategory(string category);
        public List<Course> AllStudentInCourse(int id);
        public Task<Course> CourseById(int id);

        public Task<string> AddCourse(Course course);
        public Task<string> UpdateCourse(Course course);

        public Task<string> DeleteCourse(int id);

        public Task<string> saveChanges();
    }
}
