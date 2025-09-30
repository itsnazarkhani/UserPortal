using Microsoft.AspNetCore.Mvc;

namespace UserPortal.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

    }
}
