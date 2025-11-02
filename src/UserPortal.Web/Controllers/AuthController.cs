using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserPortal.Infrastructure.Data.UnitOfWork;
using UserPortal.Infrastructure.Identity;
using UserPortal.UseCases.DTOs;

namespace UserPortal.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string? returnUrl, [FromForm] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                ViewBag.Error = "Invalid login attempt.";
                return View(dto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, true);

            if (result.Succeeded)
                return RedirectToLocal(returnUrl);

            ViewBag.Error = result.IsLockedOut
                ? "Account locked. Please try again later."
                : "Invalid login attempt.";

            return View(dto);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            if (await _userManager.FindByEmailAsync(dto.Email) != null)
            {
                ModelState.AddModelError(string.Empty, "Email is already registered.");
                return View(dto);
            }

            var appUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(dto);
            }

            await _unitOfWork.Users.AddAsync(appUser);
            await _unitOfWork.CommitAsync();

            await _signInManager.SignInAsync(appUser, dto.RememberMe);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action("ResetPassword", "Auth", new { token, email = dto.Email }, Request.Scheme);
                // TODO: send resetLink via email
            }

            ViewBag.Message = "If an account exists for that email, a password reset link has been sent.";
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied() => View();

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string? returnUrl) =>
            !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? Redirect(returnUrl)
                : RedirectToAction("Index", "Home");
    }
}
