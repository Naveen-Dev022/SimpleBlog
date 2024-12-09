using Asp.Versioning;
using BlogWebApi.Data;
using BlogWebApi.DTO;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Controllers.V2
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class RoleController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _dbContext = appDbContext;
        public async Task<IActionResult> Roles()
        {
            var role = await _dbContext.Roles.Where(a => a.IsActive == true).ToListAsync<Role>();
            if (role?.Any() != true)
                return Ok(new { message = "No Roles found" });
            else
                return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> Roles([FromBody] RoleDTO role)
        {
            if (role is null)
                return BadRequest();
            var roles = new Role
            {
                RoleInfo = role.RoleInfo,
                IsActive = role.IsActive,
                Created = DateTime.Now
            };
            await _dbContext.Roles.AddAsync(roles);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
