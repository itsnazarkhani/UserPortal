using Microsoft.AspNetCore.Mvc;

namespace UserPortal.Web.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        
        public ActionResult Logout()
        {
            return View();
        }
    }
}
