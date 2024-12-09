using BlogWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.DTO
{
    public class BlogPostDTO
    {
        public int id { get; set; }
        public required string Title { get; set; }
        public string? Summary { get; set; }
        public string? Body { get; set; }
        public string[]? Tags { get; set; }
        public required bool IsPremium { get; set; }
        [ForeignKey(nameof(AuthorDTO.AuthorId))]
        public int AuthorId { get; set; }
    }
}
