using Microsoft.AspNetCore.Identity;

namespace products.Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();

}

