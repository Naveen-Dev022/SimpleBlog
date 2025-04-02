using BlogWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Data
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllArticlesAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<BlogPost?> GetArticleByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task AddArticleAsync(BlogPost blogPost)
        {
            await _context.Posts.AddAsync(blogPost);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
