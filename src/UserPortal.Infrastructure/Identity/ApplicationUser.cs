using System;
using Microsoft.AspNetCore.Identity;
using UserPortal.Core.Entities;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Infrastructure.Identity;

/// <summary>
/// Identity user of application which has 1-1 relationship with domain user.
/// It will be mapped to domain user, i.e. User entity, or mapped back to this 
/// class from a domain user instance.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    // 1-1 relationship with doamin user
    public int DomainUserId { get; set; }
    public User User { get; set; } = null!;
}
