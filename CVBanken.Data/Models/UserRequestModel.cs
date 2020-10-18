using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CVBanken.Data.CustomValidaton;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models
{
    
    public class UserRequest
    {
        public class Register
        {
            [Required(ErrorMessage = "No Firstname was provided.")]
            [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "No Lastname was provided.")]
            [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
            public string LastName { get; set; }
            
            [IthsEmail(ErrorMessage = "Not a valid @iths.se email.")]
            //todo Lägg till @ITHS.SE som krav
            public string Email { get; set; }
            
            [StringLength(25, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 8)]
            public string Password { get; set; }
            
            [Required(ErrorMessage = "No programme Selected.")]
            [Range(1,999, ErrorMessage = "Please select a valid programme.")]
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