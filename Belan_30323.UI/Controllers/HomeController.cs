using Microsoft.AspNetCore.Mvc;

namespace Belan_30323.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
