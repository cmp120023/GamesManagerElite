using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game_Manager_Elite.Models
{
    /// <summary>
    /// Entity Model representing an individual BCA League player, tracking their first and last name, handicap skill rating (1-10), and foreign key link to an assigned BcaTeam.
    /// </summary>
    public class BcaPlayer
    {
        [Key]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "BCA Handicap must be between 1 and 10.")]
        [Display(Name = "Handicap Rating")]
        public int HandicapRating { get; set; } = 5;

        [Display(Name = "Assigned Team")]
        public int? TeamId { get; set; }

        [ForeignKey("TeamId")]
        public BcaTeam? Team { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName} (Rating: {HandicapRating})";
    }
}