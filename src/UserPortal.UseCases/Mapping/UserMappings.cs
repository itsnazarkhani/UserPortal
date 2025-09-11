using System;
using UserPortal.Core.Entities;
using UserPortal.UseCases.DTOs;

namespace UserPortal.UseCases.Mapping;

/// <summary>
/// Mapping class providing useful extension methods for mapping ApplicationUser related mappings.
/// </summary>
public static class UserMappings
{
    private const string DefaultUsername = "Anonymous";
    private const string DefaultFirstName = "Unknown";

    /// <summary>
    /// Maps ApplicationUser entity to a UserProfileDto dto.
    /// </summary>
    /// <param name="user">ApplicationUser instance to be mapped. Can be null, in which case default values are used.</param>
    /// <returns>Returns a UserProfileDto dto containing information which will be shown in profile page of users.</returns>
    /// <remarks>If the user is null, default values will be used for all properties.</remarks>
    public static UserProfileDto ToProfileDto(this User? user) =>
        new(
            user?.UserName ?? DefaultUsername,
            user?.FirstName ?? DefaultFirstName,
            user?.LastName ?? string.Empty,
            user?.Email ?? $"{DefaultUsername}@noemail.com",
            user?.AvatarId ?? Guid.Empty);

    /// <summary>
    /// Maps ApplicationUser entity to a UserListDto dto.
    /// </summary>
    /// <param name="user">ApplicationUser instance to be mapped. Can be null, in which case default values are used.</param>
    /// <returns>Returns a UserListDto dto which will be shown in users list page.</returns>
    /// <remarks>If the user is null, default values will be used for all properties.</remarks>
    public static UserListDto ToUserListDto(this User? user) =>
        new(user?.UserName ?? DefaultUsername, user?.AvatarId ?? Guid.Empty);
}
