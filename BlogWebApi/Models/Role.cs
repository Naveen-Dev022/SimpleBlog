using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.Models
{
    [Table("RoleMaster")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public required string RoleInfo { get; set; }
        public required bool IsActive { get; set; }

        public required DateTime Created {  get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Updated { get; set; }
        public ICollection<User> UserRecord { get; set; }
    }
}
