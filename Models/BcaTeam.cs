using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Game_Manager_Elite.Models
{
    /// <summary>
    /// Entity Model representing a BCA League Team, including its unique identifier, team name, division, full player roster collection, and associated home/away match histories.
    /// </summary>
    public class BcaTeam
    {
        [Key]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Team name must be between 2 and 60 characters.")]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; } = string.Empty;

        [Display(Name = "Division / Night")]
        public string Division { get; set; } = "Open Division";

        public List<BcaPlayer> Roster { get; set; } = new List<BcaPlayer>();
        public List<BcaMatch> HomeMatches { get; set; } = new List<BcaMatch>();
        public List<BcaMatch> AwayMatches { get; set; } = new List<BcaMatch>();
    }
}