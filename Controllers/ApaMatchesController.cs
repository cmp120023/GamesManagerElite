using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Game_Manager_Elite.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Game_Manager_Elite.Controllers
{
    public class ApaMatchesController : Controller
    {
        private readonly ApaContext _context;

        public ApaMatchesController(ApaContext context)
        {
            _context = context;
        }

        // GET: /ApaMatches
        public async Task<IActionResult> Index()
        {
            // Fetch match logs eagerly loading the parent Team info and child player matches
            var matches = await _context.ApaMatches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.PlayerMatches)
                    .ThenInclude(pm => pm.HomePlayer)
                .Include(m => m.PlayerMatches)
                    .ThenInclude(pm => pm.AwayPlayer)
                .OrderByDescending(m => m.MatchDate)
                .ToListAsync();

            return View(matches);
        }

        // GET: /ApaMatches/Create
        public IActionResult Create()
        {
            // Dropdown loaders for selection configuration components
            ViewData["HomeTeamId"] = new SelectList(_context.ApaTeams, "Id", "TeamName");
            ViewData["AwayTeamId"] = new SelectList(_context.ApaTeams, "Id", "TeamName");

            return View();
        }

        // POST: /ApaMatches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApaMatch match, List<ApaPlayerMatch> games)
        {
            if (match.HomeTeamId == match.AwayTeamId)
            {
                ModelState.AddModelError("", "A team cannot play against itself.");
            }

            if (games == null || games.Count != 5)
            {
                ModelState.AddModelError("", "A valid APA score sheet must contain exactly 5 games.");
            }

            if (ModelState.IsValid && games != null)
            {
                // Automatically aggregate the individual game point structures to get overall team points
                match.HomeMatchPoints = games.Sum(g => g.HomePointsEarned);
                match.AwayMatchPoints = games.Sum(g => g.AwayPointsEarned);

                // Formally link child games array context to this parent ledger sheet container
                match.PlayerMatches = games;

                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["HomeTeamId"] = new SelectList(_context.ApaTeams, "Id", "TeamName", match.HomeTeamId);
            ViewData["AwayTeamId"] = new SelectList(_context.ApaTeams, "Id", "TeamName", match.AwayTeamId);
            return View(match);
        }

        // ASYNC JSON ENDPOINT: /ApaMatches/GetRosterAndSkills?teamId=5&format=0
        [HttpGet]
        public async Task<JsonResult> GetRosterAndSkills(int teamId, GameFormat format)
        {
            var players = await _context.ApaPlayers
                .Where(p => p.ApaTeamId == teamId && p.IsActive)
                .Select(p => new
                {
                    id = p.Id,
                    fullName = p.FirstName + " " + p.LastName,
                    // Automatically filter your SL tracking depending on whether it's 8-ball or 9-ball rules
                    skillLevel = (format == GameFormat.EightBall) ? p.EightBallSkillLevel : p.NineBallSkillLevel
                })
                .ToListAsync();

            return Json(players);
        }
    }
}
