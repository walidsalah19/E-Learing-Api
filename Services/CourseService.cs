using AutoMapper;
using E_Learning.Dtos;
using E_Learning.Helpers;
using E_Learning.Interfaces.IRepo;
using E_Learning.Interfaces.IServices;
using E_Learning.Models;
using Microsoft.AspNetCore.Hosting;

namespace E_Learning.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMapper mapper;
        private readonly ICourseRepo repo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CourseService(IMapper mapper, ICourseRepo repo, IWebHostEnvironment webHostEnvironment)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> AddCourse(CourseInputDto course)
        {
            var pecture = await ImageHelper.ProcessUploadedImage(course.thumbnail, webHostEnvironment);
            var c1 = mapper.Map<Course>(course);
            c1.thumbnail = pecture;
            c1.createdAt = DateTime.UtcNow;
            var result = await repo.AddCourse(c1);
            return result;
        }

        public List<CourseOutputDto> AllCourseInCategory(string category)
        {
            var courses = repo.AllCourseInCategory(category);
            var dto = mapper.Map<List<CourseOutputDto>>(courses);
            return dto;
        }

        public List<CourseOutputDto> AllCourses()
        {
            var courses = repo.AllCourses();
            var dto = mapper.Map<List<CourseOutputDto>>(courses);
            return dto.ToList();
        }

        public List<CourseOutputDto> AllStudentInCourse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CourseOutputDto> CourseById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteCourse(int id)
        {
            throw new NotImplementedException();
        }

        public List<CourseOutputDto> InstractureCourses(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> saveChanges()
        {
            var result = await repo.saveChanges();
            return result;
        }

        public List<CourseOutputDto> StudentCourses(string id)
        {
            throw new NotImplementedException();
        }

        public List<CourseOutputDto> TopRatingCourses()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateCourse(CourseInputDto course)
        {
            throw new NotImplementedException();
        }
    }
}
