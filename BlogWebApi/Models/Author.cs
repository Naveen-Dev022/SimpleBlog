using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.Models
{
    [Table("AuthorMaster")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Updated { get; set; } = DateTime.Now;
        public bool Deleted { get; set; }
        private ICollection<BlogPost>? BlogPost { get; set; }
    }
}
