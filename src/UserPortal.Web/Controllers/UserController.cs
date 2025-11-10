using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using UserPortal.Infrastructure.Identity;
using UserPortal.UseCases.DTOs;

namespace UserPortal.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _env;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment env)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _env = env;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var model = new UserProfileDto(
                UserName: user.UserName ?? "username",
                FirstName: user.FirstName ?? "Not Set",
                LastName: user.LastName ?? "Not Set",
                Email: user.Email ?? "Not Set",
                AvatarId: user.AvatarId != Guid.Empty ? user.AvatarId : Guid.Empty
            );

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var model = new UserSettingsDto
            {
                UserName = user.UserName ?? "",
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Email = user.Email ?? "",
                Avatar = null,
                AvatarId = user.AvatarId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(UserSettingsDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
            {
                // Re-populate form with submitted data on validation error
                var errorModel = new UserSettingsDto
                {
                    UserName = user.UserName ?? "",
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = user.Email ?? "",
                    AvatarId = user.AvatarId
                };
                return View(errorModel);
            }

            // Update user properties
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            // Handle avatar upload if provided
            if (dto.Avatar != null && dto.Avatar.Length > 0)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "avatars");
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Delete old avatar if it exists
                if (user.AvatarId != Guid.Empty)
                {
                    var oldAvatarPath = Path.Combine(uploadPath, $"{user.AvatarId}.jpg");
                    if (System.IO.File.Exists(oldAvatarPath))
                        System.IO.File.Delete(oldAvatarPath);
                }

                var newAvatarId = Guid.NewGuid();
                var fileExtension = Path.GetExtension(dto.Avatar.FileName).ToLowerInvariant();
                var fileName = $"{newAvatarId}{fileExtension}";
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await dto.Avatar.CopyToAsync(stream);

                user.AvatarId = newAvatarId;
            }

            var result = await _userManager.UpdateAsync(user);
            // Remove old AvatarId claim
            var claims = await _userManager.GetClaimsAsync(user);
            var oldAvatarClaim = claims.FirstOrDefault(c => c.Type == "AvatarId");
            if (oldAvatarClaim != null)
                await _userManager.RemoveClaimAsync(user, oldAvatarClaim);

            // Add updated AvatarId claim
            await _userManager.AddClaimAsync(user, new Claim("AvatarId", user.AvatarId.ToString()));

            // Refresh sign-in with updated claims
            await _signInManager.RefreshSignInAsync(user);
            if (result.Succeeded)
            {
                TempData["Message"] = "Settings updated successfully.";

                return RedirectToAction(nameof(Settings));
            }

            // Add any identity errors to ModelState
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            // Return with error messages
            var errorReturnModel = new UserSettingsDto
            {
                UserName = user.UserName ?? "",
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = user.Email ?? "",
                AvatarId = user.AvatarId
            };
            return View(errorReturnModel);
        }
    }
}
