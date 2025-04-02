using BlogWebApi.Models;

namespace BlogWebApi.Data
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
    }
}
