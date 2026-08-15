using Microsoft.AspNetCore.Mvc;

namespace GamesManagerElite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
