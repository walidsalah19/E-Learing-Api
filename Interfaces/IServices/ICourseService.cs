using E_Learning.Dtos;
using E_Learning.Models;

namespace E_Learning.Interfaces.IServices
{
    public interface ICourseService
    {
        public List<CourseOutputDto> AllCourses();
        public List<CourseOutputDto> InstractureCourses(string id);
        public List<CourseOutputDto> StudentCourses(string id);
        public List<CourseOutputDto> TopRatingCourses();
        public List<CourseOutputDto> AllCourseInCategory(string category);
        public List<CourseOutputDto> AllStudentInCourse(int id);
        public Task<CourseOutputDto> CourseById(int id);

        public Task<string> AddCourse(CourseInputDto course);
        public Task<string> UpdateCourse(CourseInputDto course);

        public Task<string> DeleteCourse(int id);

        public Task<string> saveChanges();
    }
}
