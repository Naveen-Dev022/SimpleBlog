using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.Models
{
    [Table("UserMaster")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8)]
        [ProtectedPersonalData]
        public required string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public required string Email { get; set; }
        public DateTime Created {  get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastUpdated { get; set; }
        public int IsActive { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
