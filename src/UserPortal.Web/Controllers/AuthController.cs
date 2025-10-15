using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
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

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
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
    }
}
