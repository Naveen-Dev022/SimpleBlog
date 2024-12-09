using Asp.Versioning;
using BlogWebApi.Data;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]

    [ApiVersion("1.0")]
    [ApiController]
    public class RoleController(AppDbContext appDbContext) : ControllerBase
    {
        private readonly AppDbContext _dbContext = appDbContext;

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var role = await _dbContext.Roles.ToListAsync<Role>();
            if (role?.Any() != true)
                return Ok(new { message = "No Roles found" });
            else
                return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> Roles(bool active,string roleInfo = "General")
        {
            if (roleInfo is null && active is false)
                return BadRequest();
            var roles = new Role
            {
                RoleInfo = roleInfo,
                IsActive = active,
                Created = DateTime.Now
            };
            await _dbContext.Roles.AddAsync(roles);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
