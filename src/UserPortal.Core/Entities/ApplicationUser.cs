using System;
using Microsoft.AspNetCore.Identity;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Core.Entities;

public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid ProfilePictureId { get; set; }
}
