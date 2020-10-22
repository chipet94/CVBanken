using System.ComponentModel.DataAnnotations;
using API_CVPortalen.Models.Auth;
using CVBanken.Data.CustomValidaton;
using CVBanken.Data.Models.Auth;

namespace CVBanken.Data.Models.Requests.Auth
{
    public class RegisterRequest
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
        [Range(1000,9999, ErrorMessage = "Please select a valid programme.")]
        public int ProgrammeId { get; set; }

        public User ToUser()
        {
            return UserBuilder.NewUser(Email, Password, FirstName, LastName, ProgrammeId, Role.User);
        }
    }
}