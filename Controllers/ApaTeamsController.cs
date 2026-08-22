using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game_Manager_Elite.Models;

namespace Game_Manager_Elite.Controllers
{
    public class ApaTeamsController : Controller
    {
        private readonly ApaContext _context;

        public ApaTeamsController(ApaContext context)
        {
            _context = context;
        }

        // GET: ApaTeams
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApaTeams.ToListAsync());
        }

        // GET: ApaTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apateam = await _context.ApaTeams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apateam == null)
            {
                return NotFound();
            }

            return View(apateam);
        }

        // GET: ApaTeams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApaTeams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamName,Format,DivisionNumber,HostLocation,Session,Year,TotalPoints")] ApaTeam apateam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apateam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apateam);
        }

        // GET: ApaTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apateam = await _context.ApaTeams.FindAsync(id);
            if (apateam == null)
            {
                return NotFound();
            }
            return View(apateam);
        }

        // POST: ApaTeams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,TeamName,Format,DivisionNumber,HostLocation,Session,Year,TotalPoints")] ApaTeam apateam)
        {
            if (id != apateam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apateam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApaTeamExists(apateam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(apateam);
        }

        // GET: ApaTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apateam = await _context.ApaTeams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apateam == null)
            {
                return NotFound();
            }

            return View(apateam);
        }

        // POST: ApaTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var apateam = await _context.ApaTeams.FindAsync(id);
            if (apateam != null)
            {
                _context.ApaTeams.Remove(apateam);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApaTeamExists(int? id)
        {
            return _context.ApaTeams.Any(e => e.Id == id);
        }
    }
}
