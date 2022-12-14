using System.ComponentModel.DataAnnotations;

namespace BackEndAPI.DTOs
{
    public class RegisterDto
    {

        [Required] public string Username { get; set; }

        [Required] public string KnownAs { get; set; }

        [Required] public string Gender { get; set; }

        [Required] public DateOnly? DateOfBirth { get; set; } // optional to make required work!

        [Required] public string City { get; set; }

        [Required] public string Country { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}