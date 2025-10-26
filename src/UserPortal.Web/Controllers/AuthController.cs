using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserPortal.Core.Entities;
using UserPortal.Infrastructure.Data.UnitOfWork;
using UserPortal.Infrastructure.Identity;
using UserPortal.Infrastructure.Mapping;
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
        public ActionResult Login(string? returnUrl)
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

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
                return RedirectToLocal(returnUrl);

            if (result.IsLockedOut)
            {
                ViewBag.Error = "Account locked. Please try again later.";
                return View(dto);
            }

            ViewBag.Error = "Invalid login attempt.";
            return View(dto);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            // Check existing identity user by email
            var existingIdentityUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingIdentityUser != null)
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

            var identityResult = await _userManager.CreateAsync(appUser, dto.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(dto);
            }

            // Map to domain user and persist via UnitOfWork repository
            var domainUser = appUser.ToDomain();

            await _unitOfWork.Users.AddAsync(domainUser);
            await _unitOfWork.CommitAsync();

            await _signInManager.SignInAsync(appUser, isPersistent: dto.RememberMe);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                // Do not reveal whether the email exists
                ViewBag.Message = "If an account exists for that email, a password reset link has been sent.";
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action("ResetPassword", "Auth", new { token = token, email = dto.Email }, Request.Scheme);

            // TODO: Send `resetLink` to the user's email using your email service.
            // For now we just show a generic message to the user.
            ViewBag.Message = "If an account exists for that email, a password reset link has been sent.";

            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
    }
}
