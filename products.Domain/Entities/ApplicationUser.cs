using Microsoft.AspNetCore.Identity;

namespace products.Domain.Entities;

public class ApplicationUser: IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }

    public virtual ICollection<ApplicationRole> AspNetUserRoles { get; set; } = new List<ApplicationRole>();

}
