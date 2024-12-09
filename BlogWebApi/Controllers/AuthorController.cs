using BlogWebApi.Data;
using BlogWebApi.DTO;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthorController(AppDbContext context)
        {
            _context = context;
        }

        [Route("Authors")]
        [HttpGet]
        public ActionResult<IEnumerable<AuthorDTO>> Authors()
        {
           var authors =  _context.Authors.Select(a => new AuthorDTO
            {
                AuthorId = a.AuthorId,
                Name = a.Name,
                Description = a.Description
            }).ToList();
            return Ok(authors);
        }

        [Route("Authors")]
        [HttpPost]
        public ActionResult<AuthorDTO> Authors([FromBody] AuthorDTO author)
        {
            if (!ModelState.IsValid || author is null)
            {
                return BadRequest(ModelState);
            }
             if(author.AuthorId > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
            Author authorAdd = new()
            {
                Name = author.Name,
                Description = author.Description,
                Deleted = false
            };
            _context.Authors.Add(authorAdd);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public ActionResult Authors(int id)
        {
            var author = _context.Authors.FirstOrDefault(u => u.AuthorId == id);
            if (author is null)
                return BadRequest();

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
