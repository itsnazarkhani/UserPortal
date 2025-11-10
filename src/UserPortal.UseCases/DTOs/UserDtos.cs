using Microsoft.AspNetCore.Http;
using System;

namespace UserPortal.UseCases.DTOs;

public record class RegisterDto(
    string UserName,
    string Email,
    string Password,
    bool RememberMe = false
);

public record class LoginDto(
    string Email,
    string Password,
    bool RememberMe = false
);

public record class ForgotPasswordDto(
    string Email
);

public record class UserProfileDto(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    Guid AvatarId
);

public class UserSettingsDto
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public IFormFile? Avatar { get; set; }
    public Guid AvatarId { get; set; }
}
