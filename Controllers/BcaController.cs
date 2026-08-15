using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game_Manager_Elite.Models;

namespace Game_Manager_Elite.Controllers
{
    public class BcaController : Controller
    {
        private readonly LeagueContext _context;

        public BcaController(LeagueContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dashboard: Serves as the central hub for the BCA module, displaying all registered teams with dynamic roster counts and a responsive navigation header.
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var teams = await _context.BcaTeams
                .Include(t => t.Roster)
                .ToListAsync();
            return View(teams);
        }

        /// <summary>
        /// Team Creation: Displays the form to register a new BCA team with name and division specifications.
        /// </summary>
        public IActionResult CreateTeam()
        {
            return View();
        }

        /// <summary>
        /// Team Creation (POST): Validates input, prevents over-posting, and persists new BcaTeam entities to SQL Server LocalDB.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTeam([Bind("TeamName,Division")] BcaTeam team)
        {
            if (ModelState.IsValid)
            {
                _context.BcaTeams.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        /// <summary>
        /// Team Details: Provides a detailed breakdown of a single team by ID, eager loading the active roster and dynamically calculating match logs with Home/Away context and W/L outcomes.
        /// </summary>
        public async Task<IActionResult> TeamDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.BcaTeams
                .Include(t => t.Roster)
                .Include(t => t.HomeMatches)
                    .ThenInclude(m => m.AwayTeam)
                .Include(t => t.AwayMatches)
                    .ThenInclude(m => m.HomeTeam)
                .FirstOrDefaultAsync(m => m.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        /// <summary>
        /// Player Creation: Displays the player registration form with a dynamic dropdown populated with all existing teams for roster assignment.
        /// </summary>
        public async Task<IActionResult> AddPlayer()
        {
            ViewBag.Teams = await _context.BcaTeams.ToListAsync();
            return View();
        }

        /// <summary>
        /// Player Creation (POST): Validates player data (including handicap ratings 1-10) and maps the foreign key assignment to a team or free agent status.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlayer([Bind("FirstName,LastName,HandicapRating,TeamId")] BcaPlayer player)
        {
            if (ModelState.IsValid)
            {
                _context.BcaPlayers.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Players));
            }

            ViewBag.Teams = await _context.BcaTeams.ToListAsync();
            return View(player);
        }

        /// <summary>
        /// Player Directory: Renders an alphabetized list of all registered league players with their handicap badges and assigned team affiliations.
        /// </summary>
        public async Task<IActionResult> Players()
        {
            var players = await _context.BcaPlayers
                .Include(p => p.Team)
                .OrderBy(p => p.LastName)
                .ToListAsync();

            return View(players);
        }

        /// <summary>
        /// Match Scoring: Displays the match logging interface with team selector dropdowns.
        /// </summary>
        public async Task<IActionResult> ScoreMatch()
        {
            ViewBag.Teams = await _context.BcaTeams.ToListAsync();
            return View();
        }

        /// <summary>
        /// Match Scoring (POST): Validates match scores, prevents duplicate team selection (home cannot equal away), and saves finalized or pending match results.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ScoreMatch([Bind("MatchDate,HomeTeamId,AwayTeamId,HomeTeamScore,AwayTeamScore,IsFinalized")] BcaMatch match)
        {
            if (match.HomeTeamId == match.AwayTeamId)
            {
                ModelState.AddModelError("AwayTeamId", "Home team and Away team cannot be the same team.");
            }

            if (ModelState.IsValid)
            {
                _context.BcaMatches.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MatchHistory));
            }

            ViewBag.Teams = await _context.BcaTeams.ToListAsync();
            return View(match);
        }

        /// <summary>
        /// Match History: Displays an audit trail of all completed fixtures sorted by date descending, highlighting winning teams and finalized statuses.
        /// </summary>
        public async Task<IActionResult> MatchHistory()
        {
            var matches = await _context.BcaMatches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .OrderByDescending(m => m.MatchDate)
                .ToListAsync();

            return View(matches);
        }

        /// <summary>
        /// League Standings: Real-time leaderboard aggregating match data via BcaTeamStandingViewModel to compute and rank teams by Wins, Point Differential, and Points Scored with podium tier badges.
        /// </summary>
        public async Task<IActionResult> Standings()
        {
            var teams = await _context.BcaTeams
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .ToListAsync();

            var standings = teams.Select(team =>
            {
                var homeMatches = team.HomeMatches.Where(m => m.IsFinalized || m.HomeTeamScore > 0 || m.AwayTeamScore > 0).ToList();
                var awayMatches = team.AwayMatches.Where(m => m.IsFinalized || m.HomeTeamScore > 0 || m.AwayTeamScore > 0).ToList();

                int homeWins = homeMatches.Count(m => m.HomeTeamScore > m.AwayTeamScore);
                int awayWins = awayMatches.Count(m => m.AwayTeamScore > m.HomeTeamScore);

                int homeLosses = homeMatches.Count(m => m.HomeTeamScore < m.AwayTeamScore);
                int awayLosses = awayMatches.Count(m => m.AwayTeamScore < m.HomeTeamScore);

                int pointsFor = homeMatches.Sum(m => m.HomeTeamScore) + awayMatches.Sum(m => m.AwayTeamScore);
                int pointsAgainst = homeMatches.Sum(m => m.AwayTeamScore) + awayMatches.Sum(m => m.HomeTeamScore);

                int played = homeMatches.Count + awayMatches.Count;

                return new BcaTeamStandingViewModel
                {
                    TeamId = team.TeamId,
                    TeamName = team.TeamName,
                    Division = team.Division,
                    MatchesPlayed = played,
                    Wins = homeWins + awayWins,
                    Losses = homeLosses + awayLosses,
                    PointsScored = pointsFor,
                    PointsAllowed = pointsAgainst
                };
            })
            .OrderByDescending(s => s.Wins)
            .ThenByDescending(s => s.PointDifferential)
            .ThenByDescending(s => s.PointsScored)
            .ToList();

            return View(standings);
        }
    }
}