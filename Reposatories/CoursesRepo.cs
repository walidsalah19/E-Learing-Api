﻿using E_Learning.Data;
using E_Learning.Interfaces.IRepo;
using E_Learning.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Reposatories
{
    public class CoursesRepo : ICourseRepo
    {
        private readonly AppDbContext context;
        private readonly ILogger<CoursesRepo> logger;

        public CoursesRepo(AppDbContext context, ILogger<CoursesRepo> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<string> AddCourse(Course course)
        {
            try
            {
                var result =await context.courses.AddAsync(course);
                if (result != null)
                    return "Success";
                throw new Exception("cant Add");

            }catch(Exception e)
            {
                return e.Message;
            }
        }

        public List<Course> AllCourseInCategory(string category)
        {
            var courses = context.courses.Where(x => x.category.Equals(category)).ToList();
            return courses;
        }

        public List<Course> AllCourses()
        {
            return context.courses.ToList();
        }

        public List<Course> AllStudentInCourse(int id)
        {
            var courses = context.courses.Include(x=>x.students).ToList();
            return courses;
        }

        public async Task<Course> CourseById(int id)
        {
            var course = context.courses.FirstOrDefault(x => x.Id == id);
            if (course != null)
                return course;
            return null;
        }

        public async Task<string> DeleteCourse(int id)
        {
            try
            {
                var course =await CourseById(id);
                if (course != null)
                {
                    var result = context.courses.Remove(course);
                    if (result != null)
                        return "Success";
                   
                }
                throw new Exception("cant Remove");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public List<Course> InstractureCourses(string id)
        {
            var courses = context.courses.Where(x => x.instractureId.Equals(id)).ToList();
            return courses;
        }

        public async Task<string> saveChanges()
        {
            try
            {
               await context.SaveChangesAsync();
                return "Success";

            }catch(Exception e)
            {
                return e.Message;
            }
        }

        public List<Course> StudentCourses(string id)
        {
            throw new NotImplementedException();
        }

        public List<Course> TopRatingCourses()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }
    }
}