using System;
using Microsoft.AspNetCore.Identity;
using UserPortal.Core.Entities;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    // 1-1 relationship with doamin user
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
