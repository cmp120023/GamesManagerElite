namespace Game_Manager_Elite.Models
{
    /// <summary>
    /// View Model used to compute and project real-time leaderboard statistics, including total matches played, wins, losses, win percentage, points scored, points allowed, and point differential.
    /// </summary>
    public class BcaTeamStandingViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int PointsScored { get; set; }
        public int PointsAllowed { get; set; }
        public int PointDifferential => PointsScored - PointsAllowed;
        public double WinPercentage => MatchesPlayed > 0 ? ((double)Wins / MatchesPlayed) * 100 : 0;
    }
}