using System.ComponentModel.DataAnnotations;

namespace Santa.Models
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}