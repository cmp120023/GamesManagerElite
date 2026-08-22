using GamesManagerElite.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesManagerElite.Controllers
{
    public class HomeController : Controller
    {
        // 1. GET Action: Loads the combined landing page immediately
        [HttpGet]
        public IActionResult Index()
        {
            // Set APA as the default selected radio option on first load
            var viewModel = new LoginViewModel { LeagueType = "APA" };
            return View(viewModel);
        }

        // 2. POST Action: Processes the login bypass and handles routing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Your testing bypass rule
            bool isValidUser = true;

            if (isValidUser)
            {
                if (model.LeagueType == "APA")
                {
                    return RedirectToAction("Index", "ApaDashboard");
                }
                else if (model.LeagueType == "BCA")
                {
                    return RedirectToAction("Index", "Bca");
                }
            }

            return View(model);
        }
    }
}
