using E_Learning.Dtos;
using E_Learning.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        [Authorize(Roles = "Instracture")]
        [HttpPost("AddCourse")]
        public async Task<IActionResult> addCource([FromForm]CourseInputDto dto)
        {
            if(ModelState.IsValid)
            {
                var result =await courseService.AddCourse(dto);
                var saveResult = await courseService.saveChanges();
                if (result.Equals("Success") && saveResult.Equals("Success"))
                {
                    return Ok("Add Course successfully");
                }

                ModelState.AddModelError("",result+"\t"+saveResult);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("AllCourses")]
        public async Task<IActionResult> AllCourses()
        {
            var all = courseService.AllCourses();
            return Ok(all);
        }
        [HttpGet("Category")]
        public async Task<IActionResult> CoursesByCategory(String category)
        {
            var all = courseService.AllCourseInCategory(category);
            return Ok(all);
        }
    }
}
