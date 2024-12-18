using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Belan_30323.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Log.Information("Hello из метода Index контроллера Home!");
            return View();
        }
    }
}
