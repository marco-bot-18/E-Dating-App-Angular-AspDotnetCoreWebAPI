using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}