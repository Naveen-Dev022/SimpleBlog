using BlogWebApi.Models;

namespace BlogWebApi.Data
{
    public interface IArticleRepository
    {
        Task<IEnumerable<BlogPost>> GetAllArticlesAsync();
        Task<BlogPost?> GetArticleByIdAsync(int id);
        Task AddArticleAsync(BlogPost blogPost);
        Task DeleteArticleAsync(int id);
    }
}
