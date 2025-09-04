using System;

namespace UserPortal.UseCases.DTOs;

/// <summary>
/// Data transfer object for user registration.
/// </summary>
public record class RegisterDto(
    string UserName,
    string Email,
    string Password,
    bool RememberMe = false
);

/// <summary>
/// Data transfer object for user login.
/// </summary>
public record class LoginDto(
    string Email,
    string Password,
    bool RememberMe = false
);

/// <summary>
/// Data transfer object for user profile information.
/// </summary>
public record class UserProfileDto(
    string UserName,
    string? FirstName,
    string? LastName,
    string Email,
    Guid ProfilePictureId
)
{
    /// <summary>
    /// Gets the full name of the user.
    /// </summary>
    public string FullName => string.Join(" ", new[] { FirstName, LastName }.Where(x => !string.IsNullOrEmpty(x)));
};

/// <summary>
/// Data transfer object for displaying user in lists.
/// </summary>
public record class UserListDto(
    string UserName,
    Guid ProfilePictureId
);