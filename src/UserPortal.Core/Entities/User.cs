using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Core.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid ProfilePictureId { get; set; }

    [NotMapped]
    public string FullName { get => $"{FirstName} {LastName}"; }
}
