using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.Models
{
    [Table("BlogsMaster")]
    public class BlogPost
    {
        [Key]
        public int BlogId { get; set; }
        public required string Title { get; set; }
        public string? Summary { get; set; }
        public string? Body { get; set; }
        public string[]? Tags { get; set; }
        public required bool IsPremium { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; }
        //[ForeignKey(nameof(Author.AuthorId))]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
