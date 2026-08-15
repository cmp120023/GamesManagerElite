using System.ComponentModel.DataAnnotations;

namespace GamesManagerElite.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please select a league.")]
        public string LeagueType { get; set; } = "APA";

        [Required(ErrorMessage = "Username or email is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
