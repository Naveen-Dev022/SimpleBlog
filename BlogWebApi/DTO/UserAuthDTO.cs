using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApi.DTO
{
    public class UserAuthDTO
    {
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8)]
        [ProtectedPersonalData]
        public required string Password { get; set; }
    }
}
