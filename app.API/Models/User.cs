using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace app.API.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}