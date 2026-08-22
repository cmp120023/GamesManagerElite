using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Game_Manager_Elite.Models;

namespace Game_Manager_Elite.Controllers
{
    public class ApaPlayersController : Controller
    {
        private readonly ApaContext _context;

        public ApaPlayersController(ApaContext context)
        {
            _context = context;
        }

        // GET: /ApaPlayers
        public async Task<IActionResult> Index()
        {
            // Eagerly load the related Team entities so they display cleanly in the table row
            var players = await _context.ApaPlayers
                .Include(p => p.Team)
                .ToListAsync();

            return View(players);
        }

        // GET: /ApaPlayers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var player = await _context.ApaPlayers
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (player == null) return NotFound();

            return View(player);
        }

        // GET: /ApaPlayers/Create
        public IActionResult Create()
        {
            // Populate a dropdown list of available teams so you can assign the player on creation
            ViewData["ApaTeamId"] = new SelectList(_context.ApaTeams, "Id", "TeamName");
            return View();
        }

        // POST: /ApaPlayers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,MembershipNumber,EightBallSkillLevel,NineBallSkillLevel,IsActive,ApaTeamId")] ApaPlayer player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApaTeamId"] = new SelectList(_context.ApaTeams, "Id", "TeamName", player.ApaTeamId);
            return View(player);
        }

        // GET: /ApaPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var player = await _context.ApaPlayers.FindAsync(id);
            if (player == null) return NotFound();

            ViewData["ApaTeamId"] = new SelectList(_context.ApaTeams, "Id", "TeamName", player.ApaTeamId);
            return View(player);
        }

        // POST: /ApaPlayers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,MembershipNumber,EightBallSkillLevel,NineBallSkillLevel,IsActive,ApaTeamId")] ApaPlayer player)
        {
            if (id != player.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApaTeamId"] = new SelectList(_context.ApaTeams, "Id", "TeamName", player.ApaTeamId);
            return View(player);
        }

        // GET: /ApaPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var player = await _context.ApaPlayers
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (player == null) return NotFound();

            return View(player);
        }

        // POST: /ApaPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.ApaPlayers.FindAsync(id);
            if (player != null)
            {
                _context.ApaPlayers.Remove(player);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.ApaPlayers.Any(e => e.Id == id);
        }
    }
}
