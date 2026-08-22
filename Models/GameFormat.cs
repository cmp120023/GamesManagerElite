using System.ComponentModel.DataAnnotations;

namespace Game_Manager_Elite.Models
{
    public enum GameFormat
    {
        [Display(Name = "8-Ball")]
        EightBall,

        [Display(Name = "9-Ball")]
        NineBall
    }
}
