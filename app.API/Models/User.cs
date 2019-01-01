using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace app.API.Models
{
    public class User : IdentityUser<int>
    {
        public byte[] PasswordHashX { get; set; }
        public byte[] PasswordSaltX { get; set; }   
        public ICollection<UserRole> UserRoles { get; set; }
    }
}