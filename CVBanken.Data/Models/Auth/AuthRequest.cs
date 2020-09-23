using System.ComponentModel.DataAnnotations;

namespace API_CVPortalen.Models.Auth
{
    public class AuthRequest
    {
        [Required ]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        public int ProgrammeId { get; set; }
    }

    public class UpdateRequest
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ProgrammeId { get; set; }
    }
}