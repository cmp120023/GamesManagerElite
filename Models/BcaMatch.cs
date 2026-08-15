using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game_Manager_Elite.Models
{
    /// <summary>
    /// Entity Model representing a head-to-head match fixture between two BCA teams, recording the match date, foreign keys for home/away teams, points scored by each team, and finalized match state.
    /// </summary>
    public class BcaMatch
    {
        [Key]
        public int MatchId { get; set; }

        [Required]
        [Display(Name = "Match Date")]
        [DataType(DataType.Date)]
        public DateTime MatchDate { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Home Team")]
        public int HomeTeamId { get; set; }

        [ForeignKey("HomeTeamId")]
        public BcaTeam? HomeTeam { get; set; }

        [Required]
        [Display(Name = "Away Team")]
        public int AwayTeamId { get; set; }

        [ForeignKey("AwayTeamId")]
        public BcaTeam? AwayTeam { get; set; }

        [Range(0, 100, ErrorMessage = "Score must be non-negative.")]
        [Display(Name = "Home Score")]
        public int HomeTeamScore { get; set; }

        [Range(0, 100, ErrorMessage = "Score must be non-negative.")]
        [Display(Name = "Away Score")]
        public int AwayTeamScore { get; set; }

        [Display(Name = "Match Finalized")]
        public bool IsFinalized { get; set; } = false;
    }
}