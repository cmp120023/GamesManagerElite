using System.ComponentModel.DataAnnotations;

namespace GamesManagerElite.Models
{
    public class LoginViewModel
    {
        //forces user to select league.
        [Required(ErrorMessage = "Please select a league.")]
        public string LeagueType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username or email is required.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]

        //specifies a data type, and that the data is password masking user input
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
