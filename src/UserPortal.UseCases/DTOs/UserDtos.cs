namespace UserPortal.UseCases.DTOs;

public record class RegisterDto(
    string UserName,
    string Email,
    string Password
);

public record class LoginDto(
    string Email,
    string Password
);

public record class UserProfileDto(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    Guid ProfilePictureId
);

public record class UserListDto(
    string Username,
    Guid ProfilePictureId
);