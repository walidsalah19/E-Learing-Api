using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<RoleController> logger;

        public RoleController(RoleManager<IdentityRole> roleManager, ILogger<RoleController> logger)
        {
            this.roleManager = roleManager;
            this.logger = logger;
        }
        [HttpGet("AllRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllRoles()
        {
            var roles = roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost("AddRole")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddRole([FromQuery] string roleName)
        {
            if(ModelState.IsValid)
            {
                var roleFound = await FindRole(roleName);
                if (roleFound == null)
                {
                    var role = new IdentityRole();
                    role.Name = roleName;
                    var result = await roleManager.CreateAsync(role);
                    logger.LogInformation("role", result);
                    if (result.Succeeded)
                    {
                        return Ok("Add Role Successfully");
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
                else
                {
                    ModelState.AddModelError("", $"We Cant't add theis role {roleName}");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("UpdateRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([FromQuery] string roleName,[FromQuery]string newname)
        {
            if (ModelState.IsValid)
            {
                var role = await FindRole(roleName);
                if (role != null)
                {
                    role.Name = newname;
                    var result = await roleManager.UpdateAsync(role);
                    logger.LogInformation("role", result);
                    if (result.Succeeded)
                    {
                        return Ok("Update Role Successfully");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
                else
                {
                    ModelState.AddModelError("", $"We Cant't Update theis role {roleName}");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("DeleteRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole([FromQuery] string roleName)
        {
            if (ModelState.IsValid)
            {
                var role = await FindRole(roleName);
                if (role != null)
                {

                    var result =await roleManager.DeleteAsync(role);
                    return Ok(result);
                }
                else
                {
                    return NotFound("The Role Not Found");
                }
            }
            return BadRequest(ModelState);
        }

        private async Task<IdentityRole> FindRole(string roleName)
        {
            var result = await roleManager.FindByNameAsync(roleName);
            return result;
        }
    }
}
