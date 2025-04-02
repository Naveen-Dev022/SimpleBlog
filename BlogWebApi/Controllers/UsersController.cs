using Asp.Versioning;
using BlogWebApi.Data;
using BlogWebApi.DTO;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    //[ApiVersion("1.0")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Users()
        {
            var Users = _context.Users.Select(a => new UserDTO
            {
                UserId = a.UserId,
                UserName = a.UserName,
                Password = a.Password,
                Email = a.Email,
                RoleId = a.RoleId
            }).ToList();
            return Ok(Users);
        }

        [HttpPost]
        public ActionResult<AuthorDTO> Users([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid || user is null)
            {
                return BadRequest(ModelState);
            }
            if (user.UserId > 0)
                return BadRequest();
            User userAdd = new()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                RoleId = user.RoleId
            };
            _context.Users.Add(userAdd);
            _context.SaveChanges();
            return Ok(new {message="User Added Successfully."});
        }

    }
}
