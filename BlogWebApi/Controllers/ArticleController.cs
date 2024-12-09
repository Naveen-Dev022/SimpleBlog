using BlogWebApi.Data;
using BlogWebApi.DTO;
using BlogWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ArticleController(AppDbContext context) { 
            _context = context;
        }
        [Route("Articles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> Articles()
        {
            IEnumerable<BlogPost> posts = await _context.Posts.ToListAsync();

            if (posts.Any())
            {
                return Ok(posts);
            }
            else
                return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Articles([FromBody] BlogPostDTO articles)
        {
            if (articles.id != 0)
                return BadRequest(articles);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            BlogPost blogPost = new BlogPost()
            {
                Title = articles.Title,
                Summary = articles.Summary,
                Body = articles.Summary,
                AuthorId = articles.AuthorId,
                Tags = articles.Tags,
                IsPremium = articles.IsPremium
            };
           await _context.AddAsync(blogPost);
           await _context.SaveChangesAsync();
            return Ok(blogPost);
        }
        [HttpDelete]
        public ActionResult Articles(int id)
        {
            var post = _context.Posts.FirstOrDefault(u => u.BlogId == id);
            if (post is null)
                return BadRequest();

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
