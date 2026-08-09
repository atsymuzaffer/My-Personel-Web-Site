using Microsoft.AspNetCore.Identity;

namespace PortfolioSite.Entities;

public class ApplicationUser : IdentityUser
{
    public bool MustChangeCredentials { get; set; } = false;
}
