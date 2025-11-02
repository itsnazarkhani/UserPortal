using System;
using Microsoft.AspNetCore.Identity;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Infrastructure.Identity;

/// <summary>
/// User entity extending IdentityUser with a GUID as the primary key.
/// Serves as application user representation in the identity system.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    
}
