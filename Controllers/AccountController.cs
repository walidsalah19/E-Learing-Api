using AutoMapper;
using E_Learning.Dtos;
using E_Learning.Helpers;
using E_Learning.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace E_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.config = config;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] UserDto user)
        {
            if(ModelState.IsValid)
            {
                var isUserFind =await getUser(user.UserName);
                if(isUserFind !=null)
                {
                    var userApp = mapper.Map<ApplicationUser>(user);
                    userApp.profilePicture =await UploadImage.ProcessUploadedFile(user.ProfileImage, webHostEnvironment);

                    var createValue = await userManager.CreateAsync(userApp, user.Password);
                    if (createValue.Succeeded)
                    {
                        var addRoleResult = await userManager.AddToRoleAsync(userApp, user.Role);
                        if (addRoleResult.Succeeded)
                        {
                     
                            return Created("", "the Accout was created");
                        }
                    }
                }
                
               ModelState.AddModelError("", "We cant't create an Account Please change the UserName");
                
            }
            Log.Information("", "in valid data {user}", user);
            return BadRequest(ModelState);
        }
        private async Task<ApplicationUser> getUser(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user;
        }
    }
}
