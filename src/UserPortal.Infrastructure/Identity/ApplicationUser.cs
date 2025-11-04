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
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid AvatarId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
