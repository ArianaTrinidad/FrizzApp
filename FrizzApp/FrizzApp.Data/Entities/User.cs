using Microsoft.AspNetCore.Identity;

namespace FrizzApp.Data.Entities
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
