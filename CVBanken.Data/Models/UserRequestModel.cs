using System.ComponentModel.DataAnnotations;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    public class UserRequest
    {
        public class Register
        {
            [Required(ErrorMessage = "No Firstname was provided.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "No Lastname was provided.")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "No email was provided.")]
            //todo Lägg till @ITHS.SE som krav
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "No Password was provided")]
            public string Password { get; set; }
            [Required(ErrorMessage = "No programme Selected.")]
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