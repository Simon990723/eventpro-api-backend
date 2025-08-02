using System.ComponentModel.DataAnnotations;

namespace EventProRegistration.DTOs
{
    public class AuthDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }

    public class RegisterDto : AuthDto
    {
        [Required]
        public required string Role { get; set; }
    }
}