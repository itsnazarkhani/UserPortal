using System;
using UserPortal.Core.Entities;
using UserPortal.UseCases.DTOs;

namespace UserPortal.UseCases.Mapping;

/// <summary>
/// Mapping class providing usefull extension methods for mapping ApplicationUser related mappings.
/// </summary>
public static class UserMappings
{
    /// <summary>
    /// Maps ApplicationUser entity to a UserProfileDto dto.
    /// </summary>
    /// <param name="user">ApplicationUser instance to be mapped.</param>
    /// <returns>Returns a UserProfileDto dto containing informations which will be shown in profile page of users.</returns>
    public static UserProfileDto ToProfileDto(this ApplicationUser user) =>
        new UserProfileDto(
            user?.UserName ?? "No-User",
            user?.FirstName ?? "ناشناس",
            user?.LastName ?? string.Empty,
            user?.Email ?? "No-User",
            user?.ProfilePictureId ?? Guid.Empty);

    /// <summary>
    /// Maps ApplicationUser entity to a UserListDto dto.
    /// </summary>
    /// <param name="user">ApplicationUser instance to be mapped.</param>
    /// <returns>Returns a UserListDto dto which will be shown in users list page.</returns>
    public static UserListDto ToUserListDto(this ApplicationUser user) =>
        new UserListDto(user?.UserName ?? "No-User", user?.ProfilePictureId ?? Guid.Empty);
}
