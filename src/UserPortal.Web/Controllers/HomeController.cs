using Microsoft.AspNetCore.Mvc;

namespace UserPortal.Web.Controllers;

public class HomeController : Controller
{
    public ActionResult Index() => View();
}
