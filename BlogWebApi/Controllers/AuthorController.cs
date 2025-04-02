using BlogWebApi.Data;
using BlogWebApi.DTO;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _repository;

        public AuthorController(IAuthorRepository repository)
        {
            _repository = repository;
        }

        [Route("Authors")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> Authors()
        {
            var authors = await _repository.GetAllAuthorsAsync();
            var authorDTOs = authors.Select(a => new AuthorDTO
            {
                AuthorId = a.AuthorId,
                Name = a.Name,
                Description = a.Description
            }).ToList();
            return Ok(authorDTOs);
        }

        [Route("Authors")]
        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> Authors([FromBody] AuthorDTO author)
        {
            if (!ModelState.IsValid || author is null)
            {
                return BadRequest(ModelState);
            }
            if (author.AuthorId > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var authorAdd = new Author
            {
                Name = author.Name,
                Description = author.Description,
                Deleted = false
            };

            await _repository.AddAuthorAsync(authorAdd);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Authors(int id)
        {
            var author = await _repository.GetAuthorByIdAsync(id);
            if (author == null)
                return BadRequest();

            await _repository.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
