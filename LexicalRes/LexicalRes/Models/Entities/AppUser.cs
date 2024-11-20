using Microsoft.AspNetCore.Identity;

namespace LexicalRes.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
