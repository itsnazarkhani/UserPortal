using System;
using Microsoft.AspNetCore.Identity;
using UserPortal.SharedKernel.Domain;

namespace UserPortal.Core.Entities;

/// <summary>
/// Represents a user in the application with extended profile information.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    private string? firstName;
    private string? lastName;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
    /// </summary>
    public ApplicationUser()
    {
        ProfilePictureId = Guid.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationUser"/> class with the specified email.
    /// </summary>
    /// <param name="email">The user's email address.</param>
    public ApplicationUser(string email) : this()
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        UserName = email;
        EmailConfirmed = false;
    }

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string? FirstName
    {
        get => firstName;
        set => firstName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string? LastName
    {
        get => lastName;
        set => lastName = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    /// <summary>
    /// Gets or sets the ID of the user's profile picture.
    /// </summary>
    public Guid ProfilePictureId { get; set; }

    /// <summary>
    /// Gets the full name of the user.
    /// </summary>
    /// <returns>The concatenated first and last name, or null if both are null.</returns>
    public string? FullName
    {
        get
        {
            if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                return null;

            return $"{FirstName} {LastName}".Trim();
        }
    }

    /// <summary>
    /// Gets the display name for the user, preferring full name over email.
    /// </summary>
    /// <returns>The user's full name if available, otherwise their email address.</returns>
    public string DisplayName => FullName ?? Email ?? UserName ?? "Unknown User";

    /// <summary>
    /// Updates the user's profile information.
    /// </summary>
    /// <param name="firstName">The new first name.</param>
    /// <param name="lastName">The new last name.</param>
    /// <param name="profilePictureId">The new profile picture ID.</param>
    public void UpdateProfile(string? firstName, string? lastName, Guid? profilePictureId = null)
    {
        FirstName = firstName;
        LastName = lastName;
        if (profilePictureId.HasValue)
        {
            ProfilePictureId = profilePictureId.Value;
        }
    }
}
