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
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _repository;

        public ArticleController(IArticleRepository repository)
        {
            _repository = repository;
        }

        [Route("Articles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> Articles()
        {
            var posts = await _repository.GetAllArticlesAsync();
            if (posts.Any())
            {
                return Ok(posts);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Articles([FromBody] BlogPostDTO articles)
        {
            if (articles.id != 0)
                return BadRequest(articles);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var blogPost = new BlogPost
            {
                Title = articles.Title,
                Summary = articles.Summary,
                Body = articles.Body,
                AuthorId = articles.AuthorId,
                Tags = articles.Tags,
                IsPremium = articles.IsPremium
            };

            await _repository.AddArticleAsync(blogPost);
            return Ok(blogPost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Articles(int id)
        {
            var post = await _repository.GetArticleByIdAsync(id);
            if (post == null)
                return BadRequest();

            await _repository.DeleteArticleAsync(id);
            return NoContent();
        }
    }
}
