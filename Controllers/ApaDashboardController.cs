using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game_Manager_Elite.Models;

namespace Game_Manager_Elite.Controllers
{
    public class ApaDashboardController : Controller
    {
        private readonly ApaContext _context;

        public ApaDashboardController(ApaContext context)
        {
            _context = context;
        }

        // GET: /ApaDashboard
        public async Task<IActionResult> Index()
        {
            // Pull teams and eagerly load their respective player collections to count rosters
            var teams = await _context.ApaTeams
                .Include(t => t.Roster)
                .ToListAsync();

            return View(teams);
        }
    }
}
