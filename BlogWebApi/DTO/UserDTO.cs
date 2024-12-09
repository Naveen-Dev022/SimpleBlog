using BlogWebApi.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApi.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8)]
        [ProtectedPersonalData]
        public required string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public int IsActive { get; set; }
        public int RoleId { get; set; }
    }
}
