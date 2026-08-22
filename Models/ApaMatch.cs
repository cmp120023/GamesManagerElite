using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game_Manager_Elite.Models
{
    
    public class ApaMatch
    {
        [Key]
        public int Id { get; set; }//each league match, (not player match AKA a game) pk



        [Required, DataType(DataType.Date)]
        public DateTime MatchDate { get; set; } = DateTime.Today;//defines the date of the match



        [Required]
        public GameFormat Format { get; set; }//defines the match type, 9 ball or 8 ball



        // --- Team Links ---
        [Required]
        public int HomeTeamId { get; set; }//defines the home teams id.

        [ForeignKey("HomeTeamId")]//uses home teamid to establish a connection to the Home team data, the fk as the hometeamid.
        public virtual ApaTeam? HomeTeam { get; set; }//pulls only necessary data.



        [Required]
        public int AwayTeamId { get; set; }//defines away team id.

        [ForeignKey("AwayTeamId")]//connection to the away team data.
        public virtual ApaTeam? AwayTeam { get; set; }//only pulls necessary data.



        // --- Aggregated Team Totals (Summed automatically from the 5 games) ---
        [Required, Range(0, 20)]
        public int HomeMatchPoints { get; set; }



        [Required, Range(0, 20)]
        public int AwayMatchPoints { get; set; }



        public bool IsPlayoff { get; set; }



        [StringLength(500)]
        public string? Notes { get; set; }



        // contructs a empty list, ready to hold match objects, that hold individual match data.
        //the data is only pulled when necessary.
        public virtual List<ApaPlayerMatch> PlayerMatches { get; set; } = new List<ApaPlayerMatch>();
    }

    /// <summary>
    /// Tracks individual Game 1 through Game 5 lineups and results for an overall APA team match.
    /// </summary>
    public class ApaPlayerMatch
    {
        [Key]
        public int Id { get; set; }



        // Parent Connection
        public int ApaMatchId { get; set; }


        [ForeignKey("ApaMatchId")]
        public virtual ApaMatch? ParentMatch { get; set; }



        [Required]
        public int GameNumber { get; set; } // Tracks Game 1, 2, 3, 4, or 5

        // --- Home Player Data ---
        [Required]
        public int HomePlayerId { get; set; }


        [ForeignKey("HomePlayerId")]
        public virtual ApaPlayer? HomePlayer { get; set; }



        [Required, Range(1, 9)]
        public int HomeSkillLevel { get; set; }



        [Required, Range(0, 20)]
        public int HomePointsEarned { get; set; }



        public int HomeDefensiveShots { get; set; }



        // --- Away Player Data ---
        [Required]
        public int AwayPlayerId { get; set; }


        [ForeignKey("AwayPlayerId")]
        public virtual ApaPlayer? AwayPlayer { get; set; }



        [Required, Range(1, 9)]
        public int AwaySkillLevel { get; set; }



        [Required, Range(0, 20)]
        public int AwayPointsEarned { get; set; }



        public int AwayDefensiveShots { get; set; }
    }
}
