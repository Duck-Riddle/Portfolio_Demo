using Microsoft.AspNetCore.Identity;

namespace WebApp.Data.Entities;

public class IdentityAppUser : IdentityUser
{
    public IdentityAppUser()
    {
        Aliases = new HashSet<Alias>();
    }
    public virtual ICollection<Alias> Aliases { get; set; }
}