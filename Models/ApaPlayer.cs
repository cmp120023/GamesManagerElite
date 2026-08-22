using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game_Manager_Elite.Models
{
    public class ApaPlayer
    {
        [Key]
        public int Id { get; set; }



        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;



        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;



        [Required, RegularExpression(@"^\d{8}$", ErrorMessage = "Must be 8 digits.")]
        public string MembershipNumber { get; set; } = string.Empty;



        [Required, Range(2, 7)]
        public int EightBallSkillLevel { get; set; } = 3;



        [Required, Range(1, 9)]
        public int NineBallSkillLevel { get; set; } = 3;



        public bool IsActive { get; set; } = true;



        // Foreign Key to Team
        public int? ApaTeamId { get; set; }


        [ForeignKey("ApaTeamId")]
        public virtual ApaTeam? Team { get; set; }



        public string FullName => $"{FirstName} {LastName}";
    }
}
