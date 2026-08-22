using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game_Manager_Elite.Models
{
    public class ApaTeamStanding
    {
        [Key]
        public int Id { get; set; }



        [Required, Display(Name = "Team")]
        public int ApaTeamId { get; set; }//define team id



        [ForeignKey("ApaTeamId")]
        public virtual ApaTeam? Team { get; set; }//used team id to pull that teams data only when requested.



        [Required, Display(Name = "Division")]
        public string DivisionNumber { get; set; } = string.Empty;//define which division the team is in.



        [Required, Display(Name = "Session")]
        public ApaSession Session { get; set; }//what session this is/was



        [Required, Display(Name = "Year")]
        public int Year { get; set; } = DateTime.Today.Year;//the year



        [Required, Display(Name = "Current Rank"), Range(1, 100)]
        public int CurrentRank { get; set; }//define standings



        [Required, Display(Name = "Total Team Points")]
        public int TotalPoints { get; set; }//define total points



        [Required, Display(Name = "Weeks Played")]
        public int WeeksPlayed { get; set; }//number of weeks in the session



        [Required, Display(Name = "Match Wins")]
        public int MatchesWon { get; set; }//define matches won.



        [Required, Display(Name = "Match Losses")]
        public int MatchesLost { get; set; }//define matches lost



        public int TotalMatches => MatchesWon + MatchesLost;//define total matches played.


        public double PointsPerWeek => WeeksPlayed > 0 ? Math.Round((double)TotalPoints / WeeksPlayed, 2) : 0.0;//define points made each week.
    }
}
