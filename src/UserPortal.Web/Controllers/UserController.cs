using Microsoft.AspNetCore.Mvc;

namespace UserPortal.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}
