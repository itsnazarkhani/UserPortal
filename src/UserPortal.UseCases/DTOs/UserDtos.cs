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

public record class UserProfileDto(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    Guid ProfilePictureId
);

public record class UserListDto(
    string UserName,
    Guid ProfilePictureId
);