using AutoMapper;
using E_Learning.Dtos;
using E_Learning.Helpers;
using E_Learning.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        [AllowAnonymous]
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserDto user)
        {
            if(ModelState.IsValid)
            {
                var isUserFind =await FindByNameAsync(user.UserName);
                if(isUserFind ==null)
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

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if(ModelState.IsValid)
            {
                var user =await FindByNameAsync(loginDto.userName);
                if(user !=null)
                {
                    var checkPassword = await CheckPassword(user, loginDto.Password);
                    if(checkPassword)
                    {
                        List<Claim> userClaims =await CreateCliams(user);
                        JwtSecurityToken jwtSecurity =await CreateToken(userClaims);

                        return Ok(new
                        {
                            message = "Login succesffully",
                            token = new JwtSecurityTokenHandler().WriteToken(jwtSecurity),
                            expiration = DateTime.Now.AddDays(1)

                        });
                    }
                    else
                    {
                        return BadRequest("Uncorrect password ");
                    }
                }
                else
                {
                    return NotFound($"the user {loginDto.userName} is not found");
                }
            }
            return BadRequest(ModelState);

        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto passwordDto)
        {
            if(ModelState.IsValid)
            {
                var user = await GetUserAsync();
                var checkPassword = await CheckPassword(user, passwordDto.OldPassword);
                if(checkPassword)
                {
                    var result = await userManager.ChangePasswordAsync(user, passwordDto.OldPassword, passwordDto.NewPassword);
                    if(result.Succeeded)
                    {
                        return Ok("Change Password Successfully");
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    return BadRequest("Please enter the courrect old password");
                }

            }
            return BadRequest(ModelState);
        }
      
        [Authorize]
        [HttpPost("RefrashToken")]
        public async Task<IActionResult> RefrashToken()
        {



            return BadRequest();
        }
        [Authorize]
        [HttpPost("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await GetUserAsync();
            var result = await userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return Ok($"Remove Accpunt {user.UserName} Successfully");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result =  HttpContext.SignOutAsync();
            if (result.IsCompleted)
            {
                return Ok("Logout Successfully");
            }
           
            return BadRequest(result.Exception.Data.Values);
        }
        [Authorize]
        [HttpGet("UserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var user = await GetUserAsync();

            var dto = mapper.Map<UseViewDto>(user);
            dto.Role = GetUserRole();
            return Ok(dto);
        }
        [HttpGet("GetUsersInRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersInRole([FromQuery] string roleName)
        {
            if (ModelState.IsValid)
            {
                var users =await userManager.GetUsersInRoleAsync(roleName);
                if(!users.IsNullOrEmpty())
                {
                    var dtos = mapper.Map<List<UseViewDto>>(users);
                    return Ok(dtos);
                }
                return NotFound($"No Users in this role {(roleName)}");
            }
            return BadRequest(ModelState);
        }
        private string GetUserRole()
        {
            var role = User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList().First();
            return role;
        }
        private async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user;
        }
        private async Task<ApplicationUser> GetUserAsync()
        {
            var user = await userManager.GetUserAsync(User);
            return user;
        }
        private async Task<bool> CheckPassword(ApplicationUser user,string password)
        {
            var checkPassword = await userManager.CheckPasswordAsync(user, password);

            return checkPassword;
        }
        private async Task<List<Claim>> CreateCliams(ApplicationUser user)
        {
            List<Claim> userClaims = new List<Claim>();
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            userClaims.Add(new Claim(ClaimTypes.Email, user.Email));
            userClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            userClaims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            var role = await userManager.GetRolesAsync(user);
            foreach (var item in role)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            return userClaims;
        }
        private async Task<JwtSecurityToken> CreateToken(List<Claim> userClaims)
        {
            var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));
            SigningCredentials signing = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurity = new JwtSecurityToken(
                 audience: config["JWT:AudienceIP"],
                 issuer: config["JWT:IssuerIP"],
                 expires: DateTime.Now.AddDays(1),
                 claims: userClaims,
                 signingCredentials: signing
             );
            return jwtSecurity;
        }
    }
}
