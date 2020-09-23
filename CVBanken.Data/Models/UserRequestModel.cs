using System.ComponentModel.DataAnnotations;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    public class UserRequest
    {
        public class Register
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
        public class Authenticate
        {
            [Required ]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }
        public class Update
        {
            public string FirstName { get; set; }
        
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int ProgrammeId { get; set; }
        }
    }
    
}