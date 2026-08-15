using GamesManagerElite.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesManagerElite.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            bool isValidUser = true;

            if (isValidUser)
            {
                if (model.LeagueType == "APA")
                {
                    return RedirectToAction("Index", "Apa");
                }
                else if (model.LeagueType == "BCA")
                {
                    return RedirectToAction("Index", "Bca");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
