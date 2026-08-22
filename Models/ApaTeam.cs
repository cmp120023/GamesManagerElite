using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Game_Manager_Elite.Models
{
    public class ApaTeam
    {
        [Key]
        public int Id { get; set; }



        [Required, StringLength(100), Display(Name = "Team Name")]
        public string TeamName { get; set; } = string.Empty;



        [Required, Display(Name = "Format")]
        public GameFormat Format { get; set; }



        [Required, StringLength(50), Display(Name = "Division")]
        public string DivisionNumber { get; set; } = string.Empty;



        [Required, StringLength(100), Display(Name = "Host Location")]
        public string HostLocation { get; set; } = string.Empty;



        [Required, Display(Name = "Session")]
        public ApaSession Session { get; set; }



        [Required, Range(2020, 2100)]
        public int Year { get; set; } = DateTime.Today.Year;



        [Display(Name = "Total Points")]
        public int TotalPoints { get; set; } = 0;


        //ICollection interface allows for Entity Framework to use it own methods. 
        //again using virtual to only load data when requested, from the navigation properties.
        public virtual ICollection<ApaPlayer> Roster { get; set; } = new List<ApaPlayer>();
        public virtual ICollection<ApaMatch> HomeMatches { get; set; } = new List<ApaMatch>();
        public virtual ICollection<ApaMatch> AwayMatches { get; set; } = new List<ApaMatch>();
    }
}
