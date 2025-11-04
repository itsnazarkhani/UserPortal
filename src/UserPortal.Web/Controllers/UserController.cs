using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserPortal.Infrastructure.Identity;
using UserPortal.UseCases.DTOs;

namespace UserPortal.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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

        public IActionResult Settings()
        {
            return View();
        }
    }
}