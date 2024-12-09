using System.ComponentModel.DataAnnotations;

namespace BlogWebApi.DTO
{
    public class AuthorDTO
    {
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
