using System;
using UserPortal.Core.Entities;
using UserPortal.Infrastructure.Identity;

namespace UserPortal.Infrastructure.Mapping;

/// <summary>
/// Extension class intended to provide mapping between domain user and identity user.
/// </summary>
public static class IdentityMappings
{
    /// <summary>
    /// Mapps identity user to domain user
    /// </summary>
    /// <exception cref="ArgumentNullException">Will be thrown if UserName or Email are null.</exception>
    public static User ToDomain(this ApplicationUser appUser) =>
        new User
        {
            Id = appUser.Id,
            UserName = appUser?.UserName ?? throw new ArgumentNullException("UserName should not be null"),
            Email = appUser?.Email ?? throw new ArgumentNullException("Email should not be null")
        };

    /// <summary>
    /// Maps domain user to Identity version, i.e. ApplicationUser which inherits from IdentityUser
    /// </summary>
    public static ApplicationUser ToIdentity(this User user) =>
        new ApplicationUser
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
}
