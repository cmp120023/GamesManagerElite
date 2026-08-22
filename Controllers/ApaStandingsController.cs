using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Game_Manager_Elite.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Game_Manager_Elite.Controllers
{
    public class ApaStandingsController : Controller
    {
        private readonly ApaContext _context;

        public ApaStandingsController(ApaContext context)
        {
            _context = context;
        }

        // GET: /ApaStandings
        public async Task<IActionResult> Index()
        {
            // 1. Fetch all teams in the database
            var teams = await _context.ApaTeams.ToListAsync();

            // 2. Fetch all completed matches to aggregate real scores
            var matches = await _context.ApaMatches.ToListAsync();

            var dynamicStandings = new List<ApaTeamStanding>();

            // 3. Loop through each team and calculate real-time stats from match data
            foreach (var team in teams)
            {
                // Filter matches where this team played as Home or Away
                var homeMatches = matches.Where(m => m.HomeTeamId == team.Id).ToList();
                var awayMatches = matches.Where(m => m.AwayTeamId == team.Id).ToList();

                // Sum up actual match points recorded on score sheets
                int totalPointsEarned = homeMatches.Sum(m => m.HomeMatchPoints) +
                                       awayMatches.Sum(m => m.AwayMatchPoints);

                // Calculate Match Wins and Losses based on who scored higher in individual matchups
                int matchesWon = homeMatches.Count(m => m.HomeMatchPoints > m.AwayMatchPoints) +
                                 awayMatches.Count(m => m.AwayMatchPoints > m.HomeMatchPoints);

                int matchesLost = homeMatches.Count(m => m.HomeMatchPoints < m.AwayMatchPoints) +
                                  awayMatches.Count(m => m.AwayMatchPoints < m.HomeMatchPoints);

                // Total weeks played is simply the total match sheets logged for this team
                int weeksPlayed = homeMatches.Count + awayMatches.Count;

                var standingRecord = new ApaTeamStanding
                {
                    ApaTeamId = team.Id,
                    Team = team,
                    DivisionNumber = team.DivisionNumber,
                    Session = team.Session,
                    Year = team.Year,
                    TotalPoints = totalPointsEarned,
                    WeeksPlayed = weeksPlayed,
                    MatchesWon = matchesWon,
                    MatchesLost = matchesLost
                };

                dynamicStandings.Add(standingRecord);
            }

            // 4. Sort the list by highest total points earned, then assign ranks dynamically
            var sortedStandings = dynamicStandings
                .OrderByDescending(s => s.TotalPoints)
                .ToList();

            int rankCounter = 1;
            foreach (var standing in sortedStandings)
            {
                standing.CurrentRank = rankCounter++;
            }

            return View(sortedStandings);
        }
    }
}
